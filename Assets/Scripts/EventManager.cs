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
    public Collider startRockCollider1;
    public Collider startRockCollider2;
    public Transform startPos;
    public CinemachineFreeLook cineCam;
    public bool playStartCutscene;

    public GameObject fade;
    public GameObject fadeAnimator;

    public GameObject hpBar;
    public GameObject staminaBar;
    public GameObject npcTextField;
    public GameObject npcTextObject;
    public TextMeshProUGUI npcText;
    public GameObject textArrow;
    public GameObject choice1;
    public GameObject choice2;
    public TextMeshProUGUI choiceText1;
    public TextMeshProUGUI choiceText2;

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
    private string currentConv;
    private bool choiceMade = false;
    private int choiceValue = 0;

    public string[] oldFoxLines1;
    public string[] oldFoxLines2;
    public string[] oldFoxLines3;
    public string[] oldFoxLines4;
    public string[] oldFoxLines5;
    public string[] oldFoxLines6;
    public string[] oldFoxLines7;
    public string[] oldFoxLines8;
    public string[] oldFoxLines9;
    public string[] oldFoxLines10;

    public string[] rabbitLines1;
    public string[] rabbitLines2;
    public string[] rabbitLines3;
    public string[] rabbitLines4;
    public string[] rabbitLines5;
    public string[] rabbitLines6;
    public string[] rabbitLines7;
    public string[] rabbitLines8;
    public string[] rabbitLines9;

    public GameObject oldFoxFace;
    public GameObject oldFoxSmilingFace;
    public GameObject rabbitFace;
    public GameObject rabbitSmilingFace;

    private Color32 originalAmbientColor;
    private Color32 targetColor;
    private bool colorChanging = false;

    void Awake()
    {
        eventInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        camSpeedX = cineCam.m_XAxis.m_MaxSpeed;
        camSpeedY = cineCam.m_YAxis.m_MaxSpeed;

        fade.SetActive(true);
        StartCoroutine("HideFade", 3f);

        if (playStartCutscene)
        {
            StartCoroutine("OldFox1");
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
            targetColor = new Color32(50, 50, 50, 255);
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
                npcTextObject.SetActive(false);
                textArrow.SetActive(false);

                if (lineIndex == speechLines.Length)
                {
                    switch (currentConv)
                    {
                        case "normal":
                            StartCoroutine("QuitTalk");
                            break;

                        case "oldFox1":
                            StartCoroutine("OldFox2");
                            break;

                        case "oldFox3":
                            StartCoroutine("OldFox4");
                            break;

                        case "oldFox4":
                            StartCoroutine("OldFox5");
                            break;

                        case "oldFox6":
                            StartCoroutine("OldFox7");
                            break;

                        case "oldFox8":
                            StartCoroutine("OldFox9");
                            break;

                        case "oldFox9":
                            StartCoroutine("OldFox10");
                            break;

                        case "rabbit1":
                            StartCoroutine("rabbit2");
                            break;

                        case "rabbit3":
                            StartCoroutine("rabbit4");
                            break;

                        case "rabbit4":
                            StartCoroutine("rabbit6");
                            break;

                        case "rabbit6":
                            StartCoroutine("rabbit7");
                            break;

                        case "rabbit8":
                            StartCoroutine("rabbit9");
                            break;

                        case "chase":
                            StartCoroutine("Chase");
                            break;

                        default:
                            StartCoroutine("QuitTalk");
                            break;
                    }
                }

                else
                {
                    StartCoroutine("NextLine");
                }
            }
        }

        if (choiceMade)
        {
            choiceMade = false;
            choice1.SetActive(false);
            choice2.SetActive(false);
            Cursor.visible = false;

            switch (currentConv)
            {
                case "oldFox2":
                    if (choiceValue == 1)
                        StartCoroutine("OldFox3");
                    else
                        StartCoroutine("OldFox5");
                    break;

                case "oldFox5":
                    StartCoroutine("OldFox6");
                    break;

                case "oldFox7":
                    StartCoroutine("OldFox8");
                    break;

                case "oldFox10":
                    StartCoroutine("OldFox11");
                    break;

                case "rabbit2":
                    StartCoroutine("rabbit3");
                    break;

                case "rabbit5":
                    StartCoroutine("rabbit6");
                    break;

                case "rabbit7":
                    StartCoroutine("rabbit8");
                    break;

                case "rabbit9":
                    StartCoroutine("QuitTalk");
                    break;

                default:
                    StartCoroutine("QuitTalk");
                    break;
            }
        }
    }

    IEnumerator OldFox1()
    {
        hpBar.SetActive(false);
        staminaBar.SetActive(false);
        PlayerManager.playerInstance.canMove = false;
        PlayerManager.playerInstance.noDrain = true;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;
        cineCam.m_XAxis.Value = 255f;
        cineCam.m_YAxis.Value = 0.3f;

        startRockCollider1.enabled = false;
        startRockCollider2.enabled = false;
        fox.transform.position = startPos.position;

        yield return new WaitForSeconds(2f);

        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = -1f;

        yield return new WaitForSeconds(1.5f);

        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        yield return new WaitForSeconds(1f);

        currentConv = "oldFox1";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = oldFoxLines1;
        oldFoxFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator OldFox2()
    {
        oldFoxFace.SetActive(false);
        npcTextField.SetActive(false);

        camTurnTime = 1f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = -170f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(1.5f);

        PlayerManager.playerInstance.x = -1f;
        PlayerManager.playerInstance.z = 1f;

        oldFox.GetComponent<OldFox>().Walk();

        yield return new WaitForSeconds(0.5f);

        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        yield return new WaitForSeconds(0.5f);

        oldFox.GetComponent<OldFox>().Stop();

        yield return new WaitForSeconds(0.5f);

        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        currentConv = "oldFox2";
        choiceText1.text = oldFoxLines2[0];
        choiceText2.text = oldFoxLines2[1];
        choice1.SetActive(true);
        choice2.SetActive(true);
        Cursor.visible = true;
    }

    IEnumerator OldFox3()
    {
        currentConv = "oldFox3";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = oldFoxLines3;
        oldFoxSmilingFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator OldFox4()
    {
        npcTextField.SetActive(false);
        oldFoxSmilingFace.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        oldFoxScarf.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        currentConv = "oldFox4";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = oldFoxLines4;
        oldFoxFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator OldFox5()
    {
        oldFoxFace.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        currentConv = "oldFox5";
        choiceText1.text = oldFoxLines5[0];
        choiceText2.text = oldFoxLines5[1];
        choice1.SetActive(true);
        choice2.SetActive(true);
        Cursor.visible = true;
    }

    IEnumerator OldFox6()
    {
        currentConv = "oldFox6";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = oldFoxLines6;
        oldFoxFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator OldFox7()
    {
        oldFoxFace.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        currentConv = "oldFox7";
        choiceText1.text = oldFoxLines7[0];
        choice1.SetActive(true);
        Cursor.visible = true;
    }

    IEnumerator OldFox8()
    {
        currentConv = "oldFox8";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = oldFoxLines8;
        oldFoxFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator OldFox9()
    {
        npcTextField.SetActive(false);
        oldFoxFace.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        oldFox.GetComponent<OldFox>().Walk();

        yield return new WaitForSeconds(0.2f);

        oldFox.GetComponent<OldFox>().Stop();

        yield return new WaitForSeconds(0.5f);

        oldFoxScarf.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        scarf.SetActive(true);

        currentConv = "oldFox9";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = oldFoxLines9;
        oldFoxSmilingFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator OldFox10()
    {
        oldFoxSmilingFace.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        currentConv = "oldFox10";
        choiceText1.text = oldFoxLines10[0];
        choice1.SetActive(true);
        Cursor.visible = true;
    }

    IEnumerator OldFox11()
    {
        npcTextField.SetActive(false);

        camTurnTime = 1f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = -285f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0.3f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(2f);

        PlayerManager.playerInstance.canMove = true;
        PlayerManager.playerInstance.noDrain = false;
        startRockCollider1.enabled = true;
        startRockCollider2.enabled = true;
        cineCam.m_XAxis.m_MaxSpeed = camSpeedX;
        cineCam.m_YAxis.m_MaxSpeed = camSpeedY;
        hpBar.SetActive(true);
        staminaBar.SetActive(true);
    }

    public IEnumerator Rabbit1()
    {
        hpBar.SetActive(false);
        staminaBar.SetActive(false);
        PlayerManager.playerInstance.canMove = false;
        PlayerManager.playerInstance.noDrain = true;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;

        currentConv = "rabbit1";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = rabbitLines1;
        rabbitFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Rabbit2()
    {
        rabbitFace.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        currentConv = "rabbit2";
        choiceText1.text = rabbitLines2[0];
        choiceText2.text = rabbitLines2[1];
        choice1.SetActive(true);
        choice2.SetActive(true);
        Cursor.visible = true;
    }

    IEnumerator Rabbit3()
    {
        currentConv = "rabbit3";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = rabbitLines3;
        rabbitFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Rabbit4()
    {
        rabbitFace.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        currentConv = "rabbit4";
        choiceText1.text = rabbitLines4[0];
        choice1.SetActive(true);
        Cursor.visible = true;
    }

    IEnumerator Rabbit5()
    {
        currentConv = "rabbit5";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = rabbitLines5;
        rabbitFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Rabbit6()
    {
        currentConv = "rabbit6";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = rabbitLines6;
        rabbitFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Rabbit7()
    {
        rabbitFace.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        currentConv = "rabbit7";
        choiceText1.text = rabbitLines7[0];
        choice1.SetActive(true);
        Cursor.visible = true;
    }

    IEnumerator Rabbit8()
    {
        currentConv = "rabbit8";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = rabbitLines8;
        rabbitFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Rabbit9()
    {
        rabbitFace.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        currentConv = "rabbit9";
        choiceText1.text = rabbitLines9[0];
        choiceText2.text = rabbitLines9[1];
        choice1.SetActive(true);
        choice2.SetActive(true);
        Cursor.visible = true;
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

        currentConv = "chase";
        npcText.text = "";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = newSpeechLines;

        StartCoroutine("NextLine");
    }

    public IEnumerator Chase()
    {
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
        npcTextObject.SetActive(true);

        for (int i = 0; i < currentLine.Length; i++)
        {
            npcText.text += currentLine[i];

            yield return new WaitForSeconds(0.02f);
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

    public void Choice1()
    {
        choiceValue = 1;
        choiceMade = true;
    }

    public void Choice2()
    {
        choiceValue = 2;
        choiceMade = true;
    }

    IEnumerator HideFade(float fadeTime)
    {
        yield return new WaitForSeconds(fadeTime);

        fade.SetActive(false);
    }
}
