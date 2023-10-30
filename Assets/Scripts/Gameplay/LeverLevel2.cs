using UnityEngine;

public class LeverLevel2 : MonoBehaviour
{
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.instacne.allowFinish = true;
            sr.color = new Color(0.63767f, 0.5529412f, 0.2980392f);
        }
    }
}
