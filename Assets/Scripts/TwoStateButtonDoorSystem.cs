using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoStateButtonDoorSystem : MonoBehaviour
{
    [Header("Button Status :")]
    public bool isActivated = true;
    public bool canReactivate = false;

    [Space(8)]

    [Header("Target Block :")]
    public Vector2 offset = new Vector2(0f, 0f);
    public Transform doorBlock;
    public float moveSpeed = 5f;

    [Space(8)]

    [Header("Block and Camera Delays :")]
    public float moveDelay;
    public float returnDelay;

    [Space(8)]

    [Header("Script References :")]
    public CameraMovement cameraMovement;
    public PlayerSwitchingManager PlayerSwitcher;
    public GameManager gameManager;

    private bool playerInRange = false;
    private bool movingToDestination = false; // New variable to track the door's movement direction
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    public bool CanPress = true; //Note to check inspector, by default the box should be checked when starting

    private void Start()
    {
        initialPosition = doorBlock.position;
        targetPosition = initialPosition + new Vector3(offset.x, offset.y, 0);
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            cameraMovement.playerTransform = doorBlock.transform;
            PlayerSwitcher.players[PlayerSwitcher.activePlayerIndex].GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            if (!movingToDestination && CanPress == true)
            {
                movingToDestination = true;
                StartCoroutine(MoveDoor(moveDelay, targetPosition));
                CanPress = false;
            }
            else if(movingToDestination && CanPress == true)
            {
                movingToDestination = false;
                StartCoroutine(MoveDoor(moveDelay, initialPosition));
                CanPress = false;
            }
            StartCoroutine(CameraReturn());
            StartCoroutine(PressEnable());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    IEnumerator MoveDoor(float delayTime, Vector3 target)
    {
        gameManager.canMove = false;
        gameManager.canSwitch = false;

        yield return new WaitForSeconds(delayTime);

        Vector3 currentPos = doorBlock.position;
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(currentPos, target);

        while (doorBlock.position != target)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            doorBlock.position = Vector3.Lerp(currentPos, target, fractionOfJourney);

            yield return null;
        }

        gameManager.canMove = true;
        gameManager.canSwitch = true;
    }
    IEnumerator CameraReturn()
    {
        yield return new WaitForSeconds(returnDelay);

        cameraMovement.playerTransform = PlayerSwitcher.players[PlayerSwitcher.activePlayerIndex].transform;

        gameManager.canMove = true;
        gameManager.canSwitch = true;   //Ref GameManager

       
    }

    IEnumerator PressEnable()
    {
        yield return new WaitForSeconds(moveDelay + returnDelay);
                                                                   //Note :
        CanPress = true;                                                           //the value within the brackets is subjective and not properly tested
                                                                   //in the event that the time it takes for the "enabled" to become true
                                                                   //is inappropriate, create a new public variable for this time delay
        

    }
   
}
