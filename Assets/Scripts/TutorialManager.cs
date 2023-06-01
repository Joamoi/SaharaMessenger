using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TutorialManager : MonoBehaviour
{
    public GameObject lookTutorial;
    public GameObject moveTutorial;
    public GameObject jumpTutorial;
    public GameObject runTutorial;

    public CinemachineFreeLook cineCam;
    private float currentCamValueX;

    private bool lookTutorialOn = false;
    private bool moveTutorialOn = false;
    private bool jumpTutorialOn = false;
    private bool runTutorialOn = false;

    // Start is called before the first frame update
    void Start()
    {
        currentCamValueX = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookTutorialOn)
        {
            if (cineCam.m_XAxis.Value != currentCamValueX && currentCamValueX != 0f)
            {
                StartCoroutine("LookToMove");
                lookTutorialOn = false;
            }

            currentCamValueX = cineCam.m_XAxis.Value;
        }

        if (moveTutorialOn)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine("MoveToJump");
                moveTutorialOn = false;
            }
        }

        if (jumpTutorialOn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine("JumpToRun");
                jumpTutorialOn = false;
            }
        }

        if (runTutorialOn)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine("RunToEnd");
                runTutorialOn = false;
            }
        }
    }

    public void LookTutorial()
    {
        lookTutorial.SetActive(true);
        lookTutorialOn = true;
    }

    IEnumerator LookToMove()
    {
        yield return new WaitForSeconds(0.5f);

        lookTutorial.SetActive(false);

        yield return new WaitForSeconds(3f);

        moveTutorial.SetActive(true);
        moveTutorialOn = true;
    }

    IEnumerator MoveToJump()
    {
        yield return new WaitForSeconds(0.5f);

        moveTutorial.SetActive(false);

        yield return new WaitForSeconds(10f);

        jumpTutorial.SetActive(true);
        jumpTutorialOn = true;
    }

    IEnumerator JumpToRun()
    {
        yield return new WaitForSeconds(0.5f);

        jumpTutorial.SetActive(false);

        yield return new WaitForSeconds(10f);

        runTutorial.SetActive(true);
        runTutorialOn = true;
    }

    IEnumerator RunToEnd()
    {
        yield return new WaitForSeconds(0.5f);

        runTutorial.SetActive(false);
        gameObject.GetComponent<TutorialManager>().enabled = false;
    }
}
