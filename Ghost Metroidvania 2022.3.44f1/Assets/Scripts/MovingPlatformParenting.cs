using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformParenting : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player");
        {
            other.transform.parent = transform;
            other.attachedRigidbody.interpolation = RigidbodyInterpolation2D.None;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Player") ;
        {
            other.transform.parent = null;
            other.attachedRigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
}
