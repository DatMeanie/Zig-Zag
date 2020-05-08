using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    [Range(0.0f,100.0f)] public float spawnChance = 100.0f;
    public float timer = 0.0f;

    Rigidbody rb = null;

    PlayerController player = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find( "Player" ).GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.name == "Player" )
        {
            StartCoroutine( Fall() );
        }
    }

    IEnumerator Fall()
    {
        float newTimer = ( timer - ( 0.003f * player.movementSpeed ) );
        yield return new WaitForSeconds( Mathf.Clamp( newTimer, 0.006f, 100.0f ) );
        rb.isKinematic = false;
        rb.AddForce( Vector3.down, ForceMode.Impulse );
    }
}
