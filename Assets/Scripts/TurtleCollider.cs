using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.eventInstance.StartCoroutine("Turtle1");
            gameObject.SetActive(false);
        }
    }
}
