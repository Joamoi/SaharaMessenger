using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jackal : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetBool("Running", true);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetBool("Running", false);
        }
    }
}
