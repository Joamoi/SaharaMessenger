using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jackal : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public GameObject fox;
    private bool chasing = false;
    private bool walking = false;
    private float speed;
    public float walkSpeed;
    public float runSpeed;
    public LayerMask playerLayer;
    public LayerMask enemyLayer;
    public float startDelay;

    private Vector3 direction;
    private float targetAngle;
    private Vector3 velocity;
    public Transform groundCheck;
    public LayerMask groundMask;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        controller.detectCollisions = false;

        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // creates an invisible sphere at the bottom of the player to check if we are grounded so that we can reset gravity speed, isGrounded is true if sphere collides with ground layer, false if not
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            // a little less than zero apparently works better here
            velocity.y = -3f;
        }

        direction = (fox.transform.position - transform.position).normalized;
        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        if (walking)
        {
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        if (chasing)
        {
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);

            if (Physics.OverlapSphere(transform.position, 0.5f, playerLayer).Length != 0)
            {
                PlayerManager.playerInstance.StartCoroutine("Death");
            }

            if (Physics.OverlapSphere(transform.position, 0.5f, enemyLayer).Length != 0)
            {
                //StartCoroutine("Pause");
            }
        }

        velocity.y += -20f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public IEnumerator StartChase()
    {
        yield return new WaitForSeconds(startDelay);

        speed = runSpeed;
        chasing = true;
        animator.SetBool("Running", true);
    }

    public void StopChase()
    {
        chasing = false;
        animator.SetBool("Running", false);
    }

    IEnumerator Pause()
    {
        chasing = false;
        animator.SetBool("Running", false);

        yield return new WaitForSeconds(0.5f);

        chasing = true;
        animator.SetBool("Running", true);
    }

    public void StartWalk()
    {
        walking = true;
        animator.SetBool("Walking", true);
    }

    public void StopWalk()
    {
        walking = false;
        animator.SetBool("Walking", false);
    }
}
