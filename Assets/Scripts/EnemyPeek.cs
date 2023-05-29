using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPeek : MonoBehaviour
{
    public int peekValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (peekValue == 1)
            {
                EventManager.eventInstance.StartCoroutine("EnemyPeek1");
            }

            else if (peekValue == 2)
            {
                EventManager.eventInstance.StartCoroutine("EnemyPeek2");
            }

            else
            {
                EventManager.eventInstance.StartCoroutine("EnemyPeek3");
            }

            gameObject.SetActive(false);
        }
    }
}
