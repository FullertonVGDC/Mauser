using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class return_button : MonoBehaviour
{
    public void exitMinigame()
    {
        GlobalData.instance.ChangeMap(GlobalData.instance.GetSavedMapName());
    }
}