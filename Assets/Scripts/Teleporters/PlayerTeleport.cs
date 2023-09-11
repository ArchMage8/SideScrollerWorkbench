using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject startTeleport;
  

  

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        { 
            if(startTeleport != null)
            {
                transform.position = startTeleport.GetComponent<Teleporter>().GetDestination().position;
                
              
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            startTeleport = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if(collision.gameObject == startTeleport)
            {
                startTeleport = null;
            }
        }
    }
}
