using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class EventManager : MonoBehaviour
{
    public static EventManager eventInstance;

    public GameObject fox;
    public GameObject peekJackal;
    public GameObject chaseJackal1;
    public GameObject chaseJackal2;
    public GameObject chaseJackal3;
    public Animator peekEnemyAnimator;
    public Collider startRockCollider;
    public Transform startPos;
    public CinemachineFreeLook cineCam;
    public bool playStartCutscene;

    public GameObject hpBar;
    public GameObject staminaBar;
    public GameObject npcTextField;
    public TextMeshProUGUI npcText;
    public GameObject textArrow;

    private float camSpeedX;
    private float camSpeedY;
    private bool camTurning = false;
    private float camTurnTime;
    private float camStartX;
    private float camTargetX;
    private float camStartY;
    private float camTargetY;
    private float lerpFloat;

    string[] speechLines;
    private bool lineShown = false;
    private int lineIndex;
    private bool chaseTalk = false;

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
            lerpFloat += Time.deltaTime;

            if (lerpFloat >= camTurnTime)
            {
                lerpFloat = 1f;
                camTurning = false;
            }

            cineCam.m_XAxis.Value = Mathf.Lerp(camStartX, camTargetX, lerpFloat);
            cineCam.m_YAxis.Value = Mathf.Lerp(camStartY, camTargetY, lerpFloat);
        }

        if (lineShown)
        {
            if (Input.GetKey(KeyCode.E))
            {
                lineIndex++;
                lineShown = false;
                textArrow.SetActive(false);

                if (lineIndex == speechLines.Length)
                {
                    if (chaseTalk)
                    {
                        StartCoroutine("Chase");
                    }

                    else
                    {
                        StartCoroutine("QuitTalk");
                    }
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

        camTurnTime = 2f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = 125f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0.3f;
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

    public IEnumerator EnemyPeek()
    {
        hpBar.SetActive(false);
        staminaBar.SetActive(false);
        PlayerManager.playerInstance.canMove = false;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;

        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        peekJackal.SetActive(true);

        yield return new WaitForSeconds(1f);

        camTurnTime = 1f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = 45f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(1.5f);

        peekEnemyAnimator.SetTrigger("Peek");

        yield return new WaitForSeconds(3f);

        camTurnTime = 1f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = 125f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0.3f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(1.5f);

        peekJackal.SetActive(false);

        PlayerManager.playerInstance.canMove = true;
        cineCam.m_XAxis.m_MaxSpeed = camSpeedX;
        cineCam.m_YAxis.m_MaxSpeed = camSpeedY;
        hpBar.SetActive(true);
        staminaBar.SetActive(true);
    }

    public IEnumerator EnemyChaseTalk(string[] newSpeechLines)
    {
        hpBar.SetActive(false);
        staminaBar.SetActive(false);
        PlayerManager.playerInstance.canMove = false;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;

        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        chaseJackal1.SetActive(true);
        chaseJackal2.SetActive(true);
        chaseJackal3.SetActive(true);

        yield return new WaitForSeconds(1f);

        camTurnTime = 1f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = 180f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(1f);

        chaseTalk = true;
        npcText.text = "";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = newSpeechLines;

        StartCoroutine("NextLine");
    }

    public IEnumerator Chase()
    {
        chaseTalk = false;
        npcTextField.SetActive(false);

        chaseJackal1.GetComponent<Jackal>().StartChase();
        chaseJackal2.GetComponent<Jackal>().StartChase();
        chaseJackal3.GetComponent<Jackal>().StartChase();

        yield return new WaitForSeconds(1f);

        camTurnTime = 0.5f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = 0f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0.3f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(0.5f);

        PlayerManager.playerInstance.canMove = true;
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

            yield return new WaitForSeconds(0.03f);
        }

        yield return new WaitForSeconds(0.5f);

        lineShown = true;
        textArrow.SetActive(true);
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
