using System.Collections;
using DG.Tweening;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
    public static LeverManager instance;
    
    [SerializeField] private Transform Wall;
    [SerializeField] private Transform WallPosition;
    [SerializeField] private LeverLevel20[] AllLevers;

    private void Start()
    {
        instance = this;
    }

    public IEnumerator CheckState()
    {
        yield return null;
        
        int count = 0;
        foreach (LeverLevel20 lever in AllLevers)
            if (lever.state)
                count++;

        if (count == AllLevers.Length)
        {
            SoundController.instance.PlayWallSound();
            GameManager.instacne.allowFinish = true;
            Wall.DOMove(WallPosition.position, 4);
            foreach (LeverLevel20 lever in AllLevers)
                lever.collider.enabled = false;
        }
        else
            count = 0;
    }
}
