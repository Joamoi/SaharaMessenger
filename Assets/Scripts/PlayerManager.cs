using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager playerInstance;

    public CharacterController controller;
    public Transform cam;

    [HideInInspector]
    public bool canMove;
    private float speed;
    public float runSpeed;
    public float walkSpeed;
    public float gravity;
    public float jumpHeight;

    [HideInInspector]
    public float x;
    [HideInInspector]
    public float z;
    private float currentAngle;
    private float targetAngle;
    private float dampedAngle;
    private Vector3 direction;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private bool turning = false;

    public Transform groundCheck;
    public float checkSphereRadius;
    public float groundedSpeedY;
    public LayerMask groundMask;
    private Vector3 velocity;
    public static bool isGrounded;

    public Animator animator;

    private float hp = 100f;
    public GameObject hpMask;
    private float hp0PosX;
    private float hp100PosX;
    private float hpMax;
    public float damagePerHit = 10f;

    private float stamina = 100f;
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

    void Awake()
    {
        playerInstance = this;

        canMove = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        speed = walkSpeed;

        hp100PosX = hpMask.transform.position.x;
        hp0PosX = hp100PosX - 3.2f;
        hpMax = hp;

        stamina100PosX = staminaMask.transform.position.x;
        stamina0PosX = stamina100PosX - 3.2f;
        staminaMax = stamina;
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

        // current angle for turn animations
        currentAngle = transform.eulerAngles.y;

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
            dampedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, dampedAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        if (isGrounded)
        {
            // jump
            if (Input.GetButtonDown("Jump"))
            {
                // v = sqrt(-2hg)
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

                animator.SetTrigger("Jump");
            }

            // sprint
            if (Input.GetKey(KeyCode.LeftShift))
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

        // walk and run animations
        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("Walking", true);

            if (Input.GetKey(KeyCode.LeftShift))
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

        // HP STAMINA FOOD REST

        // stamina drain
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

        if (stamina < 0)
        {
            stamina = 0;
        }

        // update hp and stamina bars
        float newHPPosX = Mathf.Lerp(hp100PosX, hp0PosX, (hpMax - hp) / hpMax);
        hpMask.transform.position = new Vector3(newHPPosX, hpMask.transform.position.y, hpMask.transform.position.z);

        float newStaminaPosX = Mathf.Lerp(stamina100PosX, stamina0PosX, (staminaMax - stamina) / staminaMax);
        staminaMask.transform.position = new Vector3(newStaminaPosX, staminaMask.transform.position.y, staminaMask.transform.position.z);
    }

    public void TakeDamage()
    {
        hp -= damagePerHit;

        if (hp <= 0)
        {
            hp = 0;
        }
    }

    public void Eat()
    {
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
}