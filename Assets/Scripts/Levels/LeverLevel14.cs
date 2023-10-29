using System.Collections;
using DG.Tweening;
using UnityEngine;

public class LeverLevel14 : MonoBehaviour
{
    [SerializeField] private Sprite openedLeverSprite;
    [SerializeField] private Sprite closedLeverSprite;
    [SerializeField] private Transform Wall;
    [SerializeField] private Transform WallPosition;
    [SerializeField] private Transform Ladder;
    [SerializeField] private Transform LadderPosition;

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
            Wall.DOMove(WallPosition.position, 4);
            sr.sprite = openedLeverSprite;
            transform.position += new Vector3(0.12f, 0, 0);
            collider.enabled = false;

            if (Ladder)
                StartCoroutine(LiftLadder());
        }
    }

    private IEnumerator LiftLadder()
    {
        yield return new WaitForSeconds(2f);
        Ladder.DOMove(LadderPosition.position, 2);
    }
}
