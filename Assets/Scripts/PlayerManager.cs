using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager playerInstance;

    public CharacterController controller;
    public Transform cam;
    [HideInInspector]
    public bool gameIsPaused = false;

    [HideInInspector]
    public bool canMove;
    [HideInInspector]
    public bool canRun;
    [HideInInspector]
    public bool inDust;
    [HideInInspector]
    public bool dead = false;
    [HideInInspector]
    public bool noDrain = false;
    private float speed;
    public float runSpeed;
    public float walkSpeed;
    public float gravity;
    public float jumpHeight;

    [HideInInspector]
    public float x;
    [HideInInspector]
    public float z;
    private float targetAngle;
    private float dampedAngle;
    [HideInInspector]
    public Vector3 direction;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    public float turnSmoothTime2 = 0.15f;
    private float turnSmoothVelocity2;
    public float rotSpeed1;
    public float rotSpeed2;

    public Transform rotationSource;

    public Transform groundCheck;
    public float checkSphereRadius;
    public float groundedSpeedY;
    public LayerMask groundMask;
    private Vector3 velocity;
    [HideInInspector]
    public bool isGrounded;
    [HideInInspector]
    public bool inAir = false;


    public Animator animator;
    private float idleTime;
    private float idleSwitchTime;

    [HideInInspector]
    public float hp = 100f;
    public GameObject hpMask;
    private float hp0PosX;
    private float hp100PosX;
    private float hpMax;
    public float damagePerHit = 10f;

    [HideInInspector]
    public float stamina = 100f;
    public GameObject staminaMask;
    private float stamina0PosX;
    private float stamina100PosX;
    private float staminaMax;
    public float idleDrain = 0.5f;
    public float walkDrain = 1f;
    public float runDrain = 3f;


    public float healHPPerFood = 50f;
    public float healStaminaPerFood = 50f;
    public float healHPPerDrink = 50f;
    public float healStaminaPerDrink = 50f;
    public float healHPPerRest = 100f;
    public float healStaminaPerRest = 100f;

    public GameObject eatText;
    public GameObject drinkText;
    public GameObject talkText;
    public GameObject restText;

    public GameObject chaseJackal1;
    public GameObject chaseJackal2;
    public GameObject chaseJackal3;

    void Awake()
    {
        playerInstance = this;

        canMove = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = walkSpeed;

        hp100PosX = hpMask.transform.position.x;
        hp0PosX = hp100PosX - 3.2f;
        hpMax = hp;

        stamina100PosX = staminaMask.transform.position.x;
        stamina0PosX = stamina100PosX - 3.2f;
        staminaMax = stamina;

        idleSwitchTime = Random.Range(8f, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        // MOVEMENT

        // creates an invisible sphere at the bottom of the player to check if we are grounded so that we can reset gravity speed, isGrounded is true if sphere collides with ground layer, false if not
        isGrounded = Physics.CheckSphere(groundCheck.position, checkSphereRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            // a little less than zero apparently works better here
            velocity.y = groundedSpeedY;
        }

        if (canMove)
        {
            // wasd/arrow inputs
            x = Input.GetAxisRaw("Horizontal");
            z = Input.GetAxisRaw("Vertical");
        }

        direction = new Vector3(x, 0f, z).normalized;

        if (direction.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float currentAngle = transform.eulerAngles.y;

            if (Mathf.Abs(targetAngle - currentAngle) > 170f)
            {
                // takaosan py�r�hdys nopeaksi t�m�n sijaan?
                targetAngle -= 2f;
            }

            //dampedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            //float dampedTurnAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity2, turnSmoothTime2);
            //rotationSource.rotation = Quaternion.Euler(0f, dampedTurnAngle, 0f);

            //transform.rotation = Quaternion.Euler(0f, dampedAngle, 0f);

            rotationSource.rotation = Quaternion.Lerp(rotationSource.rotation, Quaternion.Euler(0f, targetAngle, 0f), rotSpeed2 * Time.deltaTime);

            // targetiksi rotationsource.rotation?
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, targetAngle, 0f), rotSpeed1 * Time.deltaTime);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);

            //float currentAngle = transform.eulerAngles.y;
            //float angleLeft = targetAngle - currentAngle;
            //float turnAngle = angleLeft;
            //rotationSource.localRotation = Quaternion.Euler(0f, turnAngle, 0f);
        }

        if (isGrounded)
        {
            // jump
            if (Input.GetButtonDown("Jump"))
            {
                // v = sqrt(-2hg)
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

                animator.SetTrigger("Jump");
                StartCoroutine("InAir");
            }

            // sprint
            if (Input.GetKey(KeyCode.LeftShift) && canRun)
            {
                speed = runSpeed;
            }
            else
            {
                speed = walkSpeed;
            }
        }

        // gravity
        // we need to multiply with time twice because y=0.5*g*t^2
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // animations
        idleTime += Time.deltaTime;

        if (direction.magnitude >= 0.1f)
        {
            idleTime = 0f;
            animator.SetBool("Walking", true);

            if (Input.GetKey(KeyCode.LeftShift) && canRun)
            {
                animator.SetBool("Running", true);
            }
            else
            {
                animator.SetBool("Running", false);
            }
        }

        else
        {
            animator.SetBool("Running", false);
            animator.SetBool("Walking", false);
        }

        if (idleTime >= idleSwitchTime)
        {
            Debug.Log("time = " + idleTime);
            Debug.Log("switch time = " + idleSwitchTime);
            animator.SetTrigger("IdleSwitch");
            idleTime = 0f;
            idleSwitchTime = Random.Range(8f, 20f);
        }

        if (inAir && isGrounded)
        {
            animator.SetTrigger("Landing");
            inAir = false;
        }

        // stamina drain

        if (!noDrain)
        {
            if (direction.magnitude >= 0.1f)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (stamina > 0)
                    {
                        stamina -= runDrain * Time.deltaTime * TimeManager.timeDrainMultiplier;
                    }

                    else
                    {
                        hp -= runDrain * Time.deltaTime * TimeManager.timeDrainMultiplier;
                    }
                }
                else
                {
                    if (stamina > 0)
                    {
                        stamina -= walkDrain * Time.deltaTime * TimeManager.timeDrainMultiplier;
                    }

                    else
                    {
                        hp -= walkDrain * Time.deltaTime * TimeManager.timeDrainMultiplier;
                    }
                }
            }

            else
            {
                if (stamina > 0)
                {
                    stamina -= idleDrain * Time.deltaTime * TimeManager.timeDrainMultiplier;
                }

                else
                {
                    hp -= idleDrain * Time.deltaTime * TimeManager.timeDrainMultiplier;
                }
            }
        }

        if (stamina <= 0)
        {
            stamina = 0;
            canRun = false;
        }

        else if (inDust)
        {
            canRun = false;
        }

        else
        {
            canRun = true;
        }

        if (hp <= 0 && !dead)
        {
            dead = true;
            hp = 0;
            StartCoroutine("Death");
        }

        // update hp and stamina bars
        float newHPPosX = Mathf.Lerp(hp100PosX, hp0PosX, (hpMax - hp) / hpMax);
        hpMask.transform.position = new Vector3(newHPPosX, hpMask.transform.position.y, hpMask.transform.position.z);

        float newStaminaPosX = Mathf.Lerp(stamina100PosX, stamina0PosX, (staminaMax - stamina) / staminaMax);
        staminaMask.transform.position = new Vector3(newStaminaPosX, staminaMask.transform.position.y, staminaMask.transform.position.z);
    }

    public void Eat()
    {
        animator.SetTrigger("Eat");
        x = 0f;
        z = 0f;
        canMove = false;
        StartCoroutine("EatDontMove");

        hp += healHPPerFood;
        stamina += healStaminaPerFood;

        if (hp > 100)
        {
            hp = 100;
        }

        if (stamina > 100)
        {
            stamina = 100;
        }

        eatText.SetActive(false);
    }

    public void Drink()
    {
        animator.SetTrigger("Eat");
        x = 0f;
        z = 0f;
        canMove = false;
        StartCoroutine("EatDontMove");

        hp += healHPPerDrink;
        stamina += healStaminaPerDrink;

        if (hp > 100)
        {
            hp = 100;
        }

        if (stamina > 100)
        {
            stamina = 100;
        }

        drinkText.SetActive(false);
    }

    IEnumerator EatDontMove()
    {
        yield return new WaitForSeconds(2.5f);
        canMove = true;
    }

    public void Rest()
    {
        hp += healHPPerRest;
        stamina += healStaminaPerRest;

        if (hp > 100)
        {
            hp = 100;
        }

        if (stamina > 100)
        {
            stamina = 100;
        }
    }

    public IEnumerator Death()
    {
        chaseJackal1.GetComponent<Jackal>().StopChase();
        chaseJackal2.GetComponent<Jackal>().StopChase();
        chaseJackal3.GetComponent<Jackal>().StopChase();

        x = 0f;
        z = 0f;
        canMove = false;
        animator.SetBool("Running", false);
        animator.SetBool("Walking", false);
        animator.SetTrigger("Death");

        yield return new WaitForSeconds(3f);

        EventManager.eventInstance.fadeAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(2f);

        hp = 100f;
        stamina = 100f;

        animator.SetTrigger("Respawn");

        yield return new WaitForSeconds(1f);

        gameObject.GetComponent<CharacterController>().enabled = false;
        transform.position = EventManager.eventInstance.respawnPos;
        gameObject.GetComponent<CharacterController>().enabled = true;

        // reset anything necessary: hide jackals, fix dropfloor, heal, ...

        EventManager.eventInstance.fadeAnimator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(2f);

        dead = false;
        canMove = true;
    }

    IEnumerator InAir()
    {
        yield return new WaitForSeconds(0.5f);

        inAir = true;
    }
}