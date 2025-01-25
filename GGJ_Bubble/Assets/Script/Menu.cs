using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        AudioManager.instance.ChangeMusic(SoundType.LvlTheme, 0.35f);
    }

    public void Click()
    {
        AudioManager.instance.PlaySFX(SoundType.Click, 2f);
    }

    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void RetryButton()
    {
        AudioManager.instance.ChangeMusic(SoundType.MenuTheme, 0.7f);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
