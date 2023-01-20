using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScoreData : MonoBehaviour
{
    //The arrays could be defined as a small class that contains them, so it could be returned by a GetHighScores() method, but for this
    //project public access is safe
    public int[] highScores = new int[10];
    public string[] playerNames = new string[10];
    private string lastPlayer;
    private int lastScore;

    public static HighScoreData instance;

    //Create singleton, this object needs to be created when the first scene loads to prevent NullRefs
    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        GetBestScore();
        //UpdateScores();
    }

    public void ReportScore(string name, int points)
    {
        instance.lastPlayer = name;
        instance.lastScore = points;
        Debug.Log("Has make receive " + instance.lastPlayer + instance.lastScore);
        instance.UpdateScores();
    }

    //This method needs logic to sort the scores by points, but the first version can do without it
    private void UpdateScores()
    {

        //initialize i at 1 so index 0 is reserved for the best score, which is loaded from the save file by another method
        for (int i = 1; i < instance.highScores.Length; i++)
        {
            //First check against zero to find an empty slot
            if(instance.highScores[i]==0)          
            {
                instance.highScores[i] = instance.lastScore;
                instance.playerNames[i] = instance.lastPlayer;
                return;
            }

        }
        //If all slots are filled, replace the lowest score
        //start from 1 to ignore the highest score
        for (int j=1;j<highScores.Length;j++)
        {
            if (instance.lastScore > Mathf.Min(instance.highScores))

            {
                instance.highScores[j] = instance.lastScore;
                instance.playerNames[j] = instance.lastPlayer;
                break;
            }
        }
          
        Debug.Log("Can into update");
        
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
    }

}
