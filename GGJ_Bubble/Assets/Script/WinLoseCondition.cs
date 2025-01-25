using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseCondition : MonoBehaviour
{
    public static WinLoseCondition instance;
    public GameObject _prefabWin;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void WinGame()
    {
        SceneManager.LoadScene(2);
        AudioManager.instance.ChangeMusic(SoundType.WinTheme, 0.35f);
    }

    public void LoseGame()
    {
        SceneManager.LoadScene(3);
        AudioManager.instance.ChangeMusic(SoundType.LoseTheme, 0.35f);
    }
}
