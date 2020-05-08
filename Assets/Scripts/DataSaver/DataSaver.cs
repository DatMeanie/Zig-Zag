using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataSaver : MonoBehaviour {

    /////////////////////////////////////////////////////////////
    // VARIABLES
    /////////////////////////////////////////////////////////////

    public static DataSaver singleton = null;
    
    public int coinCount = 0;
    public static int highScore = 0;

    public bool newHighScore = false;

    /////////////////////////////////////////////////////////////
    // AWAKE & START
    /////////////////////////////////////////////////////////////

    private void Awake()
    {
        if( singleton == null )
        {
            DontDestroyOnLoad( gameObject );
            singleton = this;
        }
        else if( singleton != this )
        {
            Destroy( gameObject );
        }
    }
    
    /////////////////////////////////////////////////////////////

    public void AddCoin()
    {
        coinCount++;
        GameObject.Find( "ScoreText" ).GetComponent<Text>().text = "Score: " + coinCount;
        GameObject.Find( "PointText" ).GetComponent<Animator>().Play("CoinTaken");

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

    /////////////////////////////////////////////////////////////

    public void ResetCount()
    {
        newHighScore = false;
        coinCount = 0;
    }

    /////////////////////////////////////////////////////////////
}
