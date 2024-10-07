using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPlate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerMovement>().PlateEquipped = true;
            Destroy(gameObject);
        }
    }
}
