using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private const string KEY_HIGH_SCORE = "High Score";
    private const string DATA_DIR = "/data/";
    private const string DATA_HS_FILE = "hs.txt";
    private string DATA_FULL_HS_FILE_PATH;

    private int score = 0;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            Debug.Log("Score changed!");

            if (score > highScore)
            {
                highScore = score;
            }
        }
    }

    private int highScore = 0;

    public int HighScore
    {
        get
        {
            if (File.Exists(DATA_FULL_HS_FILE_PATH))
            {
                string fileContents = File.ReadAllText(DATA_FULL_HS_FILE_PATH);
                highScore = Int32.Parse(fileContents);
            }

            return highScore;
        }

        // when high score is changed, reset the value and renew the directory
        set
        {
            highScore = value;
            Debug.Log("new high score! ");
            string fileContent = "" + highScore;

            // Create the directory in first run
            if (!Directory.Exists(Application.dataPath + DATA_DIR))
            {
                Directory.CreateDirectory(Application.dataPath + DATA_DIR);
            }
            
            File.WriteAllText(DATA_FULL_HS_FILE_PATH, fileContent);
        }
    }

    // Set this game manager a singleton
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        DATA_FULL_HS_FILE_PATH = Application.dataPath + DATA_DIR + DATA_HS_FILE;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.DeleteKey(KEY_HIGH_SCORE);
        }
        
    }
}
