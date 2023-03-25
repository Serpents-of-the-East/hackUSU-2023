using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public bool isPhysical;
    public bool isMagic;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnimation(bool isPhysical)
    {
        if (isPhysical)
        {
            this.isPhysical = true;
            animator.SetBool("isPhysical", true);
        }
        else
        {
            this.isMagic = true;
            animator.SetBool("isMagic", true);
        }
    }

    public void OnCompleteAnimation()
    {
        if (isPhysical)
        {
            this.isPhysical = false;
            animator.SetBool("isPhysical", false);
        } 
        else if (isMagic)
        {
            this.isMagic = false;
            animator.SetBool("isMagic", false);
        }
    }
}
