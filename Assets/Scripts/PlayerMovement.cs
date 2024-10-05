using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;

    [SerializeField] private float wallSlideSpeed = 2f;

    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private float dirX = 0f;
    private enum MovementState { idle, running, jumping, falling }

    private bool playerFacingRight = true;
    private bool touchingStickyWalls;
    private bool isWallJumping;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        

        rb2d.velocity = new Vector2(dirX * moveSpeed, rb2d.velocity.y);
        //rb2d.AddForce(new Vector2(dirX * moveSpeed, 0));

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb2d.velocity = new Vector2(0, jumpForce);
        }

        if (touchingStickyWalls)
        {
            if (Input.GetButtonDown("Jump"))
            {
                PerformWallJump();
               
            }

            // Slide down the wall if falling
            if (rb2d.velocity.y < 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, -wallSlideSpeed);
            }
        }



        UpdateAnimationState();

        //if (Input.GetKey(KeyCode.A))
        //{
        //    if (playerFacingRight)
        //    {
        //        transform.Rotate(0f, 180f, 0f);
        //        playerFacingRight = false;
        //    }
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    if (!playerFacingRight)
        //    {
        //        transform.Rotate(0f, 180f, 0f);
        //        playerFacingRight = true;
        //    }
        //}
    }

    private void PerformWallJump()
    {
        // Make the player jump away from the wall
        isWallJumping = true;

        // Apply a force opposite to the wall direction
        float jumpDirection = isTouchingLeftWall() ? -1 : 1;
        jumpSoundEffect.Play();
        rb2d.velocity = new Vector2(jumpForce * jumpDirection, jumpForce);
    }

    private bool isTouchingLeftWall()
    {
        return touchingStickyWalls && transform.position.x < 0;
    }

    private bool isTouchingRightWall()
    {
        return touchingStickyWalls && transform.position.x > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "StickyWall")
        {
            touchingStickyWalls = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "StickyWall")
        {
            touchingStickyWalls = false;
            isWallJumping = false;
        }
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        if (!touchingStickyWalls)
        {
            if (dirX > 0f)
            {
                state = MovementState.running;
                sprite.flipX = false;
                //anim.
            }
            else if (dirX < 0f)
            {
                state = MovementState.running;
                sprite.flipX = true;
            }
            else
            {
                state = MovementState.idle;
            }
        }
        else
        {
            state = MovementState.falling;
        }
        

        if (rb2d.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb2d.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        
    }
}
