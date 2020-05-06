using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMenu : MonoBehaviour {

    //A mix of the GenerateMap and Playecontroller scripts. Functions almost identically
    //Simplified both scripts for an adequate background

    public GameObject platform;
    public GameObject player;
    Vector3 position = new Vector3(12, -26, 10);
    GameObject[] goArray = new GameObject[30];
    int currentTile = 0;
	void Start () {
        int count = 0;
        for (int i = 0; i < 30; i++)
        {
            goArray[i] = Instantiate(platform);
            goArray[i].transform.position = new Vector3(count * 100, count * 100, count * 100);
            count += 2;
        }
	}
	void Update () {
        //simulate player
        player.GetComponent<Rigidbody>().velocity = new Vector3(1 * 10, 0, 0);
        if (Vector3.Distance(transform.position, player.transform.position) < 8)
        {
            MakeMap();
        }
    }
    //Simplified version of GenerateMap script
    void MakeMap()
    {
        if (currentTile == 30)
        {
            currentTile = 0;
        }
        transform.position = position;
        goArray[currentTile].transform.position = position;
        goArray[currentTile].transform.rotation = Quaternion.identity;
        goArray[currentTile].GetComponent<Rigidbody>().isKinematic = true;
        position = new Vector3(position.x + 2, position.y, position.z);
        currentTile++;
    }
}
