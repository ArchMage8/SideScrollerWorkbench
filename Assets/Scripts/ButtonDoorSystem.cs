using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoorSystem : MonoBehaviour
{
    [Header("Button Status :")]
    public bool isActivated = true; // Activation state of the button
    public bool canReactivate = false; // Can the button be reactivated?

    [Space(8)]

    [Header("Target Block :")]
    public Vector2 offset = new Vector2(0f, 0f); // Offset for door movement
    public Transform doorBlock; // Reference to the door block
    public float moveSpeed = 5f; // Speed at which the door moves

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



    private void Update()
    {
        if (playerInRange && isActivated && Input.GetKeyDown(KeyCode.F))
        {
            cameraMovement.playerTransform = doorBlock.transform;
            PlayerSwitcher.players[PlayerSwitcher.activePlayerIndex].GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            StartCoroutine(MoveDoor(moveDelay));


            StartCoroutine(CameraReturn());

            if (!canReactivate)
            {
                isActivated = false;
            }
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

    IEnumerator MoveDoor(float delayTime)
    {
        gameManager.canMove = false;   //Ref GameManager
        gameManager.canSwitch = false;

        yield return new WaitForSeconds(delayTime);

        Vector3 initialPosition = doorBlock.position;
        Vector3 targetPosition = initialPosition + new Vector3(offset.x, offset.y, 0);

        float startTime = Time.time;
        float journeyLength = Vector3.Distance(initialPosition, targetPosition);

        while (doorBlock.position != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            doorBlock.position = Vector3.Lerp(initialPosition, targetPosition, fractionOfJourney);


            yield return null;
        }
    }
    IEnumerator CameraReturn()
    {
        yield return new WaitForSeconds(returnDelay);

        cameraMovement.playerTransform = PlayerSwitcher.players[PlayerSwitcher.activePlayerIndex].transform;

        gameManager.canMove = true;
        gameManager.canSwitch = true;   //Ref GameManager
    }


}
