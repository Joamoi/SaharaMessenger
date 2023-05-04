using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static float timeDrainMultiplier;
    public GameObject light;

    public float dayLength = 10f;
    public float transitionLength = 10f;
    public float nightLength = 10f;
    private float rotationX;
    private float rotationY;
    public float dayLightAngle = -10f;
    public float nightLightAngle = -90f;
    private bool transitioning = false;
    private float transitionTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rotationY = light.transform.eulerAngles.y;
        Day();
    }

    // Update is called once per frame
    void Update()
    {
        if (transitioning)
        {
            transitionTime += Time.deltaTime;

            rotationX = Mathf.Lerp(dayLightAngle, nightLightAngle, (transitionLength - transitionTime) / transitionLength);
            light.transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);

        }
    }

    IEnumerator Day()
    {
        yield return new WaitForSeconds(dayLength);
        StartCoroutine("DayToNight");
    }

    IEnumerator DayToNight()
    {
        transitioning = true;
        transitionTime = 0f;
        yield return new WaitForSeconds(transitionLength);
        transitioning = false;
        StartCoroutine("Night");
    }

    IEnumerator Night()
    {
        yield return new WaitForSeconds(dayLength);
        StartCoroutine("NightToDay");
    }

    IEnumerator NightToDay()
    {
        transitioning = true;
        transitionTime = 0f;
        yield return new WaitForSeconds(transitionLength);
        transitioning = false;
        StartCoroutine("Day");
    }
}
