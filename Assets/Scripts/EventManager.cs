using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.Audio;

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
    public GameObject peekJackal1;
    public GameObject peekJackal2;
    public GameObject peekJackal3;
    public Animator peekAnimator1;
    public Animator peekAnimator2;
    public Animator peekAnimator3;
    public GameObject chaseJackal1;
    public GameObject chaseJackal2;
    public GameObject chaseJackal3;
    public GameObject chaseSandstorm;
    public Collider startRockCollider1;
    public Collider startRockCollider2;
    public Transform startPos;
    public CinemachineFreeLook cineCam;
    public GameObject camTarget;
    public GameObject mainCam;
    public GameObject snakeCollider;
    public GameObject chaseCollider;
    public GameObject chaseEndCollider;
    public GameObject sandstormEndCollider;
    public GameObject peek1Collider;
    public Transform turtleCamPos;
    public Transform endCamPos1;
    public Transform endCamPos2;
    public Transform endCamPos3;
    public Transform endCamPos4;
    public Transform endCamPos5;
    public Transform endCamPos6;
    private bool endCamMoving = false;
    private Vector3 endCamStartPos;
    private Vector3 endCamEndPos;
    private float endCamTimer;
    private float endCamDuration;
    public GameObject spell1;
    public GameObject spell2;
    public GameObject rain1;
    public GameObject rain2;
    public GameObject rain3;
    public GameObject endMenu;

    public bool playStartCutscene;
    public bool startFromCheckPoint;

    public GameObject fade;
    public Animator fadeAnimator;

    public GameObject uiObjects;
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

    [HideInInspector]
    public float camSpeedX;
    [HideInInspector]
    public float camSpeedY;
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

    public string[] chaseLines1;
    public string[] chaseLines2;
    public string[] chaseLines3;
    public string[] chaseLines4;
    public string[] chaseLines5;

    public string[] sheepLines;
    public string[] jackalLines;
    public string[] bunniesLines;

    public GameObject oldFoxFace;
    public GameObject oldFoxSmilingFace;
    public GameObject rabbitFace;
    public GameObject rabbitSmilingFace;
    public GameObject snakeFace;
    public GameObject snakeGrinFace;
    public GameObject turtleFace;
    public GameObject turtleSmilingFace;
    public GameObject jackalFace;
    public GameObject jackalGrinFace;
    public GameObject sheepFace;
    public GameObject chunkyJackalFace;
    public GameObject jackalsFace;

    [HideInInspector]
    public Color32 originalAmbientColor;
    [HideInInspector]
    public int lightOrDark;

    public GameObject respawn1;
    public GameObject respawn2;
    public GameObject respawn3;
    public GameObject respawn4;
    public GameObject respawn5;
    public GameObject respawn6;
    public GameObject cpCollider1;
    public GameObject cpCollider2;
    public GameObject cpCollider3;
    public GameObject cpCollider4;
    public GameObject cpCollider5;
    public GameObject cpCollider6;
    [HideInInspector]
    public int cpValue;
    [HideInInspector]
    public Vector3 respawnPos;

    public AudioSource normalMusic;
    public AudioSource chaseMusic;
    public AudioSource oldFoxSound;
    public AudioSource jackalSound;
    public AudioSource snakeSound;
    public AudioSource turtleSound;
    public AudioSource sheepSound;
    public AudioSource snakeRisesSound;
    public AudioSource rainSound;
    public AudioSource spellSound;
    public AudioSource jackalPeekSound;
    public AudioSource jackalHowlSound;

    void Awake()
    {
        eventInstance = this;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        if (!(PlayerPrefs.GetFloat("camSpeed") == 0))
        {
            camSpeedX = 75f * PlayerPrefs.GetFloat("camSpeed");
            camSpeedY = PlayerPrefs.GetFloat("camSpeed");

            cineCam.m_XAxis.m_MaxSpeed = camSpeedX;
            cineCam.m_YAxis.m_MaxSpeed = camSpeedY;
        }

        else
        {
            camSpeedX = cineCam.m_XAxis.m_MaxSpeed;
            camSpeedY = cineCam.m_YAxis.m_MaxSpeed;
        }

        if (!(PlayerPrefs.GetInt("cpValue") == 0) && startFromCheckPoint)
        {
            cpValue = PlayerPrefs.GetInt("cpValue");

            switch (cpValue)
            {
                case 1:
                    respawnPos = respawn1.transform.position;
                    cpCollider1.SetActive(false);
                    StartCoroutine("StartDay");
                    break;

                case 2:
                    respawnPos = respawn2.transform.position;
                    cpCollider1.SetActive(false);
                    cpCollider2.SetActive(false);
                    StartCoroutine("StartNight");
                    break;

                case 3:
                    respawnPos = respawn3.transform.position;
                    cpCollider1.SetActive(false);
                    cpCollider2.SetActive(false);
                    cpCollider3.SetActive(false);
                    StartCoroutine("StartDay");
                    break;

                case 4:
                    respawnPos = respawn4.transform.position;
                    cpCollider1.SetActive(false);
                    cpCollider2.SetActive(false);
                    cpCollider3.SetActive(false);
                    cpCollider4.SetActive(false);
                    StartCoroutine("StartDay");
                    break;

                case 5:
                    respawnPos = respawn5.transform.position;
                    cpCollider1.SetActive(false);
                    cpCollider2.SetActive(false);
                    cpCollider3.SetActive(false);
                    cpCollider4.SetActive(false);
                    cpCollider5.SetActive(false);
                    snakeCollider.SetActive(false);
                    StartCoroutine("StartNight");
                    break;

                case 6:
                    respawnPos = respawn6.transform.position;
                    cpCollider1.SetActive(false);
                    cpCollider2.SetActive(false);
                    cpCollider3.SetActive(false);
                    cpCollider4.SetActive(false);
                    cpCollider5.SetActive(false);
                    cpCollider6.SetActive(false);
                    snakeCollider.SetActive(false);
                    chaseCollider.SetActive(false);
                    chaseEndCollider.SetActive(false);
                    sandstormEndCollider.SetActive(false);
                    StartCoroutine("StartDay");
                    break;

                default:
                    respawnPos = respawn1.transform.position;
                    cpCollider1.SetActive(false);
                    break;
            }
        }

        else
        {
            cpValue = 0;
            StartCoroutine("StartDay");

            if (playStartCutscene)
            {
                StartCoroutine("OldFox1");
            }

            else
            {
                scarf.SetActive(true);
                normalMusic.Play();
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
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    RenderSettings.ambientLight = originalAmbientColor;
        //}

        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    RenderSettings.ambientLight = new Color32(50, 50, 50, 255);
        //}

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

        if (endCamMoving)
        {
            endCamTimer += Time.deltaTime;
            mainCam.transform.position = Vector3.Lerp(endCamStartPos, endCamEndPos, endCamTimer / endCamDuration);
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

                        case "chase1":
                            StartCoroutine("Chase2");
                            break;

                        case "chase3":
                            StartCoroutine("Chase4");
                            break;

                        case "chase4":
                            StartCoroutine("Chase5");
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

                case "chase2":
                    StartCoroutine("Chase3");
                    break;

                case "chase5":
                    StartCoroutine("Chase");
                    break;

                default:
                    StartCoroutine("QuitTalk");
                    break;
            }
        }
    }

    IEnumerator OldFox1()
    {
        uiObjects.SetActive(false);
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

        oldFoxSound.Play();

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
        uiObjects.SetActive(true);

        normalMusic.Play();
    }

    public IEnumerator Rabbit1()
    {
        uiObjects.SetActive(false);
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
        rabbitSmilingFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Rabbit2()
    {
        rabbitSmilingFace.SetActive(false);

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
        rabbitSmilingFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Rabbit6()
    {
        currentConv = "rabbit6";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = rabbitLines6;
        rabbitSmilingFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Rabbit7()
    {
        rabbitSmilingFace.SetActive(false);

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
        uiObjects.SetActive(false);
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
        camTargetY = -0.2f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(1.5f);

        snake.transform.position = fox.transform.position + new Vector3(0.2f, -0.15f, 1.5f);

        Vector3 direction = (snake.transform.position - fox.transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        snake.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        direction = (snake.transform.position - fox.transform.position).normalized;
        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        fox.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        snake.SetActive(true);

        snakeRisesSound.Play();
        //snakeSound.Play();

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
        snakeGrinFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Snake7()
    {
        snakeGrinFace.SetActive(false);

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
        uiObjects.SetActive(true);
    }

    IEnumerator Turtle1()
    {
        uiObjects.SetActive(false);
        PlayerManager.playerInstance.canMove = false;
        PlayerManager.playerInstance.noDrain = true;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;
        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        normalMusic.Stop();

        yield return new WaitForSeconds(1f);

        camTurnTime = 0.5f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = 120f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(1f);

        PlayerManager.playerInstance.z = 1f;

        yield return new WaitForSeconds(3f);

        PlayerManager.playerInstance.z = 0f;

        yield return new WaitForSeconds(1f);

        cineCam.gameObject.SetActive(false);
        
        mainCam.transform.position = turtleCamPos.position;
        mainCam.transform.eulerAngles = turtleCamPos.eulerAngles;
        mainCam.GetComponent<Camera>().fieldOfView = 60f;

        yield return new WaitForSeconds(1f);

        turtleSound.Play();

        currentConv = "turtle1";
        npcTextField.SetActive(true);
        Cursor.visible = true;

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = turtleLines1;
        turtleSmilingFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Turtle2()
    {
        turtleSmilingFace.SetActive(false);

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
        spellSound.Play();

        yield return new WaitForSeconds(1f);

        spell1.SetActive(true);
        spell2.SetActive(true);

        yield return new WaitForSeconds(5f);

        fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(3f);

        spell1.SetActive(false);
        spell2.SetActive(false);
        StartCoroutine("EndScene");
    }

    IEnumerator EndScene()
    {
        mainCam.transform.position = endCamPos1.position;
        mainCam.transform.eulerAngles = endCamPos1.eulerAngles;
        endCamStartPos = endCamPos1.position;
        endCamEndPos = endCamPos2.position;

        endCamTimer = 0f;
        endCamDuration = 8f;
        endCamMoving = true;

        RenderSettings.ambientLight = originalAmbientColor;

        fadeAnimator.SetTrigger("FadeIn");
        rain1.SetActive(true);
        rainSound.Play();

        yield return new WaitForSeconds(7f);

        fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(2f);

        endCamMoving = false;
        rainSound.Stop();
        rain1.SetActive(false);

        mainCam.transform.position = endCamPos3.position;
        mainCam.transform.eulerAngles = endCamPos3.eulerAngles;
        endCamStartPos = endCamPos3.position;
        endCamEndPos = endCamPos4.position;

        endCamTimer = 0f;
        endCamDuration = 8f;

        rainSound.Play();
        rain2.SetActive(true);

        yield return new WaitForSeconds(1f);

        endCamMoving = true;

        fadeAnimator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(7f);

        fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(2f);

        endCamMoving = false;
        rainSound.Stop();
        rain2.SetActive(false);

        mainCam.transform.position = endCamPos5.position;
        mainCam.transform.eulerAngles = endCamPos5.eulerAngles;
        endCamStartPos = endCamPos5.position;
        endCamEndPos = endCamPos6.position;

        endCamTimer = 0f;
        endCamDuration = 8f;

        rainSound.Play();
        rain3.SetActive(true);

        yield return new WaitForSeconds(1f);

        endCamMoving = true;

        fadeAnimator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(7f);

        fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(3f);

        rain3.SetActive(false);
        rainSound.Stop();
        endCamMoving = false;

        PlayerPrefs.SetInt("cpValue", 0);
        endMenu.SetActive(true);
    }

    public IEnumerator EnemyPeek1()
    {
        uiObjects.SetActive(false);
        PlayerManager.playerInstance.canMove = false;
        PlayerManager.playerInstance.noDrain = true;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;

        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        peekJackal1.SetActive(true);

        yield return new WaitForSeconds(1f);

        camTurnTime = 1.5f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = 90f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(1f);

        jackalPeekSound.Play();

        yield return new WaitForSeconds(1.5f);

        peekAnimator1.SetTrigger("Peek");

        yield return new WaitForSeconds(1f);

        peekJackal1.SetActive(false);

        camTurnTime = 1f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = 0f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0.3f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(1.5f);

        peekJackal1.SetActive(false);

        PlayerManager.playerInstance.canMove = true;
        PlayerManager.playerInstance.noDrain = false;
        cineCam.m_XAxis.m_MaxSpeed = camSpeedX;
        cineCam.m_YAxis.m_MaxSpeed = camSpeedY;
        uiObjects.SetActive(true);
    }

    public IEnumerator EnemyPeek2()
    {
        uiObjects.SetActive(false);
        PlayerManager.playerInstance.canMove = false;
        PlayerManager.playerInstance.noDrain = true;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;

        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        peekJackal2.SetActive(true);
        peekJackal3.SetActive(true);

        yield return new WaitForSeconds(1f);

        camTurnTime = 1.5f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = 120f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = -0.4f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(1f);

        jackalPeekSound.Play();

        yield return new WaitForSeconds(1.5f);

        peekAnimator2.SetTrigger("Peek");

        yield return new WaitForSeconds(0.5f);

        peekAnimator3.SetTrigger("Peek");

        yield return new WaitForSeconds(1f);

        peekJackal2.SetActive(false);
        peekJackal3.SetActive(false);

        camTurnTime = 1f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = 0f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0.3f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(1.5f);

        peekJackal2.SetActive(false);

        PlayerManager.playerInstance.canMove = true;
        PlayerManager.playerInstance.noDrain = false;
        cineCam.m_XAxis.m_MaxSpeed = camSpeedX;
        cineCam.m_YAxis.m_MaxSpeed = camSpeedY;
        uiObjects.SetActive(true);
    }

    public IEnumerator EnemyChaseTalk()
    {
        uiObjects.SetActive(false);
        PlayerManager.playerInstance.canMove = false;
        PlayerManager.playerInstance.noDrain = true;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;

        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        chaseJackal1.transform.position = fox.transform.position + new Vector3(-9f, 10f, -9f);
        chaseJackal2.transform.position = fox.transform.position + new Vector3(-10f, 10f, -5f);
        chaseJackal3.transform.position = fox.transform.position + new Vector3(-5f, 10f, -10f);

        chaseJackal1.SetActive(true);
        chaseJackal2.SetActive(true);
        chaseJackal3.SetActive(true);

        yield return new WaitForSeconds(1f);

        normalMusic.Stop();

        camTurnTime = 1f;
        camStartX = cineCam.m_XAxis.Value;
        camTargetX = 225f;
        camStartY = cineCam.m_YAxis.Value;
        camTargetY = 0.4f;
        lerpFloat = 0f;
        camTurning = true;

        yield return new WaitForSeconds(1f);

        PlayerManager.playerInstance.z = 1f;

        yield return new WaitForSeconds(0.7f);

        PlayerManager.playerInstance.z = 0f;

        yield return new WaitForSeconds(0.5f);

        jackalSound.Play();

        currentConv = "chase1";
        npcTextField.SetActive(true);
        Cursor.visible = true;

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = chaseLines1;
        jackalFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Chase2()
    {
        jackalFace.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        chaseJackal1.GetComponent<Jackal>().StartCoroutine("StartWalk");
        chaseJackal2.GetComponent<Jackal>().StartCoroutine("StartWalk");
        chaseJackal3.GetComponent<Jackal>().StartCoroutine("StartWalk");

        yield return new WaitForSeconds(1f);

        chaseJackal1.GetComponent<Jackal>().StartCoroutine("StopWalk");
        chaseJackal2.GetComponent<Jackal>().StartCoroutine("StopWalk");
        chaseJackal3.GetComponent<Jackal>().StartCoroutine("StopWalk");

        yield return new WaitForSeconds(0.5f);

        yield return new WaitForSeconds(0.2f);

        currentConv = "chase2";
        choiceText1.text = chaseLines2[0];
        choice1.SetActive(true);
    }

    IEnumerator Chase3()
    {
        currentConv = "chase3";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = chaseLines3;
        jackalGrinFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Chase4()
    {
        jackalGrinFace.SetActive(false);
        currentConv = "chase4";
        npcTextField.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = chaseLines4;
        jackalsFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    IEnumerator Chase5()
    {
        jackalsFace.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        currentConv = "chase5";
        choiceText1.text = chaseLines5[0];
        choice1.SetActive(true);
    }

    public IEnumerator Chase()
    {
        jackalHowlSound.Play();

        yield return new WaitForSeconds(2f);

        npcTextField.SetActive(false);
        Cursor.visible = false;

        chaseJackal1.GetComponent<Jackal>().StartCoroutine("StartChase");
        chaseJackal2.GetComponent<Jackal>().StartCoroutine("StartChase");
        chaseJackal3.GetComponent<Jackal>().StartCoroutine("StartChase");

        yield return new WaitForSeconds(0.5f);

        //camTurnTime = 0.3f;
        //camStartX = cineCam.m_XAxis.Value;
        //camTargetX = 0f;
        //camStartY = cineCam.m_YAxis.Value;
        //camTargetY = 0.3f;
        //lerpFloat = 0f;
        //camTurning = true;

        //yield return new WaitForSeconds(0.4f);

        chaseMusic.Play();

        PlayerManager.playerInstance.canMove = true;
        PlayerManager.playerInstance.noDrain = false;
        cineCam.m_XAxis.m_MaxSpeed = camSpeedX;
        cineCam.m_YAxis.m_MaxSpeed = camSpeedY;
        uiObjects.SetActive(true);
    }

    public IEnumerator ChaseSandstorm()
    {
        chaseSandstorm.SetActive(true);

        yield return new WaitForSeconds(3f);

        chaseSandstorm.GetComponent<BoxCollider>().enabled = true;

        yield return new WaitForSeconds(1f);

        chaseJackal1.GetComponent<Jackal>().StopChase();
        chaseJackal2.GetComponent<Jackal>().StopChase();
        chaseJackal3.GetComponent<Jackal>().StopChase();
    }

    public void SandstormEnd()
    {
        chaseMusic.Stop();
        normalMusic.Play();

        //chaseJackal1.SetActive(false);
        //chaseJackal2.SetActive(false);
        //chaseJackal3.SetActive(false);
    }

    public IEnumerator Sheep()
    {
        uiObjects.SetActive(false);
        PlayerManager.playerInstance.canMove = false;
        PlayerManager.playerInstance.noDrain = true;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;
        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        sheepSound.Play();

        currentConv = "normal";
        npcTextField.SetActive(true);
        Cursor.visible = true;

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = sheepLines;
        sheepFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    public IEnumerator Jackal()
    {
        uiObjects.SetActive(false);
        PlayerManager.playerInstance.canMove = false;
        PlayerManager.playerInstance.noDrain = true;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;
        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        jackalSound.Play();

        currentConv = "normal";
        npcTextField.SetActive(true);
        Cursor.visible = true;

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = jackalLines;
        chunkyJackalFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    public IEnumerator Bunnies()
    {
        uiObjects.SetActive(false);
        PlayerManager.playerInstance.canMove = false;
        PlayerManager.playerInstance.noDrain = true;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;
        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        currentConv = "normal";
        npcTextField.SetActive(true);
        Cursor.visible = true;

        yield return new WaitForSeconds(0.2f);

        lineIndex = 0;
        speechLines = bunniesLines;
        rabbitFace.SetActive(true);

        StartCoroutine("NextLine");
    }

    public IEnumerator Talk(string[] newSpeechLines)
    {
        uiObjects.SetActive(false);
        PlayerManager.playerInstance.canMove = false;
        PlayerManager.playerInstance.noDrain = true;
        cineCam.m_XAxis.m_MaxSpeed = 0f;
        cineCam.m_YAxis.m_MaxSpeed = 0f;
        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        npcTextField.SetActive(true);
        Cursor.visible = true;

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
        sheepFace.SetActive(false);
        chunkyJackalFace.SetActive(false);
        rabbitFace.SetActive(false);

        npcTextField.SetActive(false);
        Cursor.visible = false;

        yield return new WaitForSeconds(0.5f);

        uiObjects.SetActive(true);
        PlayerManager.playerInstance.canMove = true;
        PlayerManager.playerInstance.noDrain = false;
        cineCam.m_XAxis.m_MaxSpeed = camSpeedX;
        cineCam.m_YAxis.m_MaxSpeed = camSpeedY;
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

    public void Choice3()
    {
        choiceValue = 3;
        choiceMade = true;
    }

    IEnumerator HideFade(float fadeTime)
    {
        yield return new WaitForSeconds(fadeTime);

        fade.SetActive(false);
    }

    public void NewCheckpoint(int number)
    {
        cpValue = number;
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

            case 5:
                respawnPos = respawn5.transform.position;
                break;

            case 6:
                respawnPos = respawn6.transform.position;
                break;

            default:
                respawnPos = respawn1.transform.position;
                break;
        }
    }

    public IEnumerator StartDay()
    {
        yield return new WaitForSeconds(0.1f);

        TimeManager.timeInstance.StartCoroutine("Day");
    }

    public IEnumerator StartNight()
    {
        yield return new WaitForSeconds(0.1f);

        TimeManager.timeInstance.StartCoroutine("Night");
    }
}
