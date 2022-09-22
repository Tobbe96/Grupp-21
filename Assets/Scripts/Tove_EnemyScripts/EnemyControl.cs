using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyControl : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    private bool isAlive = true;
    public GameObject player;
    private Animator animator;
    
    [Header("Moving")]
    [SerializeField] private bool canMove;
    [SerializeField] private float moveSpeed = 2f;
    public float movementDirection = 1f;
    public GameObject groundCheckPoint;
    

    [Header("Chasing")]
    [SerializeField] private bool canChase;
    [SerializeField] private float chaseSpeedMultiplier = 1.2f;
    [SerializeField] private float chaseDistance;

    
    [Header("Shooting")]
    [SerializeField] private bool canShoot;
    [SerializeField] public GameObject bulletPrefab;
    private float shotCooldown = 1f;
    [SerializeField] private int projectileAmount = 6;
    public float projectileRadius = 360f;

    
    [Header("Jumping")] 
    [SerializeField] private bool canJump;
    [SerializeField] private float jumpHeight = 1;
    [SerializeField] private GameObject groundCheck;
    
    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }


    private void Update()
    {
    }

    void FixedUpdate()
    {
            SetAnimator();
        
        //are we alive? Return if not!
        if (!isAlive)
        {
            return;
        }


        if (!IsGrounded())
        {
            ChangeDirection();
            return;
        }

        if (canChase && IsPlayerInRange() && Mathf.Abs(player.transform.position.y - transform.position.y) < 2)
        {
            ChasePlayer();
            return;
        }

        if (canShoot && IsPlayerInRange())
        {
            StartCoroutine(Shoot());
            
            return;
        }
            
        if (canJump && IsGrounded())
        {
            Jump();

        }
        
        if (canMove)
        {
            Move();
        }
    }

    private void SetAnimator()
    {
        animator.SetBool("IsWalking", canMove);
        animator.SetBool("IsAttacking", !canShoot);
        animator.SetBool("IsAlive", isAlive);


    }

    private void LateUpdate()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 3)
        {
            ChangeDirection();
        }    
    }

    /// <summary>
    /// Move in movement moveDirection*moveSpeed
    /// </summary>
    private void Move()
    {
        Vector3 newPosition = gameObject.transform.position;
        float moveVectorX = (moveSpeed * Time.fixedDeltaTime) * movementDirection;
        newPosition.x += moveVectorX;
        rigidBody2D.MovePosition(newPosition);

    }
    
    /// <summary>
    /// Returns true if grounded, false if not
    /// </summary>
    public bool IsGrounded() 
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPoint.transform.position, 0.2f);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject && colliders[i].gameObject.layer == 3)
            {
                return true;
            }
        }
        return false;
    }
    private void Jump()
    {
rigidBody2D.AddForce(new Vector2(2, jumpHeight), ForceMode2D.Impulse);        
        //Do ajimppyjumppy dumppy
        //do not turn if you are in the air!
    }


    /// <summary>
    /// Changes moveDirection and localscale around
    /// </summary>
    private void ChangeDirection()
    {
        //do not change direction when airbourne
            movementDirection *= -1;
            Vector3 newScale = gameObject.transform.localScale;
            newScale.x = movementDirection;
            transform.localScale = newScale;
    }
    
/// <summary>
/// Returns true if player is in range
/// </summary>
    private bool IsPlayerInRange()
    {
        if (DistanceToPlayer() <= chaseDistance)
        {
            return true;
        }

        return false;
    }

    private float DistanceToPlayer()
    {
        return Vector2.Distance(player.transform.position, transform.position);
    }

/// <summary>
/// Move towards player at chaseSpeed
/// </summary>
    private void ChasePlayer()
{
        Vector3 newPosition = gameObject.transform.position;
        float moveDir = (player.transform.position.x - transform.position.x) >= 0 ? 1 : -1;

        // if ((player.transform.position.x - transform.position.x) >= 0)
        // {
        //     moveDir = 1;
        // }
        // else
        // {
        //     moveDir = -1;
        // }
        newPosition.x += (moveSpeed * chaseSpeedMultiplier * Time.fixedDeltaTime) * moveDir;
        rigidBody2D.MovePosition(newPosition);
        
        //face player when chasing
        //om vi går åt höger och inte tittar åt höger
        if (moveDir > 0 && transform.localScale.x < 0)
        {
            ChangeDirection();
        }
        // om vi går åt vänster och inte tittar åt vänster
        else if (moveDir < 0 && transform.localScale.x > 0)
        {
            ChangeDirection();
        }
        
    }

//offset projectiles 6/180
private bool isOffset = false;
/// <summary>
/// Shoot projectile
/// </summary>
IEnumerator Shoot()
{
    StartCoroutine(ShootCooldown());
    animator.SetBool("IsAttacking", true);
    //Enemy stops to shoot
    //builddup 
    float animationLength = animator.GetCurrentAnimatorClipInfo(0).Length;
    yield return new WaitForSeconds(animationLength);

    if (isOffset)
    {
        projectileAmount -= 1;
    } 
    
    for (int i = 0; i < projectileAmount; i++)
    {
        float offset = projectileRadius / projectileAmount;
        float angle = i * offset;
        if (isOffset)
        {
            angle += offset * 0.5f;
        }
        Instantiate(bulletPrefab, transform.position,
            Quaternion.Euler(0, 0, angle));
    }
    if (isOffset)
    {
        projectileAmount += 1;
    }
    isOffset = !isOffset;
}

IEnumerator ShootCooldown()
{
    rigidBody2D.velocity = Vector2.zero;
    canMove = false;
    canShoot = false;
    yield return new WaitForSeconds(shotCooldown);
    canMove = true;
    canShoot = true;

}

    

    /// <summary>
/// Death animation and destroy after 5s delay
/// </summary>
    public void KillMe()
    {
        isAlive = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Vector2 killForce = new Vector2(0f, 4f);
        rigidBody2D.AddForce(killForce, ForceMode2D.Impulse);
        Vector3 localScale = transform.localScale;
        localScale = new Vector3(localScale.x, -localScale.y);
        Destroy(gameObject, 5f);
    }
}

/*
 bool canshoot
 bool canjump
 bool canchase
 
 Groundcehck
 patrol
 move
 changedirection
 killme
 
 seekplayer
 chaseplayer
 jump
 shoot
 

*/