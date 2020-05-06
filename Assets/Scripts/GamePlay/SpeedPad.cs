using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPad : MonoBehaviour {

    //play enter pad, player change speed
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            StartCoroutine(Speed(other.gameObject));
        }
    }
    IEnumerator Speed(GameObject go)
    {
        go.GetComponent<PlayerController>().movementSpeed += 2;
        yield return new WaitForSeconds(1);
        go.GetComponent<PlayerController>().movementSpeed -= 2;
    }
}
