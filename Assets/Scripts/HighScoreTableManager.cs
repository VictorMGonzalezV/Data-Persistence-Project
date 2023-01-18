using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.IO;




public class HighScoreTableManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] highScoreTexts = new TextMeshProUGUI[10];
    private MainManager.SaveData saveData;
    // Start is called before the first frame update
    void Start()
    {
        PopulateList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateList()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<MainManager.SaveData>(json);
            for(int i=0;i<highScoreTexts.Length;i++)
            {
                highScoreTexts[i].text = "" + saveData.playerNames[i] + "------" + saveData.scores[i];
            }
        }
    }
}
