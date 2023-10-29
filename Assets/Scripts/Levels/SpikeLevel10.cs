using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpikeLevel10 : MonoBehaviour
{
    [SerializeField] private Image ButtonImage;

    private SpriteRenderer sr;
    private BoxCollider2D collider;
    [SerializeField] private bool state;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ButtonImage.gameObject.SetActive(true);
            ButtonImage.DOFade(1, 1);

            if (state)
            {
                collider.enabled = false;
                StartCoroutine(DisableGameObject());
            }
        }
    }

    private IEnumerator DisableGameObject()
    {
        yield return sr.DOFade(0, 1).WaitForCompletion();
        gameObject.SetActive(false);
    }
}
