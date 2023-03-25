using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed;
    public float runBoost = 2f;
    private Vector3 movement;
    private Rigidbody rb;
    public bool isRunning;
    public Interact interact;
    public Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (!interact.isInteracting)
        {
            rb.MovePosition(rb.position + movement * Time.deltaTime * speed * (isRunning ? runBoost : 1f));

            if (movement.x > 0)
            {
                animator.SetInteger("walkingDirection", 1);
            }
            else if (movement.x < 0)
            {
                animator.SetInteger("walkingDirection", 3);
            }
            else if (movement.z > 0)
            {
                animator.SetInteger("walkingDirection", 0);
            }
            else if (movement.z < 0)
            {
                animator.SetInteger("walkingDirection", 2);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);

        }


    }

    public void OnMovement(InputValue value)
    {
        if (!interact.isInteracting)
        {
            movement = value.Get<Vector3>();

            if (movement == Vector3.zero)
            {
                animator.SetBool("isWalking", false);
            }
            else
            {
                animator.SetBool("isWalking", true);
            }
        }
    }

    public void OnRun(InputValue inputAction)
    {
        if (!interact.isInteracting)
        {
            animator.SetBool("isRunning", inputAction.isPressed);
            isRunning = inputAction.isPressed;
        }
    }
}
