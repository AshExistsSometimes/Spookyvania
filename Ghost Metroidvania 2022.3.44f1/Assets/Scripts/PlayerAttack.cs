using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public Projectile projectile;

    // Fields ///////////////////////////////////////////////////////

    [Header ("Cooldown")]
    [SerializeField] public float AttackCooldown = 0.5f;
    public float cooldownTimer = Mathf.Infinity;

    [Header("Misc")]
    [SerializeField] private Transform ProjStartLocation;
    [SerializeField] private GameObject[] PlateProjectile;

    /////////////////////////////////////////////////////////////////

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        projectile = GetComponent<Projectile>();
    }
    private void Update()
    {
     if(Input.GetMouseButton(0) && cooldownTimer > AttackCooldown && playerMovement.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;

    }

    private void Attack()
    {
        cooldownTimer = 0;
        playerMovement.PlateThrown = true;

        PlateProjectile[0].transform.position = ProjStartLocation.position; // Move Plate from holder to player
        PlateProjectile[0].GetComponent<Projectile>().InitProjectile(Mathf.Sign(transform.localScale.x)); // Move Plate from holder to player
    }
    

}


