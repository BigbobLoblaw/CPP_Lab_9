using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer lukeSprite;

    public float speed;
    public int jumpForce;
    public bool isGrounded;
    public bool isSwinging;

    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lukeSprite = GetComponent<SpriteRenderer>();

        if (speed <= 0)
        {
            speed = 5.0f;
        }

        if (jumpForce <= 0)
        {
            jumpForce = 100;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.01f;
        }

        if (!groundCheck)
        {
            Debug.Log("Groundcheck does not exist, please set a transform value for groundcheck");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            isSwinging = true;
        }

        else if (Input.GetButtonUp("Fire1"))
        {
            isSwinging = false;
        }

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("speed", Mathf.Abs(horizontalInput));
        anim.SetBool("isSwinging", isSwinging);

        if (lukeSprite.flipX && horizontalInput > 0.1 || !lukeSprite.flipX && horizontalInput < 0)
        {
            lukeSprite.flipX = !lukeSprite.flipX;
        }

    }
    public void StartJumpForceChange()
    {
        StartCoroutine(JumpForceChange());
    }

    IEnumerator JumpForceChange()
    {
        jumpForce = 700;
        yield return new WaitForSeconds(10.0f);
        jumpForce = 500;
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pickups")
        {
            Pickups curPickup = collision.GetComponent<Pickups>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (curPickup.currentCollectible)
                {
                    case Pickups.CollectibleType.KEY:
                        //add to inventory or other mechanic
                        Destroy(collision.gameObject);
                        break;
                }
            }
        }
    }
}
