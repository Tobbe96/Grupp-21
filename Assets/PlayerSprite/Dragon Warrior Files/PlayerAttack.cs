using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipShoot;

    private Animator anim;
    private RealPlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
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
        audioSource.PlayOneShot(audioClipShoot);
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = FirePoint.position;
        //fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        if(playerMovement.isFacingRight == true)
        {
            fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(1f);
        }
        else
        {
            fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(-1f);
        }
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}


