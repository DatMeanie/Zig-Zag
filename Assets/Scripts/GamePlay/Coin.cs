using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    //player enter coin, coin vanish and add to score
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            StartCoroutine(Vanish());
        }
    }
    IEnumerator Vanish()
    {
        GameObject.Find("DataCollector").GetComponent<DataSaver>().AddCoin();
        //makes it invisible
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(2);
        GetComponent<MeshRenderer>().enabled = true;
    }
}
