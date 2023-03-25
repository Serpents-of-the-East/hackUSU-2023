using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public bool isAttacking;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        
        animator.SetBool("isAttacking", isAttacking);
    }
}
