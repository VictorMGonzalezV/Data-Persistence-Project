using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.IO;




public class HighScoreTableManager : MonoBehaviour
{
    //All of the actual score data has been moved to the HighScoreData class, this script now handles only the UI updating
    //public static HighScoreTableManager instance;
    //private int[] highScores = new int[10];
    //private string[] playerNames = new string[10];
    //private int lastScore;
    //private string lastPlayer;
    [SerializeField] TextMeshProUGUI[] highScoreTexts = new TextMeshProUGUI[10];
    //Set up table manager singleton
    private void Awake()
    {
        //A singleton isn't needed for this class anymore, it was causing trouble because the UI references broke down when reloading
        /*if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);*/
    }

    
    
    // Start is called before the first frame update
    void Start()
    {
        //GetBestScore();
        PopulateList();
        Debug.Log($" ==> { string.Join(" ==> ", HighScoreData.instance.highScores)}");
        Invoke("LoadMenu", 10f);
        //Debug.Log(HighScoreData.instance.lastScore);
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LoadMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    //This logic has been moved to the HighScoreData class
    /*public void ReportScore(string name, int points)
    {
        instance.lastPlayer = name;
        instance.lastScore = points;
    }

    private void GetBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            MainManager.SaveData data = JsonUtility.FromJson<MainManager.SaveData>(json);
            instance.playerNames[0] = data.playerName;
            instance.highScores[0] = data.score;
        }
    }*/

    public void PopulateList()
    {
        for (int i=0;i<highScoreTexts.Length;i++)
        {
            highScoreTexts[i].text = "" + HighScoreData.instance.playerNames[i] + "- - - - - " + HighScoreData.instance.highScores[i];
        }
    }
}
