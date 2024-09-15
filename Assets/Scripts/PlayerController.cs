using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //this is a variable for a rigidbody that is attached to the player.
    private Rigidbody2D playerRigidBody;
    public float movementSpeed;
    public float jumpForce;
    private float inputHorizontal;
    private int maxNumJumps;
    private int numJumps;

    // Start is called before the first frame update
    void Start()
    {
        //I can only get this component using the following line of code
        //becuase the rigidbody2d is attached to the player and this script
        //is also attached to the player. 
        playerRigidBody = GetComponent<Rigidbody2D>();

        maxNumJumps = 1;
        numJumps = 1;

    }

    // Update is called once per frame
    void Update()
    {
        movePlayerLateral();
        jump();
    }

    private void movePlayerLateral()
    {
        //if the player presses a move left, d move right
        //"Horizontal" is defined in the input section of the project settings
        //the line below will return:
        //0  - no button pressed
        //1  - right arrow or d pressed
        //-1 - left arrow or a pressed
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        playerRigidBody.velocity = new Vector2(movementSpeed * inputHorizontal, playerRigidBody.velocity.y);

        if(inputHorizontal != 0)
        {
            flipPlayerSprite(inputHorizontal);
        }
    }

    private void flipPlayerSprite(float inputHorizontal)
    {
        if(inputHorizontal > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(inputHorizontal < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && numJumps <= maxNumJumps)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpForce);

            numJumps++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject);
        if(collision.gameObject.CompareTag("Grounded"))
        {
            numJumps = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DoubleJump"))
        {
            maxNumJumps = 2;
        }
        else if (collision.gameObject.CompareTag("OB"))
        {
            //restart the level
            SceneManager.LoadScene("Level01");
        }
    }
}
