using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private bool walking = false;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (walking)
        {
            gameObject.transform.position -= transform.forward * speed * Time.deltaTime;
        }
    }

    public void Walk()
    {
        walking = true;
    }

    public void Stop()
    {
        walking = false;
    }
}
