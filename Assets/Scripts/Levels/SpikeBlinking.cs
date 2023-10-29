using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SpikeBlinking : MonoBehaviour
{
    [SerializeField] private Color redColor;
    private SpriteRenderer sr;

    private IEnumerator Start()
    {
        sr = GetComponent<SpriteRenderer>();
        
        yield return sr.DOColor(redColor, 0.5f).WaitForCompletion();
        yield return sr.DOColor(new Color(1, 1, 1, 0), 0.5f).WaitForCompletion();
        yield return new WaitForSeconds(3);

        StartCoroutine(Start());
    }
}
