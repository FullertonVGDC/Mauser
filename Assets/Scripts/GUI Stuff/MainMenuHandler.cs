using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public Image mauserImage;
    public Image titleImage;

    void Start()
    {
        LeanTween.delayedCall(3, () =>
        {
            LeanTween.moveX(mauserImage.gameObject, mauserImage.transform.position.x - (Screen.width * 1f), 2).setEase(LeanTweenType.easeOutCubic);
        });

        LeanTween.delayedCall(6, () =>
        {
            LeanTween.moveY(titleImage.gameObject, titleImage.transform.position.y - (Screen.height * 0.42f), 1).setEase(LeanTweenType.easeOutCubic);
        });
    }



    public void PlayGame()
    {
        LeanTween.cancelAll();
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
        GlobalData.instance.ChangeMap("level_garage");
    }

    public void GoToWallScene()
    {
        LeanTween.cancelAll();
        GlobalData.instance.ChangeMap("level_wall");
    }

    public void GoToKitchenScene()
    {
        LeanTween.cancelAll();
        GlobalData.instance.ChangeMap("level_kitchen");
    }
}