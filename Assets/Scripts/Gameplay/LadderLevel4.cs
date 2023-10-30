using DG.Tweening;
using UnityEngine;

public class LadderLevel4 : MonoBehaviour
{
    [SerializeField] private Transform LadderPosition;

    private void OnMouseDown()
    {
        transform.DOMove(LadderPosition.position, 1);
        GameManager.instacne.allowFinish = true;
    }
}
