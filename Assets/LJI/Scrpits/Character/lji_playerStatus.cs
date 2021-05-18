using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims;

public class lji_playerStatus : MonoBehaviour
{
    public int maxHp;
    public int hp;

    //���� ����;
    public float attackSpeed;
    public int attackPower;
    public int defense;
    public int runSpeed;

    public float addAttackSpeed;
    public int addAttackPower;
    public int addDefense;
    public int addRunSpeed;

    public float totalAttackSpeed; //0~1����
    public int totalAttackPower; 
    public int totalDefense; 
    public int totalRunSpeed; //5~10���̰� ����
    

    public RPGCharacterMovementController movementStat;
    //�̵� �ӵ� ���� �Լ��� movementStat�� �ҷ��� ����;
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
        //hp�� maxHp�� �Ѿ�� �ȵȴ�.
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
