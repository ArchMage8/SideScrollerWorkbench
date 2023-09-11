using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    public Sprite Activated;
    private Sprite Deactivated;
    
    private float OriginalBounce;
    private float JumpPadBounce;

    public float BounceScale;

    public PlayerMovement playermovement;
    public AudioSource JumpPadSound;

    public LayerMask groundLayer; 
    public LayerMask antiGroundLayer;

    private void Start()
    {
       Deactivated = GetComponent<SpriteRenderer>().sprite; 
       OriginalBounce = playermovement.jumpingPower;
       JumpPadBounce = OriginalBounce * BounceScale;
       JumpPadSound.enabled = false;

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enter");

            GetComponent<SpriteRenderer>().sprite = Activated;

            playermovement.jumpingPower = JumpPadBounce;
            JumpPadSound.enabled = true;

            JumpPadSound.Play();

        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().sprite = Deactivated;
            playermovement.jumpingPower = OriginalBounce;
        }
    }
}
    