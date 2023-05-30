using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.playerInstance.animator.SetBool("InDust", true);
            PlayerManager.playerInstance.inDust = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.playerInstance.inDust = false;
            PlayerManager.playerInstance.animator.SetBool("InDust", false);
        }
    }
}
