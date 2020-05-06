using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataSaver : MonoBehaviour {

    //Saves data between scenes

    public int coinCount = 0;
    public static int highScore;
    public static DataSaver Instance;

    public bool newHighScore = false;
    private void Awake()
    {
        //only one datasaver can exist
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void AddCoin()
    {
        coinCount++;
        GameObject.Find("ScoreText").GetComponent<Text>().text = "Score: " + coinCount;
        GameObject.Find("PointText").GetComponent<Animator>().Play("CoinTaken");
        if (coinCount > highScore)
        {
            //highscore saved
            newHighScore = true;
            highScore = coinCount;
            GameObject.Find("HighScoreText").GetComponent<Text>().text = "Highscore: " + highScore;

            //save record
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(Application.persistentDataPath + "/savedRecord"))
            {
                FileStream file = File.Open(Application.persistentDataPath + "/savedRecord", FileMode.Open);
                bf.Serialize(file, highScore);
                file.Close();
            }
            else
            {
                FileStream file = File.Create(Application.persistentDataPath + "/savedRecord");
                bf.Serialize(file, highScore);
                file.Close();
            }
        }
    }
    public int ReturnHighScore()
    {
        return highScore;
    }
    public void ResetCount()
    {
        newHighScore = false;
        coinCount = 0;
    }
}
