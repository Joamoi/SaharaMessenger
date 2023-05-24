using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class EventManager : MonoBehaviour
{
    public static EventManager eventInstance;

    public GameObject fox;
    public GameObject oldFox;
    public GameObject scarf;
    public GameObject oldFoxScarf;
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
    private bool startTalk1 = false;
    private bool startTalk2 = false;
    public string[] oldFoxLines1;
    public string[] oldFoxLines2;

    private Color32 originalAmbientColor;

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

        else
        {
            scarf.SetActive(true);
        }

        originalAmbientColor = RenderSettings.ambientLight;
    }

    // Update is called once per frame
    void Update()
    {
        // for testing
        if (Input.GetKeyDown(KeyCode.G))
        {
            RenderSettings.ambientLight = originalAmbientColor;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            RenderSettings.ambientLight = new Color32(0, 0, 0, 255);
        }

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

                    else if (startTalk2)
                    {
                        StartCoroutine("StartTalk2Done");
                    }

                    else if (startTalk1)
                    {
                        StartCoroutine("StartTalk1Done");
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
        PlayerManager.playerInstance.noDrain = true;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;
        cineCam.m_XAxis.Value = 45f;
        cineCam.m_YAxis.Value = 0.3f;

        startRockCollider.enabled = false;
        fox.transform.position = startPos.position;

        yield return new WaitForSeconds(2f);

        PlayerManager.playerInstance.x = 1f;
        PlayerManager.playerInstance.z = -1f;

        yield return new WaitForSeconds(1.5f);

        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        yield return new WaitForSeconds(2f);

        PlayerManager.playerInstance.x = 1f;
        PlayerManager.playerInstance.z = 1f;

        oldFox.GetComponent<OldFox>().Walk();

        yield return new WaitForSeconds(0.5f);

        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        yield return new WaitForSeconds(0.5f);

        oldFox.GetComponent<OldFox>().Stop();

        yield return new WaitForSeconds(0.5f);

        startTalk1 = true;
        npcText.text = "";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = oldFoxLines1;

        StartCoroutine("NextLine");
    }

    IEnumerator StartTalk1Done()
    {
        npcTextField.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        oldFoxScarf.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        scarf.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        startTalk2 = true;
        npcText.text = "";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = oldFoxLines2;

        StartCoroutine("NextLine");
    }

    IEnumerator StartTalk2Done()
    {
        npcTextField.SetActive(false);

        camTurnTime = 2f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = 125f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0.3f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(3f);

        PlayerManager.playerInstance.canMove = true;
        PlayerManager.playerInstance.noDrain = false;
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
        PlayerManager.playerInstance.noDrain = true;
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
        PlayerManager.playerInstance.noDrain = false;
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
        PlayerManager.playerInstance.noDrain = true;
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
        camTargetY = 0.3f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(1f);

        PlayerManager.playerInstance.z = 1f;

        yield return new WaitForSeconds(0.7f);

        PlayerManager.playerInstance.z = 0f;

        yield return new WaitForSeconds(0.5f);

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

        chaseJackal1.GetComponent<Jackal>().StartCoroutine("StartChase");
        chaseJackal2.GetComponent<Jackal>().StartCoroutine("StartChase");
        chaseJackal3.GetComponent<Jackal>().StartCoroutine("StartChase");

        yield return new WaitForSeconds(1.2f);

        camTurnTime = 0.3f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = 0f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0.3f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(0.4f);

        PlayerManager.playerInstance.canMove = true;
        PlayerManager.playerInstance.noDrain = false;
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
        PlayerManager.playerInstance.noDrain = true;
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
        PlayerManager.playerInstance.noDrain = false;
        cineCam.m_XAxis.m_MaxSpeed = camSpeedX;
        cineCam.m_YAxis.m_MaxSpeed = camSpeedY;
    }

    public void StopChase()
    {
        chaseJackal1.GetComponent<Jackal>().StopChase();
        chaseJackal2.GetComponent<Jackal>().StopChase();
        chaseJackal3.GetComponent<Jackal>().StopChase();
    }
}
