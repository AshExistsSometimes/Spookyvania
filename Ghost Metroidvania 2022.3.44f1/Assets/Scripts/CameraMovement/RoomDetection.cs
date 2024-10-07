using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetection : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            CameraController controller = Camera.main.transform.GetComponent<CameraController>();
            if(controller.CurrentRoom != transform)// If controller isnt in current room
            {
                controller.MoveToNewRoom(transform.position, GetComponent<BoxCollider2D>().size);// send to correct room
            }
        }
    }
}
