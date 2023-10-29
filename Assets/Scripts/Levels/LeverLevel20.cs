using UnityEngine;
using UnityEngine.Serialization;

public class LeverLevel20 : MonoBehaviour
{
    [SerializeField] private Sprite openedLeverSprite;
    [SerializeField] private Sprite closedLeverSprite;
    [SerializeField] private LeverLevel20[] Levers;
    public bool state;

    private SpriteRenderer sr;
    [HideInInspector] public BoxCollider2D collider;
        
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            foreach (LeverLevel20 lever in Levers)
                lever.Trigger();
    }

    public void Trigger()
    {
        SoundController.instance.PlayLeverSound();

        if (state)
        {
            sr.sprite = closedLeverSprite;
            transform.position -= new Vector3(0.12f, 0, 0);
        }
        else
        {
            sr.sprite = openedLeverSprite;
            transform.position += new Vector3(0.12f, 0, 0);
        }

        state = !state;
        
        StartCoroutine(LeverManager.instance.CheckState());
    }
}
