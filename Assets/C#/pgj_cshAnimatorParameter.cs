using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_cshAnimatorParameter : MonoBehaviour
{
    private Animator animator;
    private bool isAttack = false, isAttack2 = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (animator.GetInteger("temp") == -1) {
            animator.SetInteger("temp", 0);
            animator.SetBool("attack01", false);
            animator.SetBool("attack02", false);
            animator.SetBool("attack03", false);
        }

        if (animator.GetBool("walk") == false)
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.D))
            {
                animator.SetBool("walk", true);
            }

        //if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            animator.SetBool("walk", false);


        if (Input.GetMouseButtonDown(0))
        {
            if(isAttack == false)
                isAttack = true;
            animator.SetBool("isAttack01", isAttack);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isAttack == true)
                isAttack = false;
            animator.SetBool("isAttack01", isAttack);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (isAttack2 == false)
                isAttack2 = true;
            animator.SetBool("isAttack02", isAttack2);
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (isAttack2 == true)
                isAttack2 = false;
            animator.SetBool("isAttack02", isAttack2);
        }

    }
}
