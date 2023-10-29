using System.Collections;
using DG.Tweening;
using UnityEngine;

public class LadderLevel7 : MonoBehaviour
{
    [SerializeField] private Transform LadderPosition;
    [SerializeField] private BoxCollider2D triggerCollider;
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private GameObject addCollider;

    public void Activate()
    {
        triggerCollider.enabled = true;
        transform.DOMove(LadderPosition.position, 1);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            triggerCollider.enabled = false;
            collider.enabled = true;
            addCollider.SetActive(true);

            GameManager.instacne.allowFinish = true;
        }
    }
}
