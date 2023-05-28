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
    public GameObject snake;
    public Animator snakeAnimator;
    public Animator turtleAnimator;
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
    public bool startFromCheckPoint;

    public GameObject fade;
    public Animator fadeAnimator;

    public GameObject hpBar;
    public GameObject staminaBar;
    public GameObject npcTextField;
    public GameObject npcTextObject;
    public TextMeshProUGUI npcText;
    public GameObject textArrow;
    public GameObject choice1;
    public GameObject choice2;
    public GameObject choice3;
    public TextMeshProUGUI choiceText1;
    public TextMeshProUGUI choiceText2;
    public TextMeshProUGUI choiceText3;

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

    public string[] snakeLines1;
    public string[] snakeLines2;
    public string[] snakeLines3;
    public string[] snakeLines4;
    public string[] snakeLines5;
    public string[] snakeLines6;
    public string[] snakeLines7;
    public string[] snakeLines8;
    public string[] snakeLines9;
    public string[] snakeLines10;
    public string[] snakeLines11;
    public string[] snakeLines12;
    public string[] snakeLines13;

    public string[] turtleLines1;
    public string[] turtleLines2;
    public string[] turtleLines3;
    public string[] turtleLines4;
    public string[] turtleLines5;
    public string[] turtleLines6;
    public string[] turtleLines7;

    public GameObject oldFoxFace;
    public GameObject oldFoxSmilingFace;
    public GameObject rabbitFace;
    public GameObject rabbitSmilingFace;
    public GameObject snakeFace;
    public GameObject turtleFace;
    public GameObject turtleSmilingFace;

    [HideInInspector]
    public Color32 originalAmbientColor;
    [HideInInspector]
    public int lightOrDark;

    public GameObject respawn1;
    public GameObject respawn2;
    public GameObject respawn3;
    public GameObject respawn4;
    public GameObject cpCollider1;
    public GameObject cpCollider2;
    public GameObject cpCollider3;
    public GameObject cpCollider4;
    [HideInInspector]
    public int cpValue;
    [HideInInspector]
    public Vector3 respawnPos;

    void Awake()
    {
        eventInstance = this;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        camSpeedX = cineCam.m_XAxis.m_MaxSpeed;
        camSpeedY = cineCam.m_YAxis.m_MaxSpeed;

        if (!(PlayerPrefs.GetInt("cpValue") == 0) && startFromCheckPoint)
        {
            cpValue = PlayerPrefs.GetInt("cpValue");

            switch (cpValue)
            {
                case 1:
                    respawnPos = respawn1.transform.position;
                    cpCollider1.SetActive(false);
                    TimeManager.timeInstance.dayOnly = false;
                    break;

                case 2:
                    respawnPos = respawn2.transform.position;
                    cpCollider1.SetActive(false);
                    cpCollider2.SetActive(false);
                    TimeManager.timeInstance.dayOnly = false;
                    break;

                case 3:
                    respawnPos = respawn3.transform.position;
                    cpCollider1.SetActive(false);
                    cpCollider2.SetActive(false);
                    cpCollider3.SetActive(false);
                    TimeManager.timeInstance.dayOnly = true;
                    break;

                case 4:
                    respawnPos = respawn4.transform.position;
                    cpCollider1.SetActive(false);
                    cpCollider2.SetActive(false);
                    cpCollider3.SetActive(false);
                    cpCollider4.SetActive(false);
                    TimeManager.timeInstance.dayOnly = false;
                    break;

                default:
                    respawnPos = respawn1.transform.position;
                    cpCollider1.SetActive(false);
                    TimeManager.timeInstance.dayOnly = false;
                    break;
            }
        }

        else
        {
            cpValue = 0;

            if (playStartCutscene)
            {
                StartCoroutine("OldFox1");
            }

            else
            {
                scarf.SetActive(true);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("cpValue: " + cpValue);

        if (cpValue != 0)
        {
            fox.GetComponent<CharacterController>().enabled = false;
            fox.transform.position = respawnPos;
            fox.GetComponent<CharacterController>().enabled = true;
        }

        fade.SetActive(true);
        //StartCoroutine("HideFade", 3f);

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
            RenderSettings.ambientLight = new Color32(50, 50, 50, 255);
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
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
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
                            StartCoroutine("Rabbit2");
                            break;

                        case "rabbit3":
                            StartCoroutine("Rabbit4");
                            break;

                        case "rabbit5":
                            StartCoroutine("Rabbit6");
                            break;

                        case "rabbit6":
                            StartCoroutine("Rabbit7");
                            break;

                        case "rabbit8":
                            StartCoroutine("Rabbit9");
                            break;

                        case "snake1":
                            StartCoroutine("Snake2");
                            break;

                        case "snake3":
                            StartCoroutine("Snake4");
                            break;

                        case "snake5":
                            StartCoroutine("Snake6");
                            break;

                        case "snake6":
                            StartCoroutine("Snake7");
                            break;

                        case "snake8":
                            StartCoroutine("Snake9");
                            break;

                        case "snake10":
                            StartCoroutine("Snake12");
                            break;

                        case "snake11":
                            StartCoroutine("Snake12");
                            break;

                        case "snake12":
                            StartCoroutine("Snake13");
                            break;

                        case "turtle1":
                            StartCoroutine("Turtle2");
                            break;

                        case "turtle3":
                            StartCoroutine("Turtle4");
                            break;

                        case "turtle4":
                            StartCoroutine("Turtle5");
                            break;

                        case "turtle5":
                            StartCoroutine("Turtle6");
                            break;

                        case "turtle6":
                            StartCoroutine("Turtle7");
                            break;

                        case "turtle7":
                            StartCoroutine("Turtle8");
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
            choice3.SetActive(false);

            switch (currentConv)
            {
                case "oldFox2":
                    StartCoroutine("OldFox3");
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
                    if (choiceValue == 1)
                        StartCoroutine("Rabbit3");
                    else
                        StartCoroutine("Rabbit5");
                    break;

                case "rabbit4":
                    StartCoroutine("Rabbit6");
                    break;

                case "rabbit7":
                    StartCoroutine("Rabbit8");
                    break;

                case "rabbit9":
                    StartCoroutine("QuitTalk");
                    break;

                case "snake2":
                    StartCoroutine("Snake3");
                    break;

                case "snake4":
                    StartCoroutine("Snake5");
                    break;

                case "snake7":
                    StartCoroutine("Snake8");
                    break;

                case "snake9":
                    if (choiceValue == 2)
                        StartCoroutine("Snake11");
                    else
                        StartCoroutine("Snake10");
                    break;

                case "snake13":
                    StartCoroutine("Snake14");
                    break;

                case "turtle2":
                    StartCoroutine("Turtle3");
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

        yield return new WaitForSeconds(2.5f);

        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        yield return new WaitForSeconds(1f);

        currentConv = "oldFox1";
        npcTextField.SetActive(true);
        Cursor.visible = true;

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
        camTargetX = -160f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0.3f;
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
    }

    IEnumerator OldFox11()
    {
        npcTextField.SetActive(false);
        Cursor.visible = false;

        camTurnTime = 1f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = -275f;
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
        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        currentConv = "rabbit1";
        npcTextField.SetActive(true);
        Cursor.visible = true;

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
    }

    IEnumerator Snake1()
    {
        hpBar.SetActive(false);
        staminaBar.SetActive(false);
        PlayerManager.playerInstance.canMove = false;
        PlayerManager.playerInstance.noDrain = true;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;
        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        yield return new WaitForSeconds(1f);

        camTurnTime = 1f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = -10f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0.3f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(1.5f);

        snake.transform.position = fox.transform.position + new Vector3(0.2f, -0.15f, 1f);

        Vector3 direction = (snake.transform.position - fox.transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        snake.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        direction = (snake.transform.position - fox.transform.position).normalized;
        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        fox.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        snake.SetActive(true);

        yield return new WaitForSeconds(1f);

        snakeAnimator.SetTrigger("Tongue");

        yield return new WaitForSeconds(1f);

        currentConv = "snake1";
        npcTextField.SetActive(true);
        Cursor.visible = true;

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = snakeLines1;
        snakeFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Snake2()
    {
        snakeFace.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        currentConv = "snake2";
        choiceText1.text = snakeLines2[0];
        choice1.SetActive(true);
    }

    IEnumerator Snake3()
    {
        currentConv = "snake3";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = snakeLines3;
        snakeFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Snake4()
    {
        snakeFace.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        currentConv = "snake4";
        choiceText1.text = snakeLines4[0];
        choice1.SetActive(true);
    }

    IEnumerator Snake5()
    {
        currentConv = "snake5";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = snakeLines5;
        snakeFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Snake6()
    {
        npcTextField.SetActive(false);
        snakeFace.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        snake.GetComponent<Snake>().Walk();

        snakeAnimator.SetTrigger("Closer");

        yield return new WaitForSeconds(0.5f);

        snake.GetComponent<Snake>().Stop();

        snakeAnimator.SetTrigger("Tongue");

        yield return new WaitForSeconds(1f);

        currentConv = "snake6";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = snakeLines6;
        snakeFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Snake7()
    {
        snakeFace.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        currentConv = "snake7";
        choiceText1.text = snakeLines7[0];
        choice1.SetActive(true);
    }

    IEnumerator Snake8()
    {
        currentConv = "snake8";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = snakeLines8;
        snakeFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Snake9()
    {
        snakeFace.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        currentConv = "snake9";
        choiceText1.text = snakeLines9[0];
        choiceText2.text = snakeLines9[1];
        choiceText3.text = snakeLines9[2];
        choice1.SetActive(true);
        choice2.SetActive(true);
        choice3.SetActive(true);
    }

    IEnumerator Snake10()
    {
        currentConv = "snake10";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = snakeLines10;
        snakeFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Snake11()
    {
        currentConv = "snake11";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = snakeLines11;
        snakeFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Snake12()
    {
        currentConv = "snake12";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = snakeLines12;
        snakeFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Snake13()
    {
        snakeFace.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        currentConv = "snake13";
        choiceText1.text = snakeLines13[0];
        choiceText2.text = snakeLines13[1];
        choice1.SetActive(true);
        choice2.SetActive(true);
    }

    IEnumerator Snake14()
    {
        npcTextField.SetActive(false);
        Cursor.visible = false;
        snakeFace.SetActive(false);

        snakeAnimator.SetTrigger("Tongue");

        yield return new WaitForSeconds(0.5f);

        snakeAnimator.SetTrigger("Down");

        yield return new WaitForSeconds(1.5f);

        camTurnTime = 1f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = 45f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0.2f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(0.5f);

        snake.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        PlayerManager.playerInstance.canMove = true;
        PlayerManager.playerInstance.noDrain = false;
        cineCam.m_XAxis.m_MaxSpeed = camSpeedX;
        cineCam.m_YAxis.m_MaxSpeed = camSpeedY;
        hpBar.SetActive(true);
        staminaBar.SetActive(true);
    }

    IEnumerator Turtle1()
    {
        hpBar.SetActive(false);
        staminaBar.SetActive(false);
        PlayerManager.playerInstance.canMove = false;
        PlayerManager.playerInstance.noDrain = true;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;
        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        yield return new WaitForSeconds(1f);

        camTurnTime = 1f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = 90f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = -0.4f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(1f);

        PlayerManager.playerInstance.x = 1f;
        PlayerManager.playerInstance.z = 1f;

        yield return new WaitForSeconds(1f);

        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        yield return new WaitForSeconds(1f);

        currentConv = "turtle1";
        npcTextField.SetActive(true);
        Cursor.visible = true;

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = turtleLines1;
        turtleFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Turtle2()
    {
        turtleFace.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        currentConv = "turtle2";
        choiceText1.text = turtleLines2[0];
        choice1.SetActive(true);
    }

    IEnumerator Turtle3()
    {
        currentConv = "turtle3";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = turtleLines3;
        turtleFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Turtle4()
    {
        turtleFace.SetActive(false);
        currentConv = "turtle4";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = turtleLines4;

        StartCoroutine("NextLine");
    }

    IEnumerator Turtle5()
    {
        currentConv = "turtle5";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = turtleLines5;
        turtleFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Turtle6()
    {
        turtleFace.SetActive(false);
        currentConv = "turtle6";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = turtleLines6;

        StartCoroutine("NextLine");
    }

    IEnumerator Turtle7()
    {
        currentConv = "turtle7";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = turtleLines7;
        turtleFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Turtle8()
    {
        npcTextField.SetActive(false);
        Cursor.visible = false;
        turtleFace.SetActive(false);

        turtleAnimator.SetTrigger("Spell");

        yield return new WaitForSeconds(5f);

        fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(3f);
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
        Cursor.visible = true;

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = newSpeechLines;

        StartCoroutine("NextLine");
    }

    public IEnumerator Chase()
    {
        npcTextField.SetActive(false);
        Cursor.visible = false;

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
        Cursor.visible = false;

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

    public void NewCheckpoint()
    {
        cpValue++;
        PlayerPrefs.SetInt("cpValue", cpValue);

        switch (cpValue)
        {
            case 1:
                respawnPos = respawn1.transform.position;
                break;

            case 2:
                respawnPos = respawn2.transform.position;
                break;

            case 3:
                respawnPos = respawn3.transform.position;
                break;

            case 4:
                respawnPos = respawn4.transform.position;
                break;

            default:
                respawnPos = respawn1.transform.position;
                break;
        }
    }
}
