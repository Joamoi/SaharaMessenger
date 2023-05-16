using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPeek : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.eventInstance.StartCoroutine("EnemyPeek");
        }
    }
}
