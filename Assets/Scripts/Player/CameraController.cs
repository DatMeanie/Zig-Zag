using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //simple camera follow script with Y offset 

    Vector3 offset = new Vector3();
    GameObject player = null;

	void Start () 
    {
        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;
    }

    void Update () 
    {
        Vector3 result = new Vector3( player.transform.position.x + offset.x, transform.position.y, player.transform.position.z + offset.z );
        transform.position = result;
	}
}
