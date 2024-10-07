using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    // Fields ///////////////////////////////////////////////////////

    [Header("Stats")]
    public float ClimbSpeed = 6f;

    [Header("References")]
    public PlayerMovement playerMovement;

    // Private Fields ///////////////////////////////////////////////

    private bool isClimbing;
    private float vertical;
    private bool isTouchingLadder = false;
    private bool isClimbingLadder = false;

    [SerializeField] private Rigidbody2D rb;

    //////////////////////////////////////////////////////////////////
    
    private void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isTouchingLadder && Mathf.Abs(vertical) != 0f)
        {
            isClimbingLadder = true;
        }

        if (isClimbingLadder &&  Mathf.Abs(vertical) > 0.01f  ||  Mathf.Abs(vertical) < -0.01f) // Activates climbing if on ladder and moving vertically
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * ClimbSpeed);
        }
        else if (!isClimbing && isTouchingLadder)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        else if (!isClimbing && !isTouchingLadder)
        {
            rb.gravityScale = 2f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isTouchingLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isTouchingLadder = false;
            isClimbing = false;
        }
    }
}
