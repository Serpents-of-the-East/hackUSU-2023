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



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + movement * Time.deltaTime * speed);


    }

    public void OnMovement(InputValue value)
    {
        movement = value.Get<Vector3>();
        Debug.Log("This was called");

    }

    public void OnJump(InputAction inputAction)
    {
        Debug.Log("testing jump");
    }
}
