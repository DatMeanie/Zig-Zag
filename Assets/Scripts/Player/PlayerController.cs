using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    /////////////////////////////////////////////////////////////
    // PUBLIC VARIABLES
    /////////////////////////////////////////////////////////////

    public int movementSpeed = 10;

    [SerializeField] int maxSpeed = 0;
    [SerializeField] float increaseSpeedTime = 0;

    [SerializeField] Animator speedText = null;

    /////////////////////////////////////////////////////////////
    // PRIVATE VARIABLES
    /////////////////////////////////////////////////////////////
    
    Rigidbody rb = null;

    bool moveRight = false;
    bool isPlaying = false;

    /////////////////////////////////////////////////////////////
    //
    //              INITIALIZE & UPDATE
    //
    /////////////////////////////////////////////////////////////

    void Start () {
        
        maxSpeed = movementSpeed + 10;
        rb = GetComponent<Rigidbody>();
	}
    
    /////////////////////////////////////////////////////////////

    void Update()
    {
        /////////////////////////////////////////////////////////////
        // APPLY SPEED TO PLAYER
        /////////////////////////////////////////////////////////////

        if ( isPlaying == true )
        {
            if ( moveRight == false )
            {
                rb.velocity = new Vector3( movementSpeed, rb.velocity.y, 0 );
            }
            else if ( moveRight == true )
            {
                rb.velocity = new Vector3( 0, rb.velocity.y, movementSpeed );
            }
        }

        /////////////////////////////////////////////////////////////
        // CHECK FOR INPUT
        /////////////////////////////////////////////////////////////

        if ( Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor )
            InputPC();
        else if ( Application.platform == RuntimePlatform.Android )
            InputAndroid();
        
        /////////////////////////////////////////////////////////////
    }

    /////////////////////////////////////////////////////////////
    //
    //                  INPUT FUNTIONS
    //
    /////////////////////////////////////////////////////////////

    void InputPC()
    {
        /////////////////////////////////////////////////////////////
        // SWITCH DIRECTION
        /////////////////////////////////////////////////////////////

        if ( Input.GetMouseButtonDown( 0 ) == true )
        {
            if ( isPlaying == false )
                StartCoroutine( IncreaseSpeed() );

            isPlaying = true;
            moveRight = !moveRight;
        }
        
        /////////////////////////////////////////////////////////////
    }

    /////////////////////////////////////////////////////////////

    void InputAndroid()
    {
        // Can't remember how this worked. lol
        foreach ( Touch t in Input.touches )
        {
            if ( t.phase == TouchPhase.Began )
            {
                isPlaying = true;
                moveRight = !moveRight;
            }
        }
    }

    /////////////////////////////////////////////////////////////
    //
    //              ENUMERATORS
    //
    /////////////////////////////////////////////////////////////

    IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds( increaseSpeedTime );
        movementSpeed++;
        speedText.Play( "SpeedUp" );
        StartCoroutine( IncreaseSpeed() );
    }

    /////////////////////////////////////////////////////////////
}
