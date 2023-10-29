using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlatformMovingLevel15 : MonoBehaviour
{
    [SerializeField] private Transform FirstPosition;
    [SerializeField] private Transform SecondPosition;

    private IEnumerator Start()
    {
        yield return transform.DOMove(FirstPosition.position, 5).WaitForCompletion();
        yield return transform.DOMove(SecondPosition.position, 5).WaitForCompletion();

        StartCoroutine(Start());
    }
}
