using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TileSound : MonoBehaviour
{
    public AudioSource tileSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tileSound.Play();
            gameObject.SetActive(false);
        }
    }
}
