using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D theRB;
    private Animator anim;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask isGrounded;
    [SerializeField] private LayerMask wallLayer;
    

    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration = 50f;
    [SerializeField] private float maxMoveSpeed = 12f;
    [SerializeField] private float groundDeceleration = 10f;
    private float horizontalMovement;
    private bool switchDirection => (theRB.velocity.x > 0f && horizontalMovement < 0f) || (theRB.velocity.x < 0f && horizontalMovement > 0f);
    public bool isFacingRight = true;

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float airDeceleration = 2.5f;
    [SerializeField] private float fallMultiplier = 8f;
    [SerializeField] private float lowJumpFallMultiplier = 5f;
    [SerializeField] private float jumpDelay = 0.15f;
    [SerializeField] private int bonusJumps = 1;
    [SerializeField] private float hangTime = 0.2f;
    [SerializeField] private float jumpBufferLength = 0.1f;
    //håller koll på hur många bonus hopp spelaren har gjort
    private int bonusJumpsValue;
    private float hangTimeReknare;
    private float jumpBufferReknare;
    //private float jumpTimer;

    private bool canJump => jumpBufferReknare > 0f && (hangTimeReknare > 0f || bonusJumpsValue > 0 );

    [Header("Collision Variables Ground")]
    [SerializeField] private float groundedRaycastLength;
    [SerializeField] private Vector3 groundedRaycastOffset;
    private bool onGround;

    [Header("Wall Collisions Variables")]
    [SerializeField] private float wallRaycastLength;
    public bool onWall;
    public bool onRightWall;

    [Header("Corner Correction Variables")]
    [SerializeField] private float topRaycastLength;
    [SerializeField] private Vector3 edgeRaycastOffset;
    [SerializeField] private Vector3 innerRaycastOffset;
    private bool canCornerCorrect;

    private void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
       horizontalMovement = GetInput().x;
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferReknare = jumpBufferLength;
        }
        else
        {
            jumpBufferReknare -= Time.deltaTime;
        }

       

        anim.SetBool("grounded", onGround);
        anim.SetFloat("HorizontalDirection", Mathf.Abs(horizontalMovement));
        if (horizontalMovement < 0f && isFacingRight)
        {
            Flip();
        }
        else if (horizontalMovement > 0f && !isFacingRight)
        {
            Flip();
        }
        if (theRB.velocity.y < 0f)
        {
            //Animation
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }
    }

    private void FixedUpdate()
    {
        CheckCollision();
        MovePlayer();
        
        if (onGround)
        {
            ApplyGroundDeceleration();
            bonusJumpsValue = bonusJumps;
            hangTimeReknare = hangTime;

            //Animation
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }
        else
        {
            ApplyAirDeceleration();
            FallMultiplier();
            hangTimeReknare -= Time.fixedDeltaTime;
        }
        if (canJump) Jump();
        if (canCornerCorrect) CornerCorrect(theRB.velocity.y);
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

        ApplyAirDeceleration();
        theRB.velocity = new Vector2(theRB.velocity.x, 0f);
        theRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        hangTimeReknare = 0f;
        jumpBufferReknare = 0f;

        //Animation
        anim.SetBool("isJumping", true);
        anim.SetBool("isFalling", false);
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

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void CornerCorrect(float yVelocity)
    {
        //Putta spelaren till höger om hörnet
        RaycastHit2D hit = Physics2D.Raycast(transform.position - innerRaycastOffset + Vector3.up * topRaycastLength, Vector3.left, topRaycastLength, isGrounded);
        if(hit.collider != null)
        {
            float newPosition = Vector3.Distance(new Vector3(hit.point.x, transform.position.y, 0) + Vector3.up * topRaycastLength, transform.position - edgeRaycastOffset + Vector3.up * topRaycastLength);
            transform.position = new Vector3(transform.position.x + newPosition, transform.position.y, transform.position.z);
            theRB.velocity = new Vector2(theRB.velocity.x, yVelocity);
            return;
        }

        //Putta spelaren till vänster om hörnet
        hit = Physics2D.Raycast(transform.position + innerRaycastOffset + Vector3.up * topRaycastLength, Vector3.right, topRaycastLength, isGrounded);
        if (hit.collider != null)
        {
            float newPosition = Vector3.Distance(new Vector3(hit.point.x, transform.position.y, 0f) + Vector3.up * topRaycastLength, transform.position + edgeRaycastOffset + Vector3.up * topRaycastLength);
            transform.position = new Vector3(transform.position.x - newPosition, transform.position.y, transform.position.z);
            theRB.velocity = new Vector2(theRB.velocity.x, yVelocity);
        }
    }

    private void CheckCollision()
    {
        //Mark kollisioner
        onGround = Physics2D.Raycast(transform.position + groundedRaycastOffset, Vector2.down, groundedRaycastLength, isGrounded) || Physics2D.Raycast(transform.position - groundedRaycastOffset, Vector2.down, groundedRaycastLength, isGrounded);

        //Kollisioner med hörn och kanter
        canCornerCorrect = Physics2D.Raycast(transform.position + edgeRaycastOffset, Vector2.up, topRaycastLength, isGrounded) &&
            !Physics2D.Raycast(transform.position + innerRaycastOffset, Vector2.up, topRaycastLength, isGrounded) ||
            Physics2D.Raycast(transform.position - edgeRaycastOffset, Vector2.up, topRaycastLength, isGrounded) &&
            !Physics2D.Raycast(transform.position - innerRaycastOffset, Vector2.up, topRaycastLength, isGrounded);

        //Kollisioner med väggar
        onWall = Physics2D.Raycast(transform.position, Vector2.right, wallRaycastLength, wallLayer) ||
            Physics2D.Raycast(transform.position, Vector2.left, wallRaycastLength, wallLayer);
        onRightWall = Physics2D.Raycast(transform.position, Vector2.right, wallRaycastLength, wallLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        //Markens raycast
        Gizmos.DrawLine(transform.position + groundedRaycastOffset, transform.position + groundedRaycastOffset + Vector3.down * groundedRaycastLength);
        Gizmos.DrawLine(transform.position - groundedRaycastOffset, transform.position - groundedRaycastOffset + Vector3.down * groundedRaycastLength);

        //Hörn och kant raycast
        Gizmos.DrawLine(transform.position + edgeRaycastOffset, transform.position + edgeRaycastOffset + Vector3.up * topRaycastLength);
        Gizmos.DrawLine(transform.position - edgeRaycastOffset, transform.position - edgeRaycastOffset + Vector3.up * topRaycastLength);
        Gizmos.DrawLine(transform.position + innerRaycastOffset, transform.position + innerRaycastOffset + Vector3.up * topRaycastLength);
        Gizmos.DrawLine(transform.position - innerRaycastOffset, transform.position - innerRaycastOffset + Vector3.up * topRaycastLength);

        //Koll av hörn distans
        Gizmos.DrawLine(transform.position - innerRaycastOffset + Vector3.up * topRaycastLength,
            transform.position - innerRaycastOffset + Vector3.up * topRaycastLength + Vector3.left * topRaycastLength);
        Gizmos.DrawLine(transform.position + innerRaycastOffset + Vector3.up * topRaycastLength,
            transform.position + innerRaycastOffset + Vector3.up * topRaycastLength + Vector3.right * topRaycastLength);

        //Koll av väggar
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * wallRaycastLength);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * wallRaycastLength);
    }

    public bool canAttack()
    {
        return horizontalMovement == 0 && onGround;
    }
}
