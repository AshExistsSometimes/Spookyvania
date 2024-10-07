using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandShown : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerMovement.HoldingItem())
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    } 
}
