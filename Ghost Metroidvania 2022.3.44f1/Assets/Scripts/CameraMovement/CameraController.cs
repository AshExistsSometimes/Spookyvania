using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    // Fields ///////////////////////////////////////////////////////

    [Header("Movement")]
    public float Speed;

    [Header("References")]
    public Transform Player;

    [Header("Offset")]
    public Vector3 CamOffset;

    [HideInInspector]
    public Transform CurrentRoom;

    [HideInInspector]
    public bool CameraMoving = false;

    /////////////////////////////////////////////////////////////////

    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    /////
    
    Vector3 point;
    Vector2 size;

    Vector2 TopLeft;
    Vector2 BottomRight;

    Vector2 CamTopLeft;
    Vector2 CamBottomRight;
    Vector3 CameraSize;


    private void Update()
    {
        if (CameraMoving) return;

        Vector3 coords = PlayerCoords(transform.position.z); // tracks player for camera move

        FindCameraBounds();
        transform.position = GetClosestPointWithBounds(coords);
    }


    private Vector3 PlayerCoords(float zAxis)
    {
        return new Vector3(Player.position.x + CamOffset.x, Player.position.y + CamOffset.y, zAxis);
    }

    private Vector3 GetClosestPointWithBounds(Vector3 position)
    {
        // Local
        Vector3 returnedValue = Vector3.zero;

        if (position.x - (CameraSize.x / 2f) < TopLeft.x)// Exceeds Left bounds (x)
        {
            returnedValue.x = TopLeft.x + (CameraSize.x / 2f);// = new Vector3(TopLeft.x + (CameraSize.x / 2f), position.y, position.z);
        }
        //

        if (position.x + (CameraSize.x / 2) > BottomRight.x)// Exceeds Right bounds (x)
        {
            returnedValue.x = BottomRight.x - (CameraSize.x / 2f);//= new Vector3(BottomRight.x - (CameraSize.x / 2f), position.y, position.z);
        }
        //

        if (position.y + CameraSize.y / 2 > TopLeft.y)// Exceeds Top bounds (y)
        {
            returnedValue.y = TopLeft.y - (CameraSize.y / 2f); //new Vector3(position.x, TopLeft.y - (CameraSize.y / 2f), position.z);
        }
        //

        if (position.y - CameraSize.y / 2 < BottomRight.y)// Exceeds Bottom bounds (y)
        {
            returnedValue.y = BottomRight.y + (CameraSize.y / 2f); // = new Vector3(position.x, BottomRight.y + (CameraSize.y / 2f), position.z);
        }
        //
        returnedValue.z = position.z;

        return returnedValue;
    }

    public void MoveToNewRoom(Transform roomTransform, Vector2 roomSize)
    {
        if (CameraMoving)
        {
            return;
        }
            
            CameraMoving = true;

        CurrentRoom = roomTransform;

        FindRoomBounds(roomTransform.position, roomSize);
        FindCameraBounds();

        Vector3 targetPoint = PlayerCoords(transform.position.z);

        targetPoint = GetClosestPointWithBounds(targetPoint);

        // Smoothly move camera over set time
        StartCoroutine(MoveCameraToNewLocationSmoothly(transform.position, targetPoint));

        //transform.position = new Vector3(roomPosition.x, roomPosition.y, transform.position.z);
    }

    private void FindRoomBounds(Vector3 position, Vector2 size)
    {
        TopLeft = new Vector2( position.x - (size.x / 2), position.y + (size.y / 2) );
        BottomRight = new Vector2( position.x + (size.x / 2), position.y - (size.y / 2) );
    }

    public void FindCameraBounds()
    {
        CamTopLeft = Camera.main.ScreenToWorldPoint( new Vector3(0, Screen.height, 0) );
        CamBottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        CameraSize = new Vector2(CamBottomRight.x - CamTopLeft.x, CamTopLeft.y - CamBottomRight.y);
    }

    IEnumerator MoveCameraToNewLocationSmoothly(Vector3 startLocation, Vector3 endLocation, float speed = 2f)
    {
        float time = 0f;

        Vector3 startPos = startLocation;

        CameraMoving = true;

        while (transform.position != endLocation)
        {
            transform.position = Vector3.Lerp(startPos, endLocation, time);

            time += Time.deltaTime * speed;

            yield return null;
        }

        CameraMoving = false;
    }


}

