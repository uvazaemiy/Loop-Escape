using System.Collections;
using DG.Tweening;
using UnityEngine;

public class LadderLevel3 : MonoBehaviour
{
    [SerializeField] private Transform LadderPosition;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            transform.DOMove(LadderPosition.position, 1);
            StartCoroutine(Activate());
        }
    }

    private IEnumerator Activate()
    {
        yield return new WaitForSeconds(2);
        GameManager.instacne.allowFinish = true;
    }
}
