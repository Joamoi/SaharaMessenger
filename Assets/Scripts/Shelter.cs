using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelter : MonoBehaviour
{
    private bool canRest;

    // Update is called once per frame
    void Update()
    {
        if (canRest)
        {
            if (Input.GetKey(KeyCode.E))
            {
                PlayerManager.playerInstance.Rest();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canRest = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canRest = false;
        }
    }
}
