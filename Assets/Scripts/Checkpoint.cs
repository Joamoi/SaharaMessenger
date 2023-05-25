using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.eventInstance.cpValue++;
            EventManager.eventInstance.NewCheckpoint();
            gameObject.SetActive(false);
        }
    }
}
