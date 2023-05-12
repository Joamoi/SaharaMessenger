using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class EventManager : MonoBehaviour
{
    public static EventManager eventInstance;

    public GameObject fox;
    public Collider startRockCollider;
    public Transform startPos;
    public CinemachineFreeLook cineCam;
    public bool playStartCutscene;

    public GameObject hpBar;
    public GameObject staminaBar;
    public GameObject npcTextField;
    public TextMeshProUGUI npcText;

    private float camSpeedX;
    private float camSpeedY;
    private bool camTurning = false;
    private float camTurnSpeed;
    private float camStartX;
    private float camTargetX;
    private float lerpFloat;

    string[] speechLines;
    private bool lineShown = false;
    private int lineIndex;

    void Awake()
    {
        eventInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        camSpeedX = cineCam.m_XAxis.m_MaxSpeed;
        camSpeedY = cineCam.m_YAxis.m_MaxSpeed;

        if (playStartCutscene)
        {
            StartCoroutine("StartCutScene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (camTurning)
        {
            lerpFloat += camTurnSpeed * Time.deltaTime;

            if (lerpFloat >= 1f)
            {
                lerpFloat = 1f;
                camTurning = false;
            }

            cineCam.m_XAxis.Value = Mathf.Lerp(camStartX, camTargetX, lerpFloat);
        }

        if (lineShown)
        {
            if (Input.GetKey(KeyCode.E))
            {
                lineIndex++;
                lineShown = false;

                if (lineIndex == speechLines.Length)
                {
                    StartCoroutine("QuitTalk");
                }

                else
                {
                    StartCoroutine("NextLine");
                }
            }
        }
    }

    IEnumerator StartCutScene()
    {
        hpBar.SetActive(false);
        staminaBar.SetActive(false);
        PlayerManager.playerInstance.canMove = false;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;
        cineCam.m_XAxis.Value = 45f;
        cineCam.m_YAxis.Value = 0.3f;

        startRockCollider.enabled = false;
        fox.transform.position = startPos.position;

        yield return new WaitForSeconds(2f);

        PlayerManager.playerInstance.x = 1f;
        PlayerManager.playerInstance.z = -1f;

        yield return new WaitForSeconds(2f);

        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        yield return new WaitForSeconds(0.5f);

        camTurnSpeed = 0.5f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = 135f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(3f);

        PlayerManager.playerInstance.canMove = true;
        startRockCollider.enabled = true;
        cineCam.m_XAxis.m_MaxSpeed = camSpeedX;
        cineCam.m_YAxis.m_MaxSpeed = camSpeedY;
        hpBar.SetActive(true);
        staminaBar.SetActive(true);
    }

    public IEnumerator Talk(string[] newSpeechLines)
    {
        hpBar.SetActive(false);
        staminaBar.SetActive(false);
        PlayerManager.playerInstance.canMove = false;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;

        yield return new WaitForSeconds(0.5f);

        npcText.text = "";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = newSpeechLines;

        StartCoroutine("NextLine");
    }

    IEnumerator NextLine()
    {
        string currentLine = speechLines[lineIndex];
        npcText.text = "";

        for (int i = 0; i < currentLine.Length; i++)
        {
            npcText.text += currentLine[i];

            yield return new WaitForSeconds(0.05f);
        }

        lineShown = true;
    }

    IEnumerator QuitTalk()
    {
        npcTextField.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        hpBar.SetActive(true);
        staminaBar.SetActive(true);
        PlayerManager.playerInstance.canMove = true;
        cineCam.m_XAxis.m_MaxSpeed = camSpeedX;
        cineCam.m_YAxis.m_MaxSpeed = camSpeedY;
    }
}
