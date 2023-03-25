using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed;
    private Vector3 movement;
    private Rigidbody rb;

    public Animator animator;
    Vector3 lastPosition;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
        //animator = this.transform.GetChild(0).GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.x != 0 || movement.y != 0 || movement.z != 0)
        {
            rb.MovePosition(rb.position + movement * Time.deltaTime * speed);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }


    }

    public void OnMovement(InputValue value)
    {
        movement = value.Get<Vector3>();
        animator.SetBool("isWalking", true);
    }

    public void OnJump(InputAction inputAction)
    {
        Debug.Log("testing jump");
    }
}
