using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    private Canvas canvas;
    [Header("Level Panel")]
    [SerializeField] private Image LevelPanel;
    [SerializeField] private Text LevelText;
    private bool isPanelMoving;
    [Space]
    [Header("Tip Panel")]
    [SerializeField] private Image TipPanel;
    [SerializeField] private Image Fade;
    [SerializeField] private Text TipText;
    [SerializeField] private Image SkipButton;
    [SerializeField] private Text SkipText;
    [SerializeField] private Image CloseButton;
    [SerializeField] private Text CloseText;
    [Space]
    [SerializeField] private string levelText;
    [SerializeField] private string tipText;
    [Space]
    [Header("Settings")]
    [SerializeField] private float time = 0.2f;
    [SerializeField] private float ySettingsMoving;
    [SerializeField] private Transform SettingsButton;
    [SerializeField] private Transform SFXButton;
    [SerializeField] private Transform MusicButton;
    private bool isSettingsMoving;
    private bool stateOfSettings;
    private Image SFXImage;
    private Image MusicImage;

    private float xOffset = 1;
    private float yOffset = 1;

    private void Start()
    {
        instance = this;

        xOffset = Screen.width / 720;
        yOffset = Screen.height / 1280;

        canvas = GetComponent<Canvas>();
        SFXImage = SFXButton.GetComponent<Image>();
        MusicImage = MusicButton.GetComponent<Image>();


        if (PlayerPrefs.GetInt("SavedLevel") == 0)
            PlayerPrefs.SetInt("SavedLevel", 1);
        LevelText.text = PlayerPrefs.GetInt("SavedLevel") + ". " + levelText;
        TipText.text = tipText;
    }

    public void ShowTipPanel()
    {
        if (!isPanelMoving)
            StartCoroutine(ShowTipPanelRoutine());
    }
    
    private IEnumerator ShowTipPanelRoutine()
    {
        Time.timeScale = 0;
        isSettingsMoving = true;
        isPanelMoving = true;
        
        LevelPanel.DOFade(0, time).SetUpdate(true);
        yield return LevelText.DOFade(0, time).SetUpdate(true).WaitForCompletion();
        
        TipPanel.gameObject.SetActive(true);
        LevelPanel.gameObject.SetActive(false);

        SkipButton.DOFade(1, time).SetUpdate(true);
        SkipText.DOFade(1, time).SetUpdate(true);
        CloseButton.DOFade(1, time).SetUpdate(true);
        CloseText.DOFade(1, time).SetUpdate(true);
        TipText.DOFade(1, time).SetUpdate(true);
        yield return Fade.DOFade(0.4f, time).SetUpdate(true).WaitForCompletion();

        isPanelMoving = false;
    }

    public void CloseTipPanel()
    {
        StartCoroutine(CloseTipPanelRoutine());
    }

    private IEnumerator CloseTipPanelRoutine()
    {
        isPanelMoving = true;
        
        SkipButton.DOFade(0, time).SetUpdate(true);
        SkipText.DOFade(0, time).SetUpdate(true);
        CloseButton.DOFade(0, time).SetUpdate(true);
        CloseText.DOFade(0, time).SetUpdate(true);
        TipText.DOFade(0, time).SetUpdate(true);
        yield return Fade.DOFade(0, time).SetUpdate(true).WaitForCompletion();
        
        Time.timeScale = 1;
        TipPanel.gameObject.SetActive(false);
        LevelPanel.gameObject.SetActive(true);

        LevelText.DOFade(1, time);
        
        isPanelMoving = false;
        isSettingsMoving = false;
        
    }
    
    public void MoveSettingsButtons()
    {
        if (!isSettingsMoving)
            StartCoroutine(MoveButtonsRoutine());
    }
    
    private IEnumerator MoveButtonsRoutine()
    {
        isSettingsMoving = true;

        if (!stateOfSettings)
        {
            SFXButton.gameObject.SetActive(true);
            MusicButton.gameObject.SetActive(true);

            SFXImage.DOFade(1, time);
            MusicImage.DOFade(1, time);

            SFXButton.DOMoveY(SettingsButton.position.y - yOffset * ySettingsMoving, time);
            yield return MusicButton.DOMoveY(SettingsButton.position.y - yOffset * ySettingsMoving * 2, time).WaitForCompletion();
        }
        else
        {
            SFXImage.DOFade(0, time);
            MusicImage.DOFade(0, time);
            
            SFXButton.DOMoveY(SettingsButton.position.y, time);
            yield return MusicButton.DOMoveY(SettingsButton.position.y, time).WaitForCompletion();
            
            SFXButton.gameObject.SetActive(false);
            MusicButton.gameObject.SetActive(false);
        }
        
        stateOfSettings = !stateOfSettings;
        isSettingsMoving = false;
    }

    public void SkipFunc()
    {
        
        GameManager.instacne.ChangeSceneButton(true);
    }

    public void BackButton()
    {
        GameManager.instacne.BackToLevel();
    }
}
