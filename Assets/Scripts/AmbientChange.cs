using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientChange : MonoBehaviour
{
    public Color32 lightColor;
    public Color32 darkColor;
    public Transform lightEdge;
    public Transform darkEdge;

    private bool ambientChanging = false;

    // Update is called once per frame
    void Update()
    {
        if (ambientChanging)
        {
            //float r = math
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
