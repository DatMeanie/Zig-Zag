using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Reset : MonoBehaviour {

    //animating objects
    public GameObject lostText;
    public GameObject newRecord;
    public GameObject exitButton;
    public GameObject playAgainButton;
    private void Start()
    {
        Cursor.visible = false;
    }
    //if player falls into object
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Cursor.visible = true;
            //disabling not needed scripts
            other.gameObject.GetComponent<PlayerController>().enabled = false;
            Camera.main.GetComponent<CameraController>().enabled = false;
            //animating
            lostText.GetComponent<Animator>().Play("YouLost");
            exitButton.GetComponent<Animator>().Play("ExitButton");
            playAgainButton.GetComponent<Animator>().Play("PlayAgainButton");
            if (GameObject.Find("DataCollector").GetComponent<DataSaver>().newHighScore == true)
            {
                newRecord.GetComponent<Animator>().Play("NewRecord");
            }
        }
    }
}
