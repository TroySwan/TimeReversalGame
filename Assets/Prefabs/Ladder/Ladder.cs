using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float climbSpeed = 4f;

    private bool isPlayerOnLadder = false;
    private bool isPlayerClimbing = false;

    private GameObject player;

    // MARK:- PUBLIC METHODS
    void Start()
    {
        player = GameObject.Find("Ninja");
    }

    void Update()
    {
        if (isPlayerOnLadder)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Climb(climbSpeed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Climb(-climbSpeed);
            }
            else
            {
                if (isPlayerClimbing)
                {
                    isPlayerClimbing = false;
                    player.GetComponent<Rigidbody2D>().gravityScale = 0;
                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                }
            }
        }
    }
    // MARK:- PRIVATE METHODS
    private void Climb(float climbVelocity)
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, climbVelocity);
        player.GetComponent<Animator>().SetBool("Ladder", true);
        isPlayerClimbing = true;
    }

    // MARK:- COLLISION METHODS
    void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.tag == "Player")
        {
            isPlayerOnLadder = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerOnLadder = false;
            player.GetComponent<Animator>().SetBool("Ladder", false);
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
