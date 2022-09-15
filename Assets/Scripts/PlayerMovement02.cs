using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement02 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D theRB;
    [SerializeField] private LayerMask groundLayer;

    private BoxCollider2D boxCollider;
    
    
    public Animator anim;
    public SpriteRenderer playerSR;

    public float hangTime = .2f;
    private float hangCounter;

    public float jumpBufferLength = .1f;
    private float jumpBufferCount;

    //public Transform camTarget;
    //public float aheadAmount, aheadSpeed;

    public ParticleSystem footSetps;
    private ParticleSystem.EmissionModule footEmission;
    public ParticleSystem impactEffect;
    private bool wasOnGround;
    
    private void Awake()
    {
        theRB = GetComponent<Rigidbody2D>();
        footEmission = footSetps.emission;
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }
    private void Update()
    {
        //röra sig i x-led
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

        //kolla om player står på marken
        //isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .1f, whatIsGround) || Physics2D.OverlapCircle(groundCheckPoint2.position, .1f, whatIsGround);

        //flippa spelaren/player
        if(horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
        

        wasOnGround = Grounded();

        //animations värden
        
        anim.SetBool("run", horizontalInput != 0); 
        

    }

    private void Jump()
    {
        theRB.velocity = 
    }

    private bool Grounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    
}
