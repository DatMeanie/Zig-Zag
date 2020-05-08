using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPad : MonoBehaviour {

    PlayerController player;

    private void Start()
    {
        player = GameObject.Find( "Player" ).GetComponent<PlayerController>();
    }

    private void OnTriggerEnter( Collider other )
    {
        if( other.gameObject.name == "Player" )
        {
            StartCoroutine( Speed() );
        }
    }

    IEnumerator Speed()
    {
        player.movementSpeed += 2;
        yield return new WaitForSeconds(1);
        player.movementSpeed -= 2;
    }
}
