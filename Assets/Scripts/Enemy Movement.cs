using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // Horizontal movement speed
          
    public Rigidbody2D rb;              // Reference to Rigidbody2D component
    private float horizontalInput;
    public float loop = 5f;
    public float loop2 = 5f;






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = loop;
        // Input.GetAxisRaw("Horizontal");
       


        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
    }

    void FixedUpdate()
    {
        // Apply  movement 
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
      
    }
}
