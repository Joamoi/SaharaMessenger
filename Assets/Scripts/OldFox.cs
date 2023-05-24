using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldFox : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public GameObject fox;
    private bool walking = false;
    public float speed;

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

        direction = (fox.transform.position - transform.position).normalized;
        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        if (walking)
        {
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        velocity.y += -20f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // for testing
        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetBool("Walking", true);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetBool("Walking", false);
        }
    }

    public void Walk()
    {
        walking = true;
        animator.SetBool("Walking", true);
    }

    public void Stop()
    {
        walking = false;
        animator.SetBool("Walking", false);
    }
}
