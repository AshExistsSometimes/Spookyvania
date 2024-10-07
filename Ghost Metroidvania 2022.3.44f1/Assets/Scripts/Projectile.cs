using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Fields ///////////////////////////////////////////////////////

    [Header("Stats")]
    public float ProjectileSpeed;
    public float ProjectileMaxDistance;

    [Header("Public references")]
    public Transform PlayerTransform;
    public PlayerMovement playerMovement;

    /////////////////////////////////////////////////////////////////

    private float direction;
    private bool hit;

    public bool plateReturned = false;

    private Rigidbody2D rb;

    private bool returnToPlayer = false;

    private Vector3 targetPoint; // Set Distance
    private Vector3 __direction;



    private BoxCollider2D ProjectileCollider;

    private void Awake()
    {
        ProjectileCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Deactivate();
    }

    private void Update()
    {
        // ROTATION WHILE MOVING
        if (__direction != Vector3.zero)
        {
            float MovementAngle = Mathf.Atan2(__direction.x < 0 ? -(__direction.y) : __direction.y, __direction.x < 0 ? -(__direction.x) : __direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(MovementAngle, Vector3.forward);
        }
        
        // DEACTIVATION ONCE RETURNED
        if (returnToPlayer && Vector3.Distance(transform.position, PlayerTransform.position) < 0.5f)
        {
            playerMovement.PlateThrown = false;
            Deactivate();
        }

        if (hit)
        {
            return;
        }

        if (returnToPlayer)
        {
            targetPoint = PlayerTransform.position;
            __direction = (targetPoint - transform.position).normalized;
        }

        // Speed and Movement
        // float movementSpeed = ProjectileSpeed * Time.deltaTime * direction;
        rb.velocity = (((Vector2)(__direction * ProjectileSpeed)));

        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            ReturnToPlayer();
        }
    }

    //#region OnTriggerEnter
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    ReturnToPlayer();
    //}
    //#endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ReturnToPlayer();
        collision.transform.GetComponent<DestructibleBox>()?.DestroyBox();
    }

    public void InitProjectile(float _direction)// begin throw
    {
        gameObject.SetActive(true);

        plateReturned = false;

        returnToPlayer = false;

        rb.velocity = Vector2.zero;

        direction = _direction;


        hit = false;

        ProjectileCollider.enabled = true;

        float localScaleX = transform.localScale.x;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

        targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition); // GET CURSOR LOCATION
        targetPoint.z = 0;

        __direction = (targetPoint - transform.position).normalized;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void ReturnToPlayer()
    {
        returnToPlayer = true;
        __direction = (targetPoint - transform.position).normalized;
    }

    public bool PlateReturned()
    {
        return plateReturned = true;
    }
}
