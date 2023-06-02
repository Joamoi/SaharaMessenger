using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TempleFall : MonoBehaviour
{
    public GameObject[] pieces;
    public GameObject cover;
    public AudioSource tileSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cover.SetActive(false);
            tileSound.Play();

            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i].GetComponent<Rigidbody>().isKinematic = false;
                pieces[i].GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
