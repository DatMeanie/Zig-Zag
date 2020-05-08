using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMenu : MonoBehaviour {

    /////////////////////////////////////////////////////////////
    // VARIABLES
    /////////////////////////////////////////////////////////////

    //A mix of the GenerateMap and Playecontroller scripts. Functions almost identically
    //Simplified both scripts for an adequate background

    public GameObject platform;
    public GameObject player;
    Vector3 position = new Vector3(12, -26, 10);
    GameObject[] instantiatedPlatforms = new GameObject[ 30 ];
    
    int currentTile = 0;
    
    /////////////////////////////////////////////////////////////

    void Start ()
    {
        int count = 0;
        for ( int i = 0; i < 30; i++ )
        {
            instantiatedPlatforms[ i ] = Instantiate( platform );
            instantiatedPlatforms[ i ].transform.position = new Vector3( count * 100, count * 100, count * 100 );
            count += 2; // 2 is the size of the spawned platform
        }
	}
    
    /////////////////////////////////////////////////////////////
	
    void Update() 
    {
        player.GetComponent<Rigidbody>().velocity = new Vector3( 10.0f, 0, 0 );
        if ( Vector3.Distance( transform.position, player.transform.position) < 8 )
        {
            MakeMap();
        }
    }
    
    /////////////////////////////////////////////////////////////
    
    void MakeMap()
    {
        if ( currentTile == 30 )
        {
            currentTile = 0;
        }
        transform.position = position;
        instantiatedPlatforms[ currentTile ].transform.position = position;
        instantiatedPlatforms[ currentTile ].transform.rotation = Quaternion.identity;
        instantiatedPlatforms[ currentTile ].GetComponent<Rigidbody>().isKinematic = true;
        position = new Vector3( position.x + 2, position.y, position.z );
        currentTile++;
    }

    /////////////////////////////////////////////////////////////
}
