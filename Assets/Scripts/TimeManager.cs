using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TimeManager : MonoBehaviour
{
    public static TimeManager timeInstance;

    [HideInInspector]
    public float timeDrainMultiplier;
    public GameObject dirLight;
    public Gradient gradient;

    public float dayMultiplier = 1f;
    public float afternoonMultiplier = 0.8f;
    public float eveningMultiplier = 0.6f;
    public float nightMultiplier = 0.4f;

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
    [HideInInspector]
    //public bool dayOnly = false;

    public AudioSource daySound;
    public AudioSource nightSound;
    private float originalDayVolume;
    private float originalNightVolume;

    public ParticleSystem[] sandstorms;
    public Color32 dayStormColor;
    public Color32 nightStormColor;

    //public GameObject skybox;
    private float blend;

    void Awake()
    {
        timeInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalDayVolume = daySound.volume;
        originalNightVolume = nightSound.volume;

        rotationY = dirLight.transform.localEulerAngles.y;
        //StartCoroutine("Day");
    }

    // Update is called once per frame
    void Update()
    {
        if (dayToNight)
        {
            transitionTime += Time.deltaTime;

            rotationX = Mathf.Lerp(nightLightAngle, dayLightAngle, (transitionLength - transitionTime) / transitionLength);
            dirLight.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);

            if (colorChanging)
            {
                dirLight.GetComponent<Light>().color = gradient.Evaluate(1f - (transitionLength - transitionTime) / (transitionLength / 2f));
            }

            blend = ((1f - (transitionLength - transitionTime)) / transitionLength) + 1f;
            //skybox.GetComponent<SkyboxBlender>().blend = blend;

            float t = 1f - (transitionLength - transitionTime) / (transitionLength / 2f);

            byte r = (byte)Mathf.Lerp(dayStormColor.r, nightStormColor.r, t);
            byte g = (byte)Mathf.Lerp(dayStormColor.g, nightStormColor.g, t);
            byte b = (byte)Mathf.Lerp(dayStormColor.b, nightStormColor.b, t);

            for (int i = 0; i < sandstorms.Length; i++)
            {
                ParticleSystem.MainModule main = sandstorms[i].main;
                Color color = new Color32(r, g, b, 255);
                main.startColor = color;
            }
        }

        if (nightToDay)
        {
            transitionTime += Time.deltaTime;

            rotationX = Mathf.Lerp(dayLightAngle, nightLightAngle, (transitionLength - transitionTime) / transitionLength);
            dirLight.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);

            if (colorChanging)
            {
                dirLight.GetComponent<Light>().color = gradient.Evaluate((transitionLength / 2f - transitionTime) / (transitionLength / 2f));
            }

            blend = (transitionLength - transitionTime) / (transitionLength);
            //skybox.GetComponent<SkyboxBlender>().blend = blend;

            float t = (transitionLength / 2f - transitionTime) / (transitionLength / 2f);

            byte r = (byte)Mathf.Lerp(dayStormColor.r, nightStormColor.r, t);
            byte g = (byte)Mathf.Lerp(dayStormColor.g, nightStormColor.g, t);
            byte b = (byte)Mathf.Lerp(dayStormColor.b, nightStormColor.b, t);

            for (int i = 0; i < sandstorms.Length; i++)
            {
                ParticleSystem.MainModule main = sandstorms[i].main;
                Color color = new Color32(r, g, b, 255);
                main.startColor = color;
            }
        }
    }

    public IEnumerator Day()
    {
        dirLight.transform.localRotation = Quaternion.Euler(dayLightAngle, rotationY, 0f);
        dirLight.GetComponent<Light>().color = gradient.Evaluate(0f);

        for (int i = 0; i < sandstorms.Length; i++)
        {
            ParticleSystem.MainModule main = sandstorms[i].main;
            Color color = new Color32(dayStormColor.r, dayStormColor.g, dayStormColor.b, 255);
            main.startColor = color;
        }

        timeDrainMultiplier = dayMultiplier;
        Debug.Log("Day Started");
        yield return new WaitForSeconds(dayLength);

        //if (!dayOnly)
        //{
        //    StartCoroutine("DayToNight");
        //}
    }

    public IEnumerator DayToNight()
    {
        timeDrainMultiplier = afternoonMultiplier;
        Debug.Log("DayToNight Started");
        transitionTime = 0f;
        dayToNight = true;
        yield return new WaitForSeconds(transitionLength / 2f);
        colorChanging = true;
        timeDrainMultiplier = eveningMultiplier;
        daySound.Stop();
        nightSound.Play();
        yield return new WaitForSeconds(transitionLength / 2f);
        colorChanging = false;
        dayToNight = false;
        StartCoroutine("Night");
    }

    public IEnumerator Night()
    {
        dirLight.transform.localRotation = Quaternion.Euler(nightLightAngle, rotationY, 0f);
        dirLight.GetComponent<Light>().color = gradient.Evaluate(1f);

        for (int i = 0; i < sandstorms.Length; i++)
        {
            ParticleSystem.MainModule main = sandstorms[i].main;
            Color color = new Color32(nightStormColor.r, nightStormColor.g, nightStormColor.b, 255);
            main.startColor = color;
        }

        timeDrainMultiplier = nightMultiplier;
        Debug.Log("Night Started");
        yield return new WaitForSeconds(dayLength);
        //StartCoroutine("NightToDay");
    }

    public IEnumerator NightToDay()
    {
        timeDrainMultiplier = eveningMultiplier;
        Debug.Log("NightToDay Started");
        transitionTime = 0f;
        nightToDay = true;
        colorChanging = true;
        yield return new WaitForSeconds(transitionLength / 2f);
        colorChanging = false;
        timeDrainMultiplier = afternoonMultiplier;
        nightSound.Stop();
        daySound.Play();
        yield return new WaitForSeconds(transitionLength / 2f);
        nightToDay = false;
        StartCoroutine("Day");
    }
}
