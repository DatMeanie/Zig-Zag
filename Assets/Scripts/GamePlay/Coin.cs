using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    DataSaver dataSaver = null;
    new Renderer renderer = null;

    private void Start()
    {
        dataSaver = GameObject.Find( "DataCollector" ).GetComponent<DataSaver>();
        renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.name == "Player" && renderer.enabled == true )
        {
            dataSaver.AddCoin();
            StartCoroutine(Vanish());
        }
    }

    IEnumerator Vanish()
    {
        //makes it invisible
        renderer.enabled = false;
        yield return new WaitForSeconds( 2 );
        renderer.enabled = true;
    }
}
