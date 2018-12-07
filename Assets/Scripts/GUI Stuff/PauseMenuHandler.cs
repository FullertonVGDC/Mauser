using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    public void ResumeButton()
    {
        GameObject.Find("GameplayGUI").GetComponent<GameplayGUI>().SetPaused(false);
        GlobalData.instance.GetComponent<AudioSource>().volume = 0.8f;
    }

    public void QuitButton()
    {
        GlobalData.instance.StopMusic();
		GlobalData.instance.UnPause();
		GlobalData.instance.SetCheckpointEnabled (false);
		GlobalData.instance.GetComponent<AudioSource>().volume = 0.8f;
        SceneManager.LoadScene("main_menu");
        Application.Quit();
    }
}