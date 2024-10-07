using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Fields ///////////////////////////////////////////////////////

    [Header("Stats")]
    public float Speed = 1f;
    public float WaitDuration = 0.5f;

    [Header("References")]
    public Transform Platform;
    public Transform StartPoint;
    public Transform EndPoint;

    /////////////////////////////////////////////////////////////////

    private int direction = 1;
    private float LocalTime = 0f;
    private float localSpeed = 0f;
    private float WaitTime = 0f;

    private void Update()
    {
        Vector2 target = currentMovementTarget();

        if (WaitTime <= 0)// if waiting time is over
        {
            if (direction == 1 && LocalTime <= 1)
            {
                LocalTime += Time.deltaTime * Speed;
            }
            else if (direction == 0 && LocalTime >= 0)
            {
                LocalTime -= Time.deltaTime * Speed;
            }
        }
        else// reduces waiting time
        {
            WaitTime -= Time.deltaTime;
        }


        Platform.position = Vector2.Lerp(StartPoint.position, EndPoint.position, LocalTime);

        float distance = Vector2.Distance(Platform.position, target);
        
        if (distance <= 0.1f)
        {
            WaitTime = WaitDuration;
            direction = (direction == 1 ? 0 : 1); // Flips Direction
        }

    }

    Vector2 currentMovementTarget()
    {
        if(direction == 0)
        {
            return StartPoint.position;
        }
        else
        {
            return EndPoint.position;
        }
    }

    // Show Line in editor for debug
    private void OnDrawGizmos()
    {
        if (Platform != null && StartPoint != null && EndPoint != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(Platform.transform.position, StartPoint.position);
            Gizmos.DrawLine(Platform.transform.position, EndPoint.position);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(StartPoint.position, EndPoint.position);
        }
    }
}



