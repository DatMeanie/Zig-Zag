using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour {

    //Object-pool based map generation with platform spawn chance

    //startPos, tested around in editor
    Vector3 position = new Vector3(2.5f, -3, 1.5f);
    //what platforms should be implemted in game
    List<GameObject> platformList = new List<GameObject>();
    //array used for object pooling
    GameObject[] goArray = new GameObject[100];
    int currentTile = 0;
    int lastSpawnedCount = 1;
    int sizeFixerX = 2;
    int sizeFixerZ = 2;
    float posResult = 0;
    GameObject player;
	void Start () {
        player = GameObject.Find("Player");
        //adds all platforms found under Platforms object
		foreach(Transform trans in GameObject.Find("Platforms").transform)
        {
            platformList.Add(trans.gameObject);
        }
        for (int i = 0; i < goArray.Length; i++)
        {
            //add in random platforms from platform list
            goArray[i] = Instantiate(platformList[Random.Range(0, platformList.Count)]);
        }
	}
	
	void Update () {
        //if player is near enough map will reposition platforms
        if (Vector3.Distance(transform.position, player.transform.position) < 22 )
        {
            MakeMap();
        }
	}
    void MakeMap()
    {
        //object pool is 100, cant go over it
        if (currentTile == 100)
        {
            currentTile = 0;
        }
        //platforms have spawn chance stored in platform script, if result is < spawnchance its spawns
        float spawnResult = Random.Range(0, 1.0f);
        if(spawnResult < goArray[currentTile].GetComponent<PlatformScript>().spawnChance)
        {
            //posResult decides if platform go left or right
            //tiles are placed perfectly with localscale of current and previous tile

            if (sizeFixerX == 2 && sizeFixerZ == 2)
            {
                posResult = Random.Range(0, 1.0f);
            }
            if (posResult < 0.5f)
            {
                if(currentTile - lastSpawnedCount < 0)
                {
                position = new Vector3(position.x, position.y, position.z + goArray[currentTile].transform.localScale.z / 2 + goArray[100 - lastSpawnedCount].transform.localScale.z / 2);
                }
                else
                {
                    position = new Vector3(position.x, position.y, position.z + goArray[currentTile].transform.localScale.z / 2 + goArray[currentTile - lastSpawnedCount].transform.localScale.z / 2);
                }
            }
            else if (posResult > 0.5f)
            {
                if (currentTile - lastSpawnedCount < 0)
                {
                    position = new Vector3(position.x + goArray[currentTile].transform.localScale.x / 2 + goArray[100 - lastSpawnedCount].transform.localScale.x / 2, position.y, position.z);
                }
                else
                {
                    position = new Vector3(position.x + goArray[currentTile].transform.localScale.x / 2 + goArray[currentTile - lastSpawnedCount].transform.localScale.x / 2, position.y, position.z);
                }
            }
            //fix spawned platform position
            transform.position = position;
            goArray[currentTile].transform.position = position;
            goArray[currentTile].transform.rotation = Quaternion.identity;
            goArray[currentTile].GetComponent<Rigidbody>().isKinematic = true;
            //sizefixer fixes clipping platforms
            //it increases mininmum space between big platform
            if(sizeFixerX > 2)
            {
                sizeFixerX--;
            }
            if (sizeFixerZ > 2)
            {
                sizeFixerZ--;
            }
            if (goArray[currentTile].transform.localScale.x > 2)
            {
                sizeFixerX = (int)goArray[currentTile].transform.localScale.x + 1;
            }
            if (goArray[currentTile].transform.localScale.z > 2)
            {
                sizeFixerX = (int)goArray[currentTile].transform.localScale.x + 1;
            }
            currentTile++;
            lastSpawnedCount = 1;
        }
        //platform did not spawn
        else
        {
            lastSpawnedCount++;
            currentTile++;
        }
    }
}
