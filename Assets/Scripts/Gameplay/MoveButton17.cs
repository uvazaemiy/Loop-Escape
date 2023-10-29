using UnityEngine;

public class MoveButton17 : MonoBehaviour
{
    public static MoveButton17 instance;
    
    [SerializeField] private float value;
    [HideInInspector] public int jumpCount = 2;

    private void Start()
    {
        instance = this;
    }

    public void ButtonDown()
    {
        if (jumpCount > 0)
        {
            jumpCount--;
            StartCoroutine(GameManager.instacne.Player.Move(value));

            if (value != 0)
            {
                GameManager.instacne.Player.isMoving = true;

                if (!PlayerController.instance.jumping)
                    GameManager.instacne.Player.skeletonAnimation.state.SetAnimation(0, "Step", true);
            }
            
            if (jumpCount == 0)
                gameObject.SetActive(false);
        }
    }

    public void ButtonUp()
    {
        if (value != 0)
        {
            GameManager.instacne.Player.isMoving = false;

            GameManager.instacne.Player.collider.sharedMaterial = GameManager.instacne.PlayerPhysMatGround;
            GameManager.instacne.Player.skeletonAnimation.state.SetAnimation(0, "animation Costs", true);
        }
    }
}