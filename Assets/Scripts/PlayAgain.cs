using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour {

	public void ResetLevel()
    {
        GameObject.Find("DataCollector").GetComponent<DataSaver>().ResetCount();
        SceneManager.LoadScene( 1 );
    }
}
