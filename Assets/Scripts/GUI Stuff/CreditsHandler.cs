using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsHandler : MonoBehaviour
{
    public Text bestCapAmountText;
    public Text bestTimeText;

    void Start()
    {
        bestCapAmountText.text = "Highest Cap Amount: " + GlobalData.instance.GetHighestCurrency().ToString();
        bestTimeText.text = "Best Time: " + GlobalData.instance.GetBestTime().ToString("F2") + " seconds";
    }

    public void BackButton()
    {
        SceneManager.LoadScene("main_menu");
    }
}