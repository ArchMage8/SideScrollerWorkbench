using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateSystem : MonoBehaviour
{
    [Header("Target Platform :")]
    public GameObject targetBlock;
    public float moveDistanceX = 5f;
    public float moveDistanceY = 5f;

    [Space(6)]

    public float moveSpeedAway = 5f; //Block moving away from initial position
    public float moveSpeedBack = 2f; //Block returning to its initial position

    [Space(5)]

    public bool alreadyTriggered = false;
    [Space(8)]

    [Header("Camera Variables :")]
   
    public float blockDelay = 1f; 
    public float cameraReturnDelay = 1f;

    [Space(8)]

    [Header("Script References :")]
    public PlayerSwitchingManager PlayerSwitcher;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool playerOnPlate = false;
   

    public CameraMovement cameraMovement; 
    public GameManager gameManager;
   


    private void Start()
    {
        originalPosition = targetBlock.transform.position;
        targetPosition = originalPosition + new Vector3(moveDistanceX, moveDistanceY, 0);
      
    }

    private void Update()
    {
        if (playerOnPlate)
        {
                blockDelay -= Time.deltaTime;
            
            if (!alreadyTriggered)
            {
                
                cameraMovement.playerTransform = targetBlock.transform;

                PlayerSwitcher.players[PlayerSwitcher.activePlayerIndex].GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                gameManager.canMove = false;
                gameManager.canSwitch = false;
                

                if (blockDelay <= 0f)
                {
                    MoveTargetBlock(moveSpeedAway);
                }
                StartCoroutine(CameraReturn());
                
            }
            else
            {
                MoveTargetBlock(moveSpeedAway);
            }

        }
        else
        {
            
            MoveTargetBlock(moveSpeedBack);
        }

    }
       
    private void MoveTargetBlock(float speed) //Moving Block without time delay
    {
        Vector3 target = playerOnPlate ? targetPosition : originalPosition;
        targetBlock.transform.position = Vector3.MoveTowards(targetBlock.transform.position, target, speed * Time.deltaTime);
        

    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
                                                                //Used OnTrigger rather than on Oncollision in order to collision when the player is standing on the pressure plate
        if (collision.CompareTag("Player"))                     //Note: I used 2 BoxColliders, i to make the structure of the plate, and 1 to detect the player standing on it
        {
            playerOnPlate = true;       
        }

        
    }                                                           
                                                               
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnPlate = false;
        }

    }

    IEnumerator CameraReturn()
    {
        yield return new WaitForSeconds(cameraReturnDelay);

        alreadyTriggered = true;
        gameManager.canMove = true;
        gameManager.canSwitch = true;


        cameraMovement.playerTransform = PlayerSwitcher.players[PlayerSwitcher.activePlayerIndex].transform;


    }
}
