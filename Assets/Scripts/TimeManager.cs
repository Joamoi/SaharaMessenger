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
    private float lightAngle;
    public float nightLightAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float newHPPosX = Mathf.Lerp(hp100PosX, hp0PosX, (hpMax - hp) / hpMax);
        //hpMask.transform.position = new Vector3(newHPPosX, hpMask.transform.position.y, hpMask.transform.position.z);
    }
}
