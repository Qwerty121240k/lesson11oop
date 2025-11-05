using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//Stoped at doublejump titlecard 
public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    [Header("movement")]
    public float MoveSpeed = 5f;
    float horizontalMovemont;

    [Header("Jump")]
    public float JumpPower = 10f;
    [Header("Groundcheck")]
    public Transform Groundcheckpos;
    public Vector2 Groundchecksize = new Vector2(0.5f, 0.05f);
    public LayerMask Groundlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontalMovemont * MoveSpeed, rb.velocity.y);
    }
    public void move(InputAction.CallbackContext context)
    {
        horizontalMovemont = context.ReadValue<Vector2>().x;

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded())
        {


            if (context.performed)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpPower);
            }
            else if (context.canceled)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
    }



    private bool isGrounded()
    {
        if (Physics2D.OverlapBox(Groundcheckpos.position, Groundchecksize, 0, Groundlayer))
        {
            return true;
        }
        return false;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(Groundcheckpos.position, Groundchecksize);
    }
}