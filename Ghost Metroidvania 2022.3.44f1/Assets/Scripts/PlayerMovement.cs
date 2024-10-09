using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Fields ///////////////////////////////////////////////////////

    [Header("Basic Movement")]
    public float MoveSpeed;
    public float JumpForce;

    [Header("Flight")]
    public bool FlightEnabled = false;
    public float FlightForce;
    public float FlightDuration = 5;
    public float GlideRate = 0.9f;
    public float CounterRate = 0.8f;

    [Header("Items")]
    public bool PlateEquipped = false;
    public bool PlateThrown = false;

    [Header("Tweakable")]
    public float GroundCheckDist;

    [Header("Layers")]
    public LayerMask groundLayer;

    [Header("UI")]
    public Image FlightMeter;

    [Header("Debug")]
    public bool PlayerIsGrounded;

    /////////////////////////////////////////////////////////////////

    // Private Values
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private Rigidbody2D rb;
    private bool resetVertVelocity = false;
    private CameraController camController;

    private float flightTime;

    private void Awake()
    {
        //Grab references for rigidbody and animator from player
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        camController = Camera.main.transform.GetComponent<CameraController>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // Get Horizontal Input

        // Left / Right Flipping when moving
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(5, 5, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-5, 5, 1);

        // Player Movement
        rb.velocity = new Vector2(horizontalInput * MoveSpeed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Flight
        if (!Input.GetKey(KeyCode.Space) && !isGrounded())
        {
            FlightEnabled = true;
        }
        else if (isGrounded())
        {
            FlightEnabled = false;
            resetVertVelocity = false;
        }

        if (Input.GetKey(KeyCode.Space) && FlightEnabled && !resetVertVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, FlightForce * Time.deltaTime);
            resetVertVelocity = true;
        }
        
        if (flightTime >= 0 && Input.GetKey(KeyCode.Space) && FlightEnabled)
        {
            flightTime -= Time.deltaTime;
        }



        if (isGrounded())
        {
            flightTime = FlightDuration;
        }



        // Flight Controls
        if (Input.GetKey(KeyCode.Space) && flightTime > 0 && FlightEnabled)
        {
            rb.velocity = new Vector2(rb.velocity.x, ((((Vector2)transform.up * FlightForce)) * CounterRate).y); // Flight
        }
        if (Input.GetKey(KeyCode.Space) && FlightEnabled && flightTime <= 0)
        {
            if(rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, GlideRate); // Glide //  (transform.up * (Mathf.Abs(rb.velocity.y) * GlideRate)).y
            }        
        }


        FlightMeter.fillAmount = (flightTime / FlightDuration);


        // Debug Code
        if (isGrounded())
        {
            PlayerIsGrounded = true;
        }
        else
        {
            PlayerIsGrounded = false;
        }
    }

    // Jump :3
    private void Jump()
    {
        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }
    }

    // Ground Check
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, GroundCheckDist, groundLayer);
        return raycastHit.collider != null;
    }

    // Plate Boomerang
    public bool canAttack()
    {
        return PlateEquipped == true && PlateThrown == false;
    }

    public bool HoldingItem()
    {
        return PlateEquipped == true;
    }
}
