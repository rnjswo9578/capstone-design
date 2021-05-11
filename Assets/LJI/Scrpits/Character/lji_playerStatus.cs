using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims;

public class lji_playerStatus : MonoBehaviour
{
    public int maxHp;
    public int hp;

    public float attackSpeed;
    public int attackPower;
    public int defense;
    public float runSpeed;

    public float totalAttackSpeed;
    public int totalAttackPower;
    public int totalDefense;
    

    public RPGCharacterMovementController movementStat;
    //이동 속도 관련 함수는 movementStat을 불러서 수정;
    //ex) movementStat.runSpeed = 5

    RPGCharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        movementStat = GetComponent<RPGCharacterMovementController>();

        characterController = GetComponent<RPGCharacterController>();

        totalAttackSpeed = attackSpeed;
        totalAttackPower = attackPower;
        totalDefense = defense;
        movementStat.runSpeed = runSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //hp는 maxHp를 넘어서면 안된다.
        if (hp > maxHp)
            hp = maxHp;
        
        if (hp <= 0)
        {
            death();
        }

        
    }

    void death()
    {
        if (characterController.CanStartAction("Death"))
        {
            characterController.StartAction("Death");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (characterController.isDead)
                Destroy(this.gameObject);
        }
    }
}
