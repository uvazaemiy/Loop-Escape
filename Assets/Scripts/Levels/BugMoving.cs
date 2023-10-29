using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class BugMoving : MonoBehaviour
{
    [SerializeField] private Transform[] AllPositions;
    private IEnumerator Start()
    {
        for (int i = 0; i < AllPositions.Length; i++)
        {
            yield return transform.DOMove(AllPositions[i].position, 2.5f).WaitForCompletion();
            if (i == 1 || i == 3)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        Array.Reverse(AllPositions);
        StartCoroutine(Start());
    }
}
