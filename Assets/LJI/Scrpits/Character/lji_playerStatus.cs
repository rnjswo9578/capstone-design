using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims;

public class lji_playerStatus : MonoBehaviour
{
    [Header("Character")]
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

    [Header("Weapon")]
    // Weapon SET//3���� ���ָ�
    public int [] rightWeapon = new int[3] { 0, 0, 0 };
    public int [] leftWeapon = new int[3] { 0, 0, 0 };

    public int[] rightWeaponTier = new int[3] { 0, 0, 0 };
    public int[] leftWeaponTier = new int[3] { 0, 0, 0 };

    public float[] rightWeaponSpeed = new float[3] { 0f, 0f, 0f };
    public float[] leftWeaponSpeed = new float[3] { 0f, 0f, 0f };

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
