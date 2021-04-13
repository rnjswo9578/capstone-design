using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lji_playerAttack : MonoBehaviour
{
    Animator animator;
    float attackTerm = 0f;
    public float attackSpeed = 1.0f;
    private lji_attackArea attackArea = null;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        attackArea = GetComponentInChildren<lji_attackArea>();
    }

    // Update is called once per frame
    void Update()
    {
        if(attackTerm > 2/attackSpeed)
        {
            attackTerm = 0f;
        }
        else if (attackTerm != 0f)
        {
            attackTerm += Time.deltaTime;
        }


        if (Input.GetMouseButtonDown(0))//마우스 왼쪽:0 마우스 오른쪽:1
        {
            if (attackTerm == 0f)
            {
                Attack();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {

        }
    }

    public void Attack()
    {
        if (this == null) { return; }

        animator.SetFloat("AttackSpeed", attackSpeed);
        animator.SetTrigger("Attack");
        attackTerm += Time.deltaTime;

        for(int i = 0; i < attackArea.colliders.Count; i++)
        {
            var collider = attackArea.colliders[i];

            //var obj = collider.GetComponent<>();//monster 체력 스크립트 넣어줘야한다.

        }
    }

}
