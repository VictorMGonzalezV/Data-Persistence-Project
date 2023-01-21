using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//Use this to have access to Array.Sort, etc
using System.Linq;

public class HighScoreData : MonoBehaviour
{
    //The arrays could be defined as a small class that contains them, so it could be returned by a GetHighScores() method, but for this
    //project public access is safe
    public int[] highScores = new int[10];
    public string[] playerNames = new string[10];
    private string lastPlayer;
    private int lastScore;

    public static HighScoreData instance;

    public ScoreEntry[] scoreEntries = new ScoreEntry[10];
    public ScoreEntry newScoreEntry;

    public struct ScoreEntry
    {
       public string playerName;
       public int score;
    }

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
        instance.newScoreEntry.playerName = name;
        instance.newScoreEntry.score = points;
        instance.UpdateScores();
    }

    
    private void UpdateScores()
    {
        
        //No need to sort it first since it will start with a 0 in the last position anyway
        //If the new entry beats the lowest in the array it can be replaced and then the sorting can be automated with System.Array.Sort
        if(newScoreEntry.score>instance.scoreEntries[instance.scoreEntries.Length-1].score)
        {
            instance.scoreEntries[instance.scoreEntries.Length - 1].score= newScoreEntry.score;
            instance.scoreEntries[instance.scoreEntries.Length - 1].playerName = newScoreEntry.playerName;
        }
        //Reversing x and y sorts the array in descending order, which is what we need here
        System.Array.Sort<ScoreEntry>(instance.scoreEntries, (x, y) => y.score.CompareTo(x.score));
        
        //After the array has been sorted, start comparing from the lowest score
        /*for (int i=scoreEntries.Length-1;i>=0;i--)
        {
            if(instance.scoreEntries[i].score<instance.newScoreEntry.score)
            {
                if(i<scoreEntries.Length)
                {
                    for(int j=scoreEntries.Length-1;j<i+1;j++)
                    {
                        instance.scoreEntries[j].playerName = instance.scoreEntries[j - 1].playerName;
                        instance.scoreEntries[j].score = instance.scoreEntries[j - 1].score;
                    }
                    instance.scoreEntries[i-1].playerName = instance.newScoreEntry.playerName;
                    instance.scoreEntries[i-1].score = instance.newScoreEntry.score;

                }
                break;
            }

            
        }*/


        
        
    }

    private void GetBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            MainManager.SaveData data = JsonUtility.FromJson<MainManager.SaveData>(json);
            instance.scoreEntries[0].playerName = data.playerName;
            instance.scoreEntries[0].score = data.score;
        }
    }

}
