using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseStart : MonoBehaviour
{
    public string[] speechLines;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.eventInstance.StartCoroutine("EnemyChaseTalk", speechLines);
            gameObject.SetActive(false);
        }
    }
}
