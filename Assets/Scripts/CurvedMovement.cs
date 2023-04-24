using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    private float speed;
    public float runSpeed;
    public float walkSpeed;
    public float gravity;
    public float jumpHeight;

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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // creates an invisible sphere at the bottom of the player to check if we are grounded so that we can reset gravity speed, isGrounded is true if sphere collides with ground layer, false if not
        isGrounded = Physics.CheckSphere(groundCheck.position, checkSphereRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            // a little less than zero apparently works better here
            velocity.y = groundedSpeedY;
        }

        // current angle for turn animations
        currentAngle = transform.eulerAngles.y;

        if (!turning)
        {
            // wasd/arrow inputs
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            direction = new Vector3(x, 0f, z).normalized;
        }

        if (direction.magnitude >= 0.1f)
        {
            // angle to the direction player wants to move
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            if (Mathf.Abs(targetAngle - dampedAngle) < 3f)
            {
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
                turning = false;
            }

            else
            {
                // smooth angle to avoid instant turn
                dampedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, dampedAngle, 0f);
                turning = true;
            }

            // get move direction from target angle
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        if (turning && (Mathf.Abs(targetAngle - dampedAngle) <= 2.5f || Mathf.Abs(targetAngle - dampedAngle) >= 357.5f))
        {
            turning = false;
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

        return;

        // turn animations
        if (!turning && Mathf.Abs(targetAngle - currentAngle) > 0.1f)
        {
            Debug.Log("turning");
            animator.SetLayerWeight(1, 1f);
            animator.SetLayerWeight(2, 1f);
            animator.SetLayerWeight(3, 1f);

            if ((targetAngle - currentAngle) < 0)
            {
                if (Mathf.Abs(targetAngle - currentAngle) < 45f)
                {
                    Debug.Log("head left");
                    animator.SetTrigger("HeadLeft");
                }

                else if (Mathf.Abs(targetAngle - currentAngle) < 90f)
                {
                    Debug.Log("head and neck left");
                    animator.SetTrigger("HeadLeft");
                    animator.SetTrigger("NeckLeft");
                }

                else
                {
                    Debug.Log("all left");
                    animator.SetTrigger("HeadLeft");
                    animator.SetTrigger("NeckLeft");
                    animator.SetTrigger("SpineLeft");
                }
            }

            else
            {
                if (Mathf.Abs(targetAngle - currentAngle) < 45f)
                {
                    Debug.Log("head right");
                    animator.SetTrigger("HeadRight");
                }

                else if (Mathf.Abs(targetAngle - currentAngle) < 90f)
                {
                    Debug.Log("neck right");
                    animator.SetTrigger("HeadRight");
                    animator.SetTrigger("NeckRight");
                }

                else
                {
                    Debug.Log("all right");
                    animator.SetTrigger("HeadRight");
                    animator.SetTrigger("NeckRight");
                    animator.SetTrigger("SpineRight");
                }
            }

            turning = true;
        }

        if (turning && Mathf.Abs(targetAngle - currentAngle) < 0.1f)
        {
            Debug.Log("turning ended");
            animator.SetLayerWeight(1, 0f);
            animator.SetLayerWeight(2, 0f);
            animator.SetLayerWeight(3, 0f);
            turning = false;
        }
    }
}