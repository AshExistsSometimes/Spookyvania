using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    // Fields ///////////////////////////////////////////////////////

    [Header ("Delays")]
    public float FallDelay = 1f;
    public float ReturnDelay = 1f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    /////////////////////////////////////////////////////////////////

    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallAndReturn());
        }
    }

    private IEnumerator FallAndReturn()
    {
        rb.gravityScale = 1f;
        yield return new WaitForSeconds(FallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;// Fall
        yield return new WaitForSeconds(ReturnDelay);
        transform.position = startPosition;// Return to Start Point
        rb.velocity = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
    }
}
