using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class HighScore : MonoBehaviour {
	void Start () {

        if (File.Exists(Application.persistentDataPath + "/savedRecord"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedRecord", FileMode.Open);
            GetComponent<Text>().text = "Highscore: " + (int)bf.Deserialize(file);
            file.Close();
        }
        if (GameObject.Find("DataCollector"))
        {
            GetComponent<Text>().text = "Highscore: " + GameObject.Find("DataCollector").GetComponent<DataSaver>().ReturnHighScore();
        }
    }
}
