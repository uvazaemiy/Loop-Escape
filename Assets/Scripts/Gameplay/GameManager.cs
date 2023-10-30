using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instacne;

    public PlayerController Player;
    [SerializeField] private Transform PlayersEndPosition;
    public PhysicsMaterial2D PlayerPhysMat;
    public Transform SpawnPosition;
    public bool allowFinish;

    private void Start()
    {
        Application.targetFrameRate = 120;
        
        instacne = this;
    }

    public IEnumerator ChangeScene()
    {
        yield return Player.transform.DOMove(PlayersEndPosition.position, 2).WaitForCompletion();
        
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        currentScene = currentScene == 3 ? 0 : currentScene + 1;

        DOTween.KillAll();
        SceneManager.LoadScene(currentScene);
    }
}
