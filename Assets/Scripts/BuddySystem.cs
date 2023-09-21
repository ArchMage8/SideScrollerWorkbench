using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuddySystem : MonoBehaviour
{
    public GameObject[] players; // Populate this array with your player GameObjects
    public int activePlayerIndex = 0;

    public CameraMovement cameraMovement; // Reference to the CameraMovement script
    public GameManager gameManager;

    private void Start()
    {
        cameraMovement.CameraTarget = players[0].transform; // Set initial player transform for camera
        SwitchPlayer(0); // Activate Player 1 by default
    }

    private void Update()
    {
        if (gameManager.canSwitch == true)
        {
            // Handle player switching input
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchPlayer(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchPlayer(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SwitchPlayer(2);
            }
        }
    }

    private void SwitchPlayer(int newIndex)
    {

        players[activePlayerIndex].GetComponent<PlayerMovement>().isActivePlayer = false;
        activePlayerIndex = newIndex;
        players[activePlayerIndex].GetComponent<PlayerMovement>().isActivePlayer = true;

        cameraMovement.CameraTarget = players[activePlayerIndex].transform; // Update camera target



    }
}
