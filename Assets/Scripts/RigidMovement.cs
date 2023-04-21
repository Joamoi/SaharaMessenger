using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    private float speed;
    public float runSpeed;
    public float walkSpeed;
    public float gravity;
    public float jumpHeight;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

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

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, 0f, z).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

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

        if (z > 0)
        {
            animator.SetBool("Walking", true);
        }

        else
        {
            animator.SetBool("Walking", false);
        }
    }
}
