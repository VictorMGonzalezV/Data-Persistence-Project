using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//Remember to add this so Save/Load can work
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private int bestScore=0;
    private string bestPlayer;
    
    private bool m_GameOver = false;
   
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        LoadScore();

        HighScoreText.text = "Best Score: " + bestPlayer + ": "+bestScore; 
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            } else if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("HighScoreTable");
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        if(m_Points>bestScore)
        {
              SaveScore();
        }
        else
        {
            HighScoreData.instance.ReportScore(MenuManager.instance.GetPlayerName(), m_Points);
            Debug.Log("Reportings of " + MenuManager.instance.GetPlayerName() +""+ m_Points);
        }
       
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
    //Class definition for the High Score save data
    [System.Serializable]

    public class SaveData
    {
        //Always make the fields you want to save public, ToJson() only reads those
        public string playerName;
        public int score;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.playerName = MenuManager.instance.GetPlayerName();
        data.score = m_Points;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
   
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestPlayer = data.playerName;
            bestScore = data.score;
        }
    }

    //This doesn't work, JsonUtility only recognizes one save file in the required path
    private string GenerateSaveFileName(int slot)
    {
        return "/"+slot+"savefile.json";
    }
}
