using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

	public void ExitGame()
    {
        GameObject.Find("DataCollector").GetComponent<DataSaver>().ResetCount();
        SceneManager.LoadScene(0);
    }
}
