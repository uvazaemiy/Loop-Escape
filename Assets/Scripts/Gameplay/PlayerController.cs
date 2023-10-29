using System;
using System.Collections;
using Spine.Unity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    
    public BoxCollider2D collider;
    public BoxCollider2D addCollider;
    public SkeletonAnimation skeletonAnimation;
    
    [HideInInspector] public Rigidbody2D rb;
    private bool ground;
    public bool isMoving;
    public bool jumping;

    private void Start()
    {
        instance = this;
        
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()   //DEBUG
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isMoving = true;

            StartCoroutine(Move(1));

            skeletonAnimation.state.SetAnimation(0, "Step", true);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            isMoving = true;

            StartCoroutine(Move(-1));
            
            skeletonAnimation.state.SetAnimation(0, "Step", true);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            GameManager.instacne.Player.collider.sharedMaterial = GameManager.instacne.PlayerPhysMatGround;
            GameManager.instacne.Player.isMoving = false;
            
            skeletonAnimation.state.SetAnimation(0, "animation Costs", true);
        }
    }

    public IEnumerator Move(float value)
    {
        collider.sharedMaterial = GameManager.instacne.PlayerPhysMatIce;

        if (value != 0)
        {
            rb.velocity = new Vector2(value * moveSpeed * Time.deltaTime, rb.velocity.y);
            
            transform.localScale = new Vector3( Mathf.Abs(transform.localScale.x)* value, transform.localScale.y, transform.localScale.z);
        }
        else if (ground)
        {
            ground = false;
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            
            //yield return new WaitForSeconds(0.07f);
            skeletonAnimation.state.SetAnimation(0, "Jump new", false);
            StartCoroutine(HoldJump());
        }

        yield return new WaitForEndOfFrame();

        if (value != 0 && isMoving)
            StartCoroutine(Move(value));
        else if (value != 0)
            rb.velocity = Vector2.zero;
    }

    private IEnumerator HoldJump()
    {
        jumping = true;
        yield return new WaitForSeconds(0.5f);
        
        if (isMoving)
            skeletonAnimation.state.SetAnimation(0, "Step", true);
        else
            skeletonAnimation.state.SetAnimation(0, "animation Costs", true);
        
        jumping = false;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (rb.velocity.y == 0)
        {
            ground = true;

            GameManager.instacne.Player.collider.sharedMaterial = GameManager.instacne.PlayerPhysMatGround;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Floor")
        {
            ground = true;
            
            GameManager.instacne.Player.collider.sharedMaterial = GameManager.instacne.PlayerPhysMatGround;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        ground = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Finish" && GameManager.instacne.allowFinish)
            StartCoroutine(GameManager.instacne.ChangeScene());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Spike")
            StartCoroutine(GameManager.instacne.DeathPlayer(rb.velocity));
    }
}
