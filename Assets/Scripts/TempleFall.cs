using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleFall : MonoBehaviour
{
    public GameObject[] pieces;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i].GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
