using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    //Player goes either left or right with certain speed
    //Speed increase of time with time.deltaTime

    //values
    Rigidbody rb;
    public int movementSpeed = 10;
    bool moveRight = false;
    bool playing = false;
    float timer = 0;
    int maxSpeed;
    bool noMoreSpeed = false; 
	void Start () {
        maxSpeed = movementSpeed + 10;
        rb = GetComponent<Rigidbody>();
	}

	void Update()
    {
        ChangeDirection();
        if(timer >= 10 && movementSpeed != maxSpeed && !noMoreSpeed)
        {
            movementSpeed++;
            timer = 0;
            GameObject.Find("SpeedText").GetComponent<Animator>().Play("SpeedUp");
            if(movementSpeed == maxSpeed)
            {
                noMoreSpeed = true;
            }
        }
        //player has clicked
        if (playing)
        {
            timer += Time.deltaTime;
            if (!moveRight)
            {
                rb.velocity = new Vector3(1 * movementSpeed, rb.velocity.y, 0);
            }
            else if (moveRight)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 1 * movementSpeed);
            }
        }
    }

    void ChangeDirection()
    {
        if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                playing = true;
                if (moveRight)
                {
                    moveRight = false;
                }
                else if (!moveRight)
                {
                    moveRight = true;
                }
            }
        }
        else
        {
            foreach (Touch t in Input.touches)
            {
                if (t.phase == TouchPhase.Began)
                {
                    playing = true;
                    if (moveRight)
                    {
                        moveRight = false;
                    }
                    else if (!moveRight)
                    {
                        moveRight = true;
                    }
                }
            }
        }
    }
}
