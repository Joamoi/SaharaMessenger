using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static float timeDrainMultiplier;
    public GameObject light;
    public Gradient gradient;

    public float dayLength = 10f;
    public float transitionLength = 10f;
    public float nightLength = 10f;
    private float rotationX;
    private float rotationY;
    public float dayLightAngle = -10f;
    public float nightLightAngle = -90f;
    private bool dayToNight = false;
    private bool nightToDay = false;
    private float transitionTime = 0f;
    private bool colorChanging = false;

    // Start is called before the first frame update
    void Start()
    {
        rotationY = light.transform.localEulerAngles.y;
        StartCoroutine("Day");
    }

    // Update is called once per frame
    void Update()
    {
        if (dayToNight)
        {
            transitionTime += Time.deltaTime;

            rotationX = Mathf.Lerp(nightLightAngle, dayLightAngle, (transitionLength - transitionTime) / transitionLength);
            light.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);

            if (colorChanging)
            {
                light.GetComponent<Light>().color = gradient.Evaluate(1f - (transitionLength - transitionTime) / (transitionLength / 2f));
            }
        }

        if (nightToDay)
        {
            transitionTime += Time.deltaTime;

            rotationX = Mathf.Lerp(dayLightAngle, nightLightAngle, (transitionLength - transitionTime) / transitionLength);
            light.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);

            if (colorChanging)
            {
                light.GetComponent<Light>().color = gradient.Evaluate((transitionLength / 2f - transitionTime) / (transitionLength / 2f));
            }
        }
    }

    IEnumerator Day()
    {
        Debug.Log("Day Started");
        yield return new WaitForSeconds(dayLength);
        StartCoroutine("DayToNight");
    }

    IEnumerator DayToNight()
    {
        Debug.Log("DayToNight Started");
        transitionTime = 0f;
        dayToNight = true;
        yield return new WaitForSeconds(transitionLength / 2f);
        colorChanging = true;
        yield return new WaitForSeconds(transitionLength / 2f);
        colorChanging = false;
        dayToNight = false;
        StartCoroutine("Night");
    }

    IEnumerator Night()
    {
        Debug.Log("Night Started");
        yield return new WaitForSeconds(dayLength);
        StartCoroutine("NightToDay");
    }

    IEnumerator NightToDay()
    {
        Debug.Log("NightToDay Started");
        transitionTime = 0f;
        nightToDay = true;
        colorChanging = true;
        yield return new WaitForSeconds(transitionLength / 2f);
        colorChanging = false;
        yield return new WaitForSeconds(transitionLength / 2f);
        nightToDay = false;
        StartCoroutine("Day");
    }
}
