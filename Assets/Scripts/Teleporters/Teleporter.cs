using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform Destination;
    public AudioSource TeleportSound;

    private void Start()
    {
        TeleportSound.enabled = false;
    }

    public Transform GetDestination()
    {

        TeleportSound.enabled = true;
        TeleportSound.Play();
        return Destination;

    }
    

}
