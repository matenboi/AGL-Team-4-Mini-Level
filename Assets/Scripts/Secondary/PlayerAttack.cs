using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Timer = Unity.VisualScripting.Timer;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bulletPrefab;
    private Animator animator;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // if (Input.GetKey(KeyCode.O) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        // {
        //     Shoot();
        // }
        
        cooldownTimer += Time.deltaTime;
    }

    private bool isShooting()
    {
        return cooldownTimer > attackCooldown;
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");
        cooldownTimer = 0;
        
        bulletPrefab[findShot()].transform.position = firePoint.position;
        bulletPrefab[findShot()].GetComponent<Projectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }
    
    private int findShot()
    {
        for (int i = 0; i < bulletPrefab.Length; i++)
        {
            if(!(bulletPrefab[i].activeInHierarchy))
                return i;
        }

        return 0;
    }
    
}
