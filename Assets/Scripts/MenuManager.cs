using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] TextMeshProUGUI nameWarning;
    [SerializeField] TextMeshProUGUI offensiveWarning;
    private string playerName;

    public static MenuManager instance;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (playerName!=null)
        {
            SceneManager.LoadScene("main");
        }
        nameWarning.enabled = true;
        
    }

    //This is setup this way so the TMP_InputField can work, it doesn't accept a method with a string return type
    public void EnterPlayerName()
    {
        
        instance.playerName = nameInputField.text;
        if(instance.playerName.Length>12)
        {
            instance.playerName = null;
            nameWarning.enabled = true;
        }
        //This is not an optimal implementation as ToUpper/ToLower return a new instance, increasing memory allocation
        //For an optimal case-insensitive validation an extension method using String.IndexOf(substring,StringComparison.OrdinalIgnoreCase)
        //can be used, or regular expressions. This project won't need to store a large volume of strings so it's fine.
        if(instance.playerName.ToUpper().Contains("HITLER")|| instance.playerName.ToUpper().Contains("SHIT")|| instance.playerName.ToUpper().Contains("FUCK"))
        {
            offensiveWarning.enabled = true;
            instance.playerName = null;
        }
       
    }

    //This method allows the MainManager to access the property (making playerName static should also work)
    public string GetPlayerName()
    {
        return instance.playerName;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
