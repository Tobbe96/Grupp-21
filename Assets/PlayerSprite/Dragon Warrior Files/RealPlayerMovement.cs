using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D theRB;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask isGrounded;
    

    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration = 50f;
    [SerializeField] private float maxMoveSpeed = 12f;
    [SerializeField] private float groundDeceleration = 10f;
    private float horizontalMovement;
    private bool switchDirection => (theRB.velocity.x > 0f && horizontalMovement < 0f) || (theRB.velocity.x < 0f && horizontalMovement > 0f);

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float airDeceleration = 2.5f;
    [SerializeField] private float fallMultiplier = 8f;
    [SerializeField] private float lowJumpFallMultiplier = 5f;
    [SerializeField] private float jumpDelay = 0.15f;
    [SerializeField] private int bonusJumps = 1;
    //håller koll på hur många bonus hopp spelaren har gjort
    private int bonusJumpsValue;
    //private float jumpTimer;

    private bool canJump => Input.GetButtonDown("Jump") && (onGround || bonusJumpsValue > 0 );

    [Header("Collision Variables Ground")]
    [SerializeField] private float groundedRaycastLength;
    [SerializeField] private Vector3 groundedRaycastOffset;
    private bool onGround;

    private void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
       horizontalMovement = GetInput().x;
       if (canJump) Jump();
    }

    private void FixedUpdate()
    {
        CheckCollision();
        MovePlayer();
        
        if (onGround)
        {
            bonusJumpsValue = bonusJumps;
            ApplyGroundDeceleration();
        }
        else
        {
            ApplyAirDeceleration();
            FallMultiplier();
        }
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MovePlayer()
    {
        theRB.AddForce(new Vector2(horizontalMovement, 0f) * movementAcceleration);

        if(Mathf.Abs(theRB.velocity.x) > maxMoveSpeed)
        {
            theRB.velocity = new Vector2(Mathf.Sign(theRB.velocity.x) * maxMoveSpeed, theRB.velocity.y);
        }
    }

    private void ApplyGroundDeceleration()
    {
        if (Mathf.Abs(horizontalMovement) < 0.4f || switchDirection)
        {
            theRB.drag = groundDeceleration;
        }
        else
        {
            theRB.drag = 0f;
        }
    }

    private void ApplyAirDeceleration()
    {
        theRB.drag = airDeceleration;
    }

    private void Jump()
    {
        if (!onGround)
        {
            bonusJumpsValue--;
        }

        theRB.velocity = new Vector2(theRB.velocity.x, 0f);
        theRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void FallMultiplier()
    {
        if (theRB.velocity.y < 0)
        {
            theRB.gravityScale = fallMultiplier;
        }
        else if(theRB.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            theRB.gravityScale = lowJumpFallMultiplier;
        }
        else
        {
            theRB.gravityScale = 1f;
        }
    }

    private void CheckCollision()
    {
        onGround = Physics2D.Raycast(transform.position + groundedRaycastOffset, Vector2.down, groundedRaycastLength, isGrounded) || Physics2D.Raycast(transform.position - groundedRaycastOffset, Vector2.down, groundedRaycastLength, isGrounded);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + groundedRaycastOffset, transform.position + groundedRaycastOffset + Vector3.down * groundedRaycastLength);
        Gizmos.DrawLine(transform.position - groundedRaycastOffset, transform.position - groundedRaycastOffset + Vector3.down * groundedRaycastLength);
    }

    public bool canAttack()
    {
        return horizontalMovement == 0 && onGround;
    }
}
