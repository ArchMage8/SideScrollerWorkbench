using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    

    [Header("Movement Values :")]
    public float jumpingPower = 16f;
    public float speed = 8f;
    public AudioSource JumpSound;

    private bool isfacingRight = true;

    [Space(8)]
    public bool isActivePlayer = false; // Add this variable for player activation

    private bool jumpPressed = false;
    private bool isJumping = false;

    [Space(8)]

    [Header("Ground Checks :")]
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Transform headGroundCheck;
    public LayerMask groundLayer;
    public LayerMask headLayer;

    [Space(8)]

    [Header("Coin Stuff :")]
    public CoinManager coinManager;
    public AudioSource coinAudioSource;
    public GameManager gameManager;

    private void Start()
    {
        JumpSound.enabled = false;
        coinAudioSource.enabled = false;
    }

    void Update()
    {
        
        if (!isActivePlayer) return;

        horizontal = Input.GetAxis("Horizontal"); //Pressing A and D

        if (Input.GetButtonDown("Jump") && gameManager.canMove == true)
        {
            if (IsGrounded() || HeadGrounded())
            {
                jumpPressed = true;

                JumpSound.enabled = true;

                JumpSound.Play();
            }
        }

        flip();
    }

    private void FixedUpdate()
    {
        if (!isActivePlayer)
        {
            rb.velocity = Vector2.zero; // Stop player movement when not active
            rb.gravityScale = 30f; //Boosted Gravity to compensate Vector2.Zero slowing down fall rate
            return;
        }

        if (jumpPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpPressed = false;
            isJumping = true;
        }

        if (gameManager.canMove == true)
        {

            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        }

        if (IsGrounded() || HeadGrounded())
        {
            isJumping = false;
        }
    }

    private void flip()
    {
        if (isfacingRight && horizontal < 0f || !isfacingRight && horizontal > 0f)
        {
            isfacingRight = !isfacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActivePlayer) return;

        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Point!");
            coinAudioSource.enabled = true;

            if (coinAudioSource != null)
            {
                coinAudioSource.Play();
            }

            coinManager.CoinCount++;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);
    }

    private bool HeadGrounded()
    {
        return Physics2D.OverlapCircle(headGroundCheck.position, 1f, headLayer);
    }
}
