using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SpikeVanishing18 : MonoBehaviour
{    
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
            collider.enabled = false;
            StartCoroutine(DestroySpike());
        }
    }

    private IEnumerator DestroySpike()
    {
        float randomValue = Random.Range(-0.025f, 0.025f);
        sr.DOColor(Color.red, 0.15f);
        transform.DOLocalMoveX(transform.position.x + randomValue, 0.25f).SetEase(Ease.InOutBounce);
        yield return transform.DOScale(0, 0.2f).SetEase(Ease.InBounce).WaitForCompletion();
        
        Destroy(gameObject);
    }
}
