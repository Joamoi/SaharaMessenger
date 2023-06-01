using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SilentTemple : MonoBehaviour
{
    public AudioSource daySound;
    public AudioSource nightSound;

    private float daySoundVolume;
    private float nightSoundVolume;

    // Start is called before the first frame update
    void Start()
    {
        daySoundVolume = daySound.volume;
        nightSoundVolume = nightSound.volume;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            daySound.volume = 0.2f * daySoundVolume;
            nightSound.volume = 0.2f * nightSoundVolume;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            daySound.volume = daySoundVolume;
            nightSound.volume = nightSoundVolume;
        }
    }
}
