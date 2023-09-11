using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityReverse : MonoBehaviour
{
    public float invertedGravity;
    public float originalGravity;

    private float OriginalBounce;
    public PlayerMovement playermovement;
    public GameObject Player;
    Rigidbody2D PlayerRigidbody;

    public Animator playerAnimator;

    private void Start()
    {
        PlayerRigidbody = Player.GetComponent<Rigidbody2D>();
        originalGravity = PlayerRigidbody.gravityScale;
        invertedGravity = originalGravity * -1;

        OriginalBounce = playermovement.jumpingPower;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerAnimator = collision.GetComponent<Animator>();

            PlayerRigidbody.gravityScale = invertedGravity;
            playermovement.jumpingPower = playermovement.jumpingPower * -1;

            playerAnimator.Play("Idle (AntiGravity)");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerAnimator = collision.GetComponent<Animator>();

            PlayerRigidbody.gravityScale = originalGravity;
            playermovement.jumpingPower = OriginalBounce;

            playerAnimator.Play("Idle Animation");

        }
    }
}
