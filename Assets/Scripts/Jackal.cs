using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jackal : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public GameObject fox;
    private bool chasing = false;
    public float speed;
    public LayerMask playerLayer;

    private Vector3 direction;
    private float targetAngle;
    private Vector3 velocity;
    public Transform groundCheck;
    public LayerMask groundMask;
    private bool isGrounded;

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

        if (chasing)
        {
            direction = (fox.transform.position - transform.position).normalized;
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);

            if (Physics.OverlapSphere(transform.position, 1f, playerLayer).Length != 0)
            {
                PlayerManager.playerInstance.StartCoroutine("Death");
            }
        }

        velocity.y += -20f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // for testing
        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetBool("Running", true);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetBool("Running", false);
        }
    }

    public void StartChase()
    {
        chasing = true;
        animator.SetBool("Running", true);
    }

    public void StopChase()
    {
        chasing = false;
        animator.SetBool("Running", false);
    }
}
