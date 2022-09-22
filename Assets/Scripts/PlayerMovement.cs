using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    
    private bool isGrounded;

    public string characterName;

    public float movementSpeed = 2f;
    private float defaultMovementSpeed;
    float moveDirection = 0f;
    bool isJumpPressed = false;
    public float jumpForce = 10f;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    bool isFacingLeft = false;
    
    private Vector3 velocity;
    public float smoothTime = 0.2f;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    public void Start()
    {
        defaultMovementSpeed = movementSpeed;
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            isJumpPressed = true;
            animator.SetTrigger("Jump");
            audioSource.PlayOneShot(audioClip);
        }

        //animator.SetBool("Idle", isGrounded);
        animator.SetFloat("Walk", Mathf.Abs(moveDirection));
    }

    //FixedUpdate 
    private void FixedUpdate()
    {
        //GroundCheck();
        Vector3 calculatedMovement = Vector3.zero;
        float verticalVelocity = 0f;

        //if (isGrounded == false)
        {
            verticalVelocity = rigidBody2D.velocity.y;
        }
        calculatedMovement.x = movementSpeed * 100f * moveDirection * Time.fixedDeltaTime;
        calculatedMovement.y = verticalVelocity;
        Move(calculatedMovement, isJumpPressed);
        isJumpPressed = false;
    }

    //private bool isGrounded()
    //{
    //    RaycastHit2D raycasrHit = Physics2D.BoxCast(boxo);
     //   return false;
   // }
    private void Move(Vector3 moveDirection, bool isJumpPressed)
    {
        rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, moveDirection, ref velocity, smoothTime);

        if (isJumpPressed == true && isGrounded == true)
        {
            rigidBody2D.AddForce(new Vector2(0f, jumpForce * 100f));
        }

        //Change this code in a later moment so it also moves the players gameObject.
        if (moveDirection.x > 0f && isFacingLeft == true)
        {
            FlipSpriteDirection();
        }
        else if (moveDirection.x < 0f && isFacingLeft == false)
        {
            FlipSpriteDirection();
        }
    }
    private void FlipSpriteDirection()
    {
        spriteRenderer.flipX = !isFacingLeft;
        isFacingLeft = !isFacingLeft;
    }
    public bool IsFalling() {
        if (rigidBody2D.velocity.y < 0f)
        {
            return true;
        }
        return false;
    }

    public void ResetMovementSpeed()
    {
        movementSpeed = defaultMovementSpeed;
    }

    public void SetNewMovementSpeed(float multiplyBy)
    {
        movementSpeed *= multiplyBy;
    }
}
//WARNING! Delete this from final product.

//PLAYER
//Player will be able to gain HP back (+1 to healthPoints)

