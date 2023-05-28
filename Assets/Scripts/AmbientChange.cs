using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientChange : MonoBehaviour
{
    public Color32 lightColor;
    public Color32 darkColor;
    public Transform lightEdge;
    public Transform darkEdge;
    public Transform fox;

    private bool ambientChanging = false;

    // Update is called once per frame
    void Update()
    {
        if (ambientChanging)
        {
            float t = (fox.position.z - lightEdge.position.z) / (darkEdge.position.z - lightEdge.position.z);

            byte r = (byte)Mathf.Lerp(lightColor.r, darkColor.r, t);
            byte g = (byte)Mathf.Lerp(lightColor.g, darkColor.g, t);
            byte b = (byte)Mathf.Lerp(lightColor.b, darkColor.b, t);

            RenderSettings.ambientLight = new Color32(r, g, b, 255);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ambientChanging = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ambientChanging = false;

            if (EventManager.eventInstance.lightOrDark == 1)
            {
                RenderSettings.ambientLight = lightColor;
            }

            else
            {
                RenderSettings.ambientLight = darkColor;
            }
        }
    }
}
