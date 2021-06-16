using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPGCharacterAnims;
using System.Xml;
using System;

public class lji_playerStatus : MonoBehaviour
{
    GameObject playerStatusManger=null;
    //[Header("Xml")]
    //public string loadXml;
    [Header("Character")]
    public int maxHp;
    public int hp;

    //기초 스텟;
    public float attackSpeed;
    public int attackPower;
    public int defense;
    public int runSpeed;

    public float addAttackSpeed;
    public int addAttackPower;
    public int addDefense;
    public int addRunSpeed;

    public float totalAttackSpeed; //0~1사이
    public int totalAttackPower; 
    public int totalDefense; 
    public int totalRunSpeed; //5~10사이가 적당
    

    public RPGCharacterMovementController movementStat;

    //공격 손과 현재 무기세트
    public int side = 1;
    public int nowWeaponSet = 0;
    //이동 속도 관련 함수는 movementStat을 불러서 수정;
    //ex) movementStat.runSpeed = 5

    public RPGCharacterController characterController;

    public int gold = 0;
    private bool goldInit = false;

    [Header("Weapon")]
    // Weapon SET//3번은 맨주먹
    public int [] rightWeapon = new int[3] { 0, 0, 0 };
    public int [] leftWeapon = new int[3] { 0, 0, 0 };

    public int[] rightWeaponTier = new int[3] { 0, 0, 0 };
    public int[] leftWeaponTier = new int[3] { 0, 0, 0 };

    public float[] rightWeaponSpeed = new float[3] { 0f, 0f, 0f };
    public float[] leftWeaponSpeed = new float[3] { 0f, 0f, 0f };

    [Header("Armor")]
    public int head = (int)Armor.Default;
    public int upperArmor = (int)Armor.Default;
    public int lowerArmor = (int)Armor.Default;


    //
    lji_playerSounds sound;
    // Start is called before the first frame update
    void Start()
    {
        movementStat = GetComponent<RPGCharacterMovementController>();
        characterController = GetComponent<RPGCharacterController>();
        sound = GetComponent<lji_playerSounds>();
        //LoadXml(loadXml);
        //StartCoroutine(WaitLoadXml());
        //this.transform.position=GameObject.FindGameObjectWithTag("StartPortal").transform.position;

        totalAttackSpeed = attackSpeed;
        totalAttackPower = attackPower;
        totalDefense = defense;
        movementStat.runSpeed = runSpeed;

        playerStatusManger = GameObject.FindGameObjectWithTag("StatusManager");
        GetPlayerStatus();
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
            hp = 0;
        }
    }


    void death()
    {
        Debug.Log("HP0 is Dead");
        if (characterController.CanStartAction("Death"))
        {
            characterController.StartAction("Death");
            sound.PlaySound("Die");
        }

        StartCoroutine(LoadDeathScene());
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    if (characterController.isDead)
        //        Destroy(this.gameObject);
        //}
    }

    IEnumerator LoadDeathScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("DeadScene");
    }


    private void OnDestroy()
    {
        SetPlayerStatus();
        //OverwriteXml();
    }

    public void SetGold(int addGold)
    {
        gold += addGold;
    }
    

    public void GetPlayerStatus()
    {
        lji_statusManager statusManager = playerStatusManger.GetComponent<lji_statusManager>();
        maxHp = statusManager.maxHp;
        hp = statusManager.hp;
        attackSpeed = statusManager.attackSpeed;
        attackPower = statusManager.attackPower;
        defense = statusManager.defense;
        runSpeed = statusManager.runSpeed;
        addAttackSpeed = statusManager.addAttackSpeed;
        addAttackPower = statusManager.addAttackPower;
        addDefense = statusManager.addDefense;
        addRunSpeed = statusManager.addRunSpeed;
        side = statusManager.side;
        nowWeaponSet = statusManager.nowWeaponSet;
        gold = statusManager.gold;

        rightWeapon = statusManager.rightWeapon;
        leftWeapon = statusManager.leftWeapon;
        rightWeaponTier = statusManager.rightWeaponTier;
        leftWeaponTier = statusManager.leftWeaponTier;
        rightWeaponSpeed = statusManager.rightWeaponSpeed;
        leftWeaponSpeed = statusManager.leftWeaponSpeed;


        if (head != statusManager.head)
        {
            changeHeadStatus(statusManager.head); 
        }
        if (upperArmor != statusManager.upperArmor)
        {
            changeUpperArmorStatus(statusManager.head);
        }
        if (lowerArmor != statusManager.lowerArmor)
        {
            changeLowerArmorStatus(statusManager.head);
        }
        Debug.Log("------------------GetPlayerStatus-------------------");
        setEffect();

        SetPlayerStatus();
    }

    private void changeHeadStatus(int stHead)
    {
        int tmpDefence = 0;
        int tmpAttackPower = 0;
        int tmpRunSpeed = 0;
        float tmpAttackSpeed = 0;

        if (stHead == 1)
        {

        }
        else if (stHead == 2)
        {

        }
        else if (stHead == 3)
        {
            tmpDefence++;
        }
        else if (stHead == 4)
        {
            tmpDefence++;
        }
        else if (stHead == 5)
        {
            tmpDefence++;
        }

        //기본 방어력
        if (head == 0 && stHead != 0)
            tmpDefence++;
        if (head != 0 && stHead == 0)//기본 장비 착용시
        {
            tmpAttackPower = 0;
            tmpRunSpeed = 0;
            tmpAttackSpeed = 0;
            tmpDefence = 0;
        }



        head = stHead;
        addDefense = tmpDefence;
        addAttackPower = tmpAttackPower;
        addAttackSpeed = tmpAttackSpeed;
        addRunSpeed = tmpRunSpeed;
    }

    private void changeUpperArmorStatus(int stUpperArmor)
    {
        int tmpDefence = 0;
        int tmpAttackPower = 0;
        int tmpRunSpeed = 0;
        float tmpAttackSpeed = 0;

        if (stUpperArmor == 1)
        {

        }
        else if (stUpperArmor == 2)
        {
            tmpAttackPower++;
        }
        else if (stUpperArmor == 3)
        {
            tmpAttackPower++;
        }
        else if (stUpperArmor == 4)
        {
            tmpDefence++;
            tmpAttackPower++;
        }
        else if (stUpperArmor == 5)
        {
            tmpDefence++;
            tmpAttackPower++;
        }

        if (upperArmor == 0 && stUpperArmor != 0)
            tmpDefence++;
        if (upperArmor != 0 && stUpperArmor == 0)
        {
            tmpAttackPower = 0;
            tmpRunSpeed = 0;
            tmpAttackSpeed = 0;
            tmpDefence = 0;
        }

        upperArmor = stUpperArmor;
        addDefense = tmpDefence;
        addAttackPower = tmpAttackPower;
        addAttackSpeed = tmpAttackSpeed;
        addRunSpeed = tmpRunSpeed;

    }

    private void changeLowerArmorStatus(int stLowerArmor)
    {
        int tmpDefence = 0;
        int tmpAttackPower = 0;
        int tmpRunSpeed = 0;
        float tmpAttackSpeed = 0;

        if (stLowerArmor == 1)
        {
            tmpRunSpeed++;
        }
        else if (stLowerArmor == 2)
        {
            tmpRunSpeed++;
        }
        else if (stLowerArmor == 3)
        {
            tmpRunSpeed++;
        }
        else if (stLowerArmor == 4)
        {
            tmpRunSpeed++;
        }
        else if (stLowerArmor == 5)
        {
            tmpRunSpeed++;
            tmpDefence++;
        }

        if (lowerArmor == 0 && stLowerArmor != 0)
            tmpDefence++;
        if (lowerArmor != 0 && stLowerArmor == 0)
        {
            tmpAttackPower = 0;
            tmpRunSpeed = 0;
            tmpAttackSpeed = 0;
            tmpDefence = 0;
        }

        lowerArmor = stLowerArmor;
        addDefense = tmpDefence;
        addAttackPower = tmpAttackPower;
        addAttackSpeed = tmpAttackSpeed;
        addRunSpeed = tmpRunSpeed;

    }

    private void setEffect() 
    {
        int tmpDefence = 0;
        int tmpAttackPower = 0;
        int tmpRunSpeed = 0;
        float tmpAttackSpeed = 0;

        if (head == 1 && upperArmor == 1 && lowerArmor == 1)
        {
            tmpAttackPower += 1;
            tmpAttackSpeed += 0.1f;
        }
        else if (head == 2 && upperArmor == 2 && lowerArmor == 2)
        {
            tmpDefence -= 2; //방감 
            tmpAttackPower += 2;
            tmpAttackSpeed += 0.2f;
        }
        else if (head == 3 && upperArmor == 3 && lowerArmor == 3)
        {
            tmpAttackPower += 1;
            tmpRunSpeed += 1;
            tmpAttackSpeed += 0.1f;
        }
        else if (head == 4 && upperArmor == 4 && lowerArmor == 4)
        {
            tmpDefence += 2;
            tmpAttackPower += 1;
            tmpRunSpeed += 1;
            tmpAttackSpeed += 0.1f;
        }
        else if (head == 5 && upperArmor == 5 && lowerArmor == 5)
        {
            tmpDefence += 3;
            tmpAttackPower += 2;
            tmpAttackSpeed += 0.1f;
        }
        else 
        {
            tmpDefence = 0;
            tmpAttackPower = 0;
            tmpRunSpeed = 0;
            tmpAttackSpeed = 0;
        }

        addDefense += tmpDefence;
        addAttackPower += tmpAttackPower;
        addAttackSpeed += tmpAttackSpeed;
        addRunSpeed += tmpRunSpeed;

    }

    void SetPlayerStatus()
    {
        lji_statusManager statusManager = playerStatusManger.GetComponent<lji_statusManager>();
        statusManager.maxHp= maxHp;
        statusManager.hp=hp;
        statusManager.attackSpeed=attackSpeed;
        statusManager.attackPower=attackPower;
        statusManager.defense=defense;
        statusManager.runSpeed=runSpeed;
        statusManager.addAttackSpeed=addAttackSpeed;
        statusManager.addAttackPower=addAttackPower;
        statusManager.addDefense=addDefense;
        statusManager.addRunSpeed=addRunSpeed;
        statusManager.side=side;
        statusManager.nowWeaponSet=nowWeaponSet;
        statusManager.gold=gold;

        statusManager.rightWeapon=rightWeapon;
        statusManager.leftWeapon =leftWeapon ;
        statusManager.rightWeaponTier=rightWeaponTier;
        statusManager.leftWeaponTier=leftWeaponTier;
        statusManager.rightWeaponSpeed=rightWeaponSpeed;
        statusManager.leftWeaponSpeed=leftWeaponSpeed;

        statusManager.head = head;
        statusManager.upperArmor=upperArmor;
        statusManager.lowerArmor=lowerArmor;
    }
}
