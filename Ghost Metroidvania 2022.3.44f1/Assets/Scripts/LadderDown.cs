using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LadderDown : MonoBehaviour
{
    public Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }
    private void Update()
    {
       if(Input.GetAxis("Vertical") == -1)
        {
            collider.enabled = false;
        }
       else
        {
            collider.enabled = true;
        }
    }
}
