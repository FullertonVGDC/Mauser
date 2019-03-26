using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public Image mauserImage;
    public Image titleImage;
    public GameObject secretPanel;

    public KeyCode[] keysToPress;
    int currentKey;
    bool secretActivated;

    void Start()
    {
        GlobalData.instance.SetCurrency(0); //Absolutely filthy way of ensuring caps don't carry over into subsequent playthroughs
        GlobalData.instance.SetSavedCurrency(0);
        GlobalData.instance.StopTime();
        GlobalData.instance.SetTimer(0);

        LeanTween.delayedCall(3, () =>
        {
            LeanTween.moveX(mauserImage.gameObject, mauserImage.transform.position.x - (Screen.width * 1f), 2).setEase(LeanTweenType.easeOutCubic);
        });

        LeanTween.delayedCall(6, () =>
        {
            LeanTween.moveY(titleImage.gameObject, titleImage.transform.position.y - (Screen.height * 0.42f), 1).setEase(LeanTweenType.easeOutCubic);
        });
    }



    void Update()
    {
        //Konami code handler
        if (!secretActivated)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(keysToPress[currentKey]))
                {
                    currentKey++;
                    if (currentKey >= keysToPress.Length)
                    {
                        secretPanel.SetActive(true);
                        secretActivated = true;
                    }
                }
                else
                {
                    currentKey = 0;
                }
            }
        }
    }



    public void PlayGame()
    {
        LeanTween.cancelAll();
        GlobalData.instance.StartTime();
        GlobalData.instance.ChangeMap("level_garage");
    }

    public void Credits()
    {
        LeanTween.cancelAll();
        SceneManager.LoadScene("credits");
    }

    public void QuitGame()
    {
        LeanTween.cancelAll();
        Application.Quit();
    }

    public void GoToGarageScene()
    {
        LeanTween.cancelAll();
        GlobalData.instance.SetTimer(9999);
        GlobalData.instance.StartTime();
        GlobalData.instance.ChangeMap("level_garage");
    }

    public void GoToWallScene()
    {
        LeanTween.cancelAll();
        GlobalData.instance.SetTimer(9999);
        GlobalData.instance.StartTime();
        GlobalData.instance.ChangeMap("level_wall");
    }

    public void GoToKitchenScene()
    {
        LeanTween.cancelAll();
        GlobalData.instance.SetTimer(9999);
        GlobalData.instance.StartTime();
        GlobalData.instance.ChangeMap("level_kitchen");
    }
}