using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform fireballPoint;
    [SerializeField] private GameObject[] fireballs;
    private Animator anim;
    private RealPlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<RealPlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetButton("Fire1") && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[FindFIreball()].transform.position = fireballPoint.position;
        fireballs[FindFIreball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFIreball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}


