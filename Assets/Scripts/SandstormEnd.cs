using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandstormEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.eventInstance.StartCoroutine("SandstormEnd");
            gameObject.SetActive(false);
        }
    }
}
