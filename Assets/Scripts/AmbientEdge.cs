using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientEdge : MonoBehaviour
{
    public int lightOrDark;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.eventInstance.lightOrDark = lightOrDark;
        }
    }
}
