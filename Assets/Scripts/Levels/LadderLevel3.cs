using System.Collections;
using DG.Tweening;
using UnityEngine;

public class LadderLevel3 : MonoBehaviour
{
    [SerializeField] private Transform LadderPosition;
    [SerializeField] private BoxCollider2D collider;
    private bool state = true;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (state)
            {
                collider.enabled = false;
                StartCoroutine(Activate());
            }
        }
    }

    private IEnumerator Activate()
    {
        state = false;
        
        foreach (Transform collider in GameManager.instacne.LadderBorders)
            collider.gameObject.SetActive(true);

        yield return transform.DOMove(LadderPosition.position, 1).WaitForCompletion();

        collider.enabled = true;
        GameManager.instacne.Player.transform.position += new Vector3(0, 0.2f, 0);
        
        StartCoroutine(GameManager.instacne.ChangeScene());
    }
}
