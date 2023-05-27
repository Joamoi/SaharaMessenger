using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleFall : MonoBehaviour
{
    public GameObject[] pieces;
    public GameObject cover;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cover.SetActive(false);

            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i].GetComponent<Rigidbody>().isKinematic = false;
                pieces[i].GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
