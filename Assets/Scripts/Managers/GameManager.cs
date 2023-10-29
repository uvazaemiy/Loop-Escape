using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instacne;
    
    public PlayerController Player;
    public GameObject DeadPlayer;
    [SerializeField] private Transform Ladder;
    public Transform[] LadderBorders;
    [SerializeField] private Transform PlayersEndPosition;
    public PhysicsMaterial2D PlayerPhysMatIce;
    public PhysicsMaterial2D PlayerPhysMatGround;
    public Transform SpawnPosition;
    public bool allowFinish;
    
    private void Start()
    {
        Time.timeScale = 1;        
        instacne = this;
    }

    public IEnumerator DeathPlayer(Vector2 newVelocity)
    {
        SoundController.instance.PlayDeathSound();
        DeadPlayer.SetActive(true);
        DeadPlayer.transform.position = Player.transform.position + new Vector3(0, 0.2f);

        yield return null;
        
        Player.rb.velocity = Vector2.zero;
        Player.transform.position = SpawnPosition.position;

        if (SceneManager.GetActiveScene().buildIndex == 17)
        {
            MoveButton17.instance.gameObject.SetActive(true);
            MoveButton17.instance.jumpCount = 2;
        }
    }

    public IEnumerator ChangeScene()
    {
        allowFinish = false;
        
        foreach (Transform collider in LadderBorders)
            collider.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        Player.collider.enabled = false;
        Player.addCollider.enabled = true;
        /*Player.isMoving = false;
        Player.skeletonAnimation.state.SetAnimation(0, "animation Costs", true);*/

        yield return Ladder.transform.DOMove(PlayersEndPosition.position, 2).WaitForCompletion();
        
        ChangeSceneButton();
    }

    public void ChangeSceneButton(bool certainScene = false)
    {
        DOTween.KillAll();

        PlayerPrefs.SetInt("SavedLevel", PlayerPrefs.GetInt("SavedLevel") + 1);
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        currentScene = currentScene == 20 ? 1 : currentScene + 1;
        PlayerPrefs.SetInt("SavedRealLevel", currentScene);
        
        SceneManager.LoadScene(currentScene);
    }

    public void BackToLevel()
    {
        DOTween.KillAll();

        PlayerPrefs.SetInt("SavedLevel", PlayerPrefs.GetInt("SavedLevel") - 1);
        
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        currentScene = currentScene == 1 ? 20 : currentScene - 1;
        PlayerPrefs.SetInt("SavedRealLevel", currentScene);
        
        SceneManager.LoadScene(currentScene);
    }
}
