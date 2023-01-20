using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameInputField : MonoBehaviour
{

    public void ReportName(string name)
    {
        MenuManager.instance.EnterPlayerName(GameObject.FindObjectOfType< TMP_InputField>().text);
    }
   
}
