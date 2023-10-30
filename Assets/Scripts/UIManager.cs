using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    public float time;
    
    [SerializeField] private Image TextPanel;
    [SerializeField] private Text LevelText;
    [Space]
    [SerializeField] private string levelText;
    [SerializeField] private string tipText;

    private void Start()
    {
        instance = this;

        StartCoroutine(ShowLevelText(levelText));
    }

    private IEnumerator ShowLevelText(string text)
    {
        TextPanel.gameObject.SetActive(true);
        LevelText.text = text;
        
        TextPanel.DOFade(0.5f, time);
        yield return LevelText.DOFade(1, time).WaitForCompletion();

        yield return new WaitForSeconds(3);
        
        TextPanel.DOFade(0, time);
        yield return LevelText.DOFade(0, time).WaitForCompletion();
        
        TextPanel.gameObject.SetActive(false);
    }

    public void ShowTip()
    {
        StartCoroutine(ShowLevelText(tipText));
    }
}
