using UnityEngine;
using UnityEngine.SceneManagement;

public class SDKSaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
		
        if (PlayerPrefs.GetInt("SavedRealLevel") == 0)
            PlayerPrefs.SetInt("SavedRealLevel", 1);
		
		SceneManager.LoadScene(PlayerPrefs.GetInt("SavedRealLevel"));
		//PlayerPrefs.DeleteAll();
    }
}
