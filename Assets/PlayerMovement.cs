using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
}
