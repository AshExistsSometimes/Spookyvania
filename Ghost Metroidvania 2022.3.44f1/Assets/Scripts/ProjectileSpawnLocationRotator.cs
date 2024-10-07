using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawnLocationRotator : MonoBehaviour
{
    [Header("Public references")]
    public Transform RotationPoint;


    private void Update()
    {
        Vector3 targetPoint = (Camera.main.ScreenToWorldPoint(Input.mousePosition));

        transform.LookAt(targetPoint);
    }
}
