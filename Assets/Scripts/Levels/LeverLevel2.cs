using DG.Tweening;
using UnityEngine;

public class LeverLevel2 : MonoBehaviour
{
    [SerializeField] private Sprite openedLeverSprite;
    [SerializeField] private Sprite closedLeverSprite;
    [SerializeField] private Transform Wall;
    [SerializeField] private Transform WallPosition;

    private SpriteRenderer sr;
    private BoxCollider2D collider;
        
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SoundController.instance.PlayLeverSound();
            SoundController.instance.PlayWallSound();
            GameManager.instacne.allowFinish = true;
            sr.sprite = openedLeverSprite;
            transform.position += new Vector3(0.12f, 0, 0);
            collider.enabled = false;

            Wall.DOMove(WallPosition.position, 4);
        }
    }

    public void ResetLever()
    {
        sr.sprite = closedLeverSprite;
        transform.position -= new Vector3(0.12f, 0, 0);
        collider.enabled = true;
    }
}
