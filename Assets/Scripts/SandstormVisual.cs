using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandstormVisual : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventManager.eventInstance.VisualSandstorm();
            gameObject.SetActive(false);
        }
    }
}
