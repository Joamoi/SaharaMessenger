using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.eventInstance.StartCoroutine("ChaseSandstorm");
            gameObject.SetActive(false);
        }
    }
}
