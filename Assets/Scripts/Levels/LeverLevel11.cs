using System.Collections;
using DG.Tweening;
using UnityEngine;

public class LeverLevel11 : MonoBehaviour
{
    [SerializeField] private Sprite openedLeverSprite;
    [SerializeField] private Sprite closedLeverSprite;
    [SerializeField] private Transform Wall;
    [SerializeField] private float timeForWall;
    [SerializeField] private Transform FirstWallPosition;
    [SerializeField] private Transform SecondWallPosition;

    private SpriteRenderer sr;
    private BoxCollider2D collider;

    private Tweener WallMoving;
    private Coroutine SoundRoutine;
        
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
            sr.sprite = openedLeverSprite;
            transform.position += new Vector3(0.12f, 0, 0);
            collider.enabled = false;

            WallMoving.Kill();
            StartCoroutine(StartMoving());
        }
    }

    private IEnumerator OnTriggerExit2D(Collider2D other)
    {
        yield return new WaitForSeconds(3);
        
        SoundController.instance.PlayLeverSound();
        sr.sprite = closedLeverSprite;
        transform.position -= new Vector3(0.12f, 0, 0);
        collider.enabled = true;
    }

    private IEnumerator StartMoving()
    {
        if (SoundRoutine != null)
            StopCoroutine(SoundRoutine);
        SoundRoutine = StartCoroutine(PlaySound());
        
        yield return Wall.DOMove(FirstWallPosition.position, 2).WaitForCompletion();

        yield return new WaitForSeconds(2);
        SoundRoutine = StartCoroutine(PlaySound());
        WallMoving = Wall.DOMove(SecondWallPosition.position, timeForWall).SetEase(Ease.Linear);
        WallMoving.Play();
        yield return new WaitForSeconds(4);
        SoundRoutine = StartCoroutine(PlaySound());
        yield return new WaitForSeconds(4);
        SoundRoutine = StartCoroutine(PlaySound());
        yield return new WaitForSeconds(2);
        StopCoroutine(SoundRoutine);
    }

    private IEnumerator PlaySound()
    {
        SoundController.instance.PlayWallSound();
        yield return new WaitForSeconds(4);
    }
}
