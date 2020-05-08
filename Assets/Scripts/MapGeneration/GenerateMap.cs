using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour {

    /////////////////////////////////////////////////////////////
    // PUBLIC VARIABLES
    /////////////////////////////////////////////////////////////

    List<GameObject> platformList = new List<GameObject>();
    
    //array used for object pooling
    GameObject[] instantiatedPlatforms = new GameObject[ 150 ];
    GameObject player = null;
    
    /////////////////////////////////////////////////////////////

    Vector3 position = new Vector3( 2.5f, -3.0f, 1.5f );
    
    float posResult = 0;
    
    int currentTile = 0;
    int lastSpawnedCount = 1;
    int sizeFixerX = 2;
    int sizeFixerZ = 2;

    /////////////////////////////////////////////////////////////

    void Start ()
    {
        player = GameObject.Find("Player");
        // adds all platforms found under Platforms object
		foreach(Transform trans in GameObject.Find("Platforms").transform)
        {
            platformList.Add( trans.gameObject );
        }

        for (int i = 0; i < instantiatedPlatforms.Length; i++)
        {
            // add in random platforms from platform list
            instantiatedPlatforms[ i ] = Instantiate( platformList[ Random.Range( 0, platformList.Count ) ] );
        }
	}
    
    /////////////////////////////////////////////////////////////

    void Update () {
        // if player is near enough map will reposition platforms
        if ( Vector3.Distance( transform.position, player.transform.position ) < 25 )
        {
            MakeMap();
        }
	}
    
    /////////////////////////////////////////////////////////////
    
    void MakeMap()
    {
        //object pool is 100, cant go over it
        if ( currentTile >= instantiatedPlatforms.Length )
        {
            currentTile = 0;
        }

        /////////////////////////////////////////////////////////////
        // CHECK IF THE PLATFORM SHOULD SPAWN
        /////////////////////////////////////////////////////////////

        float spawnResult = Random.Range(0, 1.0f);
        if( spawnResult < instantiatedPlatforms[ currentTile ].GetComponent<PlatformScript>().spawnChance )
        {
            //posResult decides if platform go left or right
            //tiles are placed perfectly with localscale of current and previous tile

            /////////////////////////////////////////////////////////////
            
            if ( sizeFixerX == 2 && sizeFixerZ == 2)
            {
                posResult = Random.Range(0, 1.0f);
            }

            if (posResult < 0.5f)
            {
                if(currentTile - lastSpawnedCount < 0)
                {
                    position = new Vector3( position.x, position.y, position.z + instantiatedPlatforms[ currentTile ].transform.localScale.z / 2 + instantiatedPlatforms[ instantiatedPlatforms.Length - lastSpawnedCount ].transform.localScale.z / 2 );
                }
                else
                {
                    position = new Vector3( position.x, position.y, position.z + instantiatedPlatforms[ currentTile ].transform.localScale.z / 2 + instantiatedPlatforms[ currentTile - lastSpawnedCount ].transform.localScale.z / 2 );
                }
            }
            else if (posResult > 0.5f)
            {
                if (currentTile - lastSpawnedCount < 0)
                {
                    position = new Vector3( position.x + instantiatedPlatforms[ currentTile ].transform.localScale.x / 2 + instantiatedPlatforms[ instantiatedPlatforms.Length - lastSpawnedCount ].transform.localScale.x / 2, position.y, position.z );
                }
                else
                {
                    position = new Vector3( position.x + instantiatedPlatforms[ currentTile ].transform.localScale.x / 2 + instantiatedPlatforms[ currentTile - lastSpawnedCount ].transform.localScale.x / 2, position.y, position.z );
                }
            }
            
            /////////////////////////////////////////////////////////////

            //fix spawned platform position
            transform.position = position;
            instantiatedPlatforms[ currentTile ].transform.position = position;
            instantiatedPlatforms[ currentTile ].transform.rotation = Quaternion.identity;
            instantiatedPlatforms[ currentTile ].GetComponent<Rigidbody>().isKinematic = true;
            
            //sizefixer fixes clipping platforms
            //it increases mininmum space between big platform
            
            // 2020 note: wtf is this
            if(sizeFixerX > 2 )
            {
                sizeFixerX--;
            }
            if (sizeFixerZ > 2 )
            {
                sizeFixerZ--;
            }

            if ( instantiatedPlatforms[ currentTile ].transform.localScale.x > 2 )
            {
                sizeFixerX = (int)instantiatedPlatforms[ currentTile ].transform.localScale.x + 1;
            }
            if ( instantiatedPlatforms[ currentTile ].transform.localScale.z > 2 )
            {
                sizeFixerX = (int)instantiatedPlatforms[ currentTile ].transform.localScale.x + 1;
            }

            currentTile++;
            lastSpawnedCount = 1;
        }

        /////////////////////////////////////////////////////////////
        // PLATFORM DID NOT SPAWN
        /////////////////////////////////////////////////////////////
        
        else
        {
            lastSpawnedCount++;
            currentTile++;
        }
        
        /////////////////////////////////////////////////////////////
    }

    /////////////////////////////////////////////////////////////
}
