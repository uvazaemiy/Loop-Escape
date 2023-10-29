using System;
using DG.Tweening;
using UnityEngine;

public class LadderLevel4 : MonoBehaviour
{
    [SerializeField] private Transform LadderPosition;
    private bool state;

    private void OnMouseDown()
    {
        if (!state)
        {
            transform.DOMove(LadderPosition.position, 1);
            GameManager.instacne.allowFinish = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            state = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            state = false;
    }
}
