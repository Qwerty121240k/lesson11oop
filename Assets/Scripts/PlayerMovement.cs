using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // Horizontal movement speed
    public float jumpForce = 10f;       // Force applied when jumping
    public Rigidbody2D rb;              // Reference to Rigidbody2D component

    private float horizontalInput;      // Store horizontal input
    public bool isGrounded;            // Is the player on the ground?
    private bool canDoubleJump;         // Can the player perform a double jump?

    public Transform groundCheck;       // Position to check if grounded
    public float groundCheckRadius = 0.2f; // Radius of ground check circle
    public LayerMask groundLayer;       // Layer considered as ground

    private GameManager gameManager; // Reference to the Game Manager script
    public bool deathState = false; // Set default death state to false

    //Add these variables to your player script
    private bool isFalling = false;
    private float fallStartTime = 0.5f;
    private float fallThreshold = 2.0f; // Adjust this value to set the fall time threshold.

    // The name of the next scene to load
    public string loadSceneName;
    // Add this code to your player script
    public DisplayMessage displayMessage;

    public string showMessage;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        // Get horizontal input (-1 for left, 1 for right)
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Flip the player's sprite to face the direction of movement
        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }

        // Check if the player is on the ground (should happen before jump input)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump input
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                // First jump
                Jump();
                canDoubleJump = true; // Enable double jump
            }
            else if (canDoubleJump)
            {
                // Double jump
                Jump();
                canDoubleJump = false; // Use up double jump
            }
        }

        // Add this code to your update() script
        // Check if the player is falling
        if (IsFalling())
        {
            // The player is falling, start timing the fall.
            if (!isFalling)
            {
                isFalling = true;
                fallStartTime = Time.time;
            }
            // Check if the fall duration has exceeded the threshold
            if (Time.time - fallStartTime >= fallThreshold)
            {
                deathState = true;
            }
        }
        else
        {
            // The player is not falling, reset the fall timer.
            isFalling = false;
        }
    
        if (deathState == true)
        {
            // display win message
            displayMessage.ShowMessage(showMessage);
            // Delay for visual effect (optional)
            Invoke("LoadLevel", 2f);
    }

}

    private void LoadLevel()
    {
        SceneManager.LoadScene(loadSceneName);
    }

    void FixedUpdate()
    {
        // Apply horizontal movement using physics (keeps gravity working properly)
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        // Apply vertical velocity for jumping
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
           // gameManager.coinsCounter += 1;
            Destroy(other.gameObject);
            Debug.Log("Player has collected a coin!");
        }


        if (other.gameObject.tag == "Finish")
        {
            // Game will reload in 3 seconds

            Debug.Log("Finish");
            gameManager.Invoke("EndGame", 3);
        }

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            deathState = true; // Say to GameManager that player is dead
        }
        else
        {
            deathState = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    //lesion 13 fall 

    //Add this method to the player script
    bool IsFalling()
    {
        // You can implement your own logic here to determine if the player is falling.
        // For example, you can check if the player's vertical velocity is negative.
        return GetComponent<Rigidbody2D>().velocity.y < 0;
    }



}
