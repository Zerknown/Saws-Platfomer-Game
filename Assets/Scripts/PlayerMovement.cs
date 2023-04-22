using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;

    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private float dirX = 0f;
    private enum MovementState { idle, running, jumping, falling }

    private bool playerFacingRight = true;
    
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


    private void UpdateAnimationState()
    {
        MovementState state;

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
