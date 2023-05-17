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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.playerInstance.animator.SetBool("InDust", false);
        }
    }
}
