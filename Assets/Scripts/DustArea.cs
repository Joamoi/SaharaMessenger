using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DustArea : MonoBehaviour
{
    public AudioSource windSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.playerInstance.animator.SetBool("InDust", true);
            PlayerManager.playerInstance.inDust = true;

            windSound.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.playerInstance.inDust = false;
            PlayerManager.playerInstance.animator.SetBool("InDust", false);

            windSound.Stop();
        }
    }
}
