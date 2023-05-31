using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChange : MonoBehaviour
{
    public int startDayOrNight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (startDayOrNight == 2)
            {
                TimeManager.timeInstance.StartCoroutine("DayToNight");
            }

            else
            {
                TimeManager.timeInstance.StartCoroutine("NightToDay");

            }
        }
    }
}
