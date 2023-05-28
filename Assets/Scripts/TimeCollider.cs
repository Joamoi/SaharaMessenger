using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCollider : MonoBehaviour
{
    public int dayOrCycle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (dayOrCycle == 1)
            {
                TimeManager.timeInstance.dayOnly = true;
            }

            else
            {
                TimeManager.timeInstance.dayOnly = false;
                TimeManager.timeInstance.StartCoroutine("DayToNight");
            }
        }
    }
}
