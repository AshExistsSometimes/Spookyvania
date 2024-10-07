using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPlate : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
    spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerMovement.PlateThrown == true || playerMovement.PlateEquipped == false)
        {
            spriteRenderer.enabled = false;
        }
        else if (playerMovement.PlateThrown == false || playerMovement.PlateEquipped == true)
        {
            spriteRenderer.enabled = true;
        }
    }
}
