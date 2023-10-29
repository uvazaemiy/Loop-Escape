using System.Collections;
using DG.Tweening;
using UnityEngine;

public class WebManager : MonoBehaviour
{
    [SerializeField] private int lifes = 3;
    [SerializeField] private Color redColor;
    [SerializeField] private LadderLevel7 ladder;

    private SpriteRenderer sr;
    private CircleCollider2D collider;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            lifes--;
            if (lifes >= 0)
                StartCoroutine(ShakeWeb());
        }
    }

    private IEnumerator ShakeWeb()
    {
        sr.DOColor(redColor, 0.1f);
        yield return transform.DOMoveX(transform.position.x + 0.025f, 0.2f).SetEase(Ease.OutBounce).WaitForCompletion();
        sr.DOColor(Color.white, 0.2f);
        yield return transform.DOMoveX(transform.position.x - 0.025f, 0.2f).SetEase(Ease.OutBounce).WaitForCompletion();
        
        if (lifes <= 0)
        {
            collider.enabled = false;
            yield return transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.InBounce).WaitForCompletion();
            if (ladder)
                ladder.Activate();
            Destroy(gameObject);
        }
    }
}
