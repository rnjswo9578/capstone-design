using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims;
using System.Xml;

public class lji_statusManager : MonoBehaviour
{
    public static lji_statusManager instance = null; //private static lji_statusManager instance = null;

    [Header("Xml")]
    public string loadXml;
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

    RPGCharacterController characterController;

    public int gold = 0;

    [Header("Weapon")]
    // Weapon SET//3번은 맨주먹
    public int[] rightWeapon = new int[3] { 0, 0, 0 };
    public int[] leftWeapon = new int[3] { 0, 0, 0 };

    public int[] rightWeaponTier = new int[3] { 0, 0, 0 };
    public int[] leftWeaponTier = new int[3] { 0, 0, 0 };

    public float[] rightWeaponSpeed = new float[3] { 0f, 0f, 0f };
    public float[] leftWeaponSpeed = new float[3] { 0f, 0f, 0f };

    [Header("Armor")]
    public int head = (int)Armor.Default;
    public int upperArmor = (int)Armor.Default;
    public int lowerArmor = (int)Armor.Default;

    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때 
        {
            instance = this; //내자신을 instance로 넣어줍니다. 
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지 
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미 
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제 
        }

        //LoadXml("Gold");
    }


    //private void OnDestroy()
    //{
    //    OverwriteXml();
    //}



    public void changeGold(int other) 
    {
        gold = gold + other;
        GameObject.FindWithTag("PLAYER").SendMessage("GetPlayerStatus");
    }
    public int getGold()
    {
        return gold;
    }
    public void changeW(int r, int l)
    {
        rightWeapon[0] = r;
        leftWeapon[0] = l;
        GameObject.FindWithTag("PLAYER").SendMessage("GetPlayerStatus");
    }
    public void changeHead(int other)
    {
        head = other;
        GameObject.FindWithTag("PLAYER").SendMessage("GetPlayerStatus");
    }
    public void changeUArmor(int other)
    {
        upperArmor = other;
        GameObject.FindWithTag("PLAYER").SendMessage("GetPlayerStatus");
    }
    public void changeLArmor(int other)
    {
        lowerArmor = other;
        GameObject.FindWithTag("PLAYER").SendMessage("GetPlayerStatus");
    }
    public void changeTier(int other)
    {
        rightWeaponTier[0] = other;
        leftWeaponTier[0] = other;
        GameObject.FindWithTag("PLAYER").SendMessage("GetPlayerStatus");
    }

    void LoadXml(string filename)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(filename);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNode goldXml = xmlDoc.SelectSingleNode("GoldInfo/Gold");
        gold=int.Parse(goldXml.InnerText);

    }

    public void OverwriteXml()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Gold");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

<<<<<<< Updated upstream
        XmlNode goldXml = xmlDoc.SelectSingleNode("GoldInfo/Gold");
        goldXml.InnerText = gold + "";

        xmlDoc.Save("./Assets/Resources/Gold.xml");
=======

    public void changeSubW(int r, int l)
    {
        rightWeapon[1] = r;
        leftWeapon[1] = l;
        GameObject.FindWithTag("PLAYER").SendMessage("GetPlayerStatus");
    }
    public void changeSubTier(int other)
    {
        rightWeaponTier[1] = other;
        leftWeaponTier[1] = other;
        GameObject.FindWithTag("PLAYER").SendMessage("GetPlayerStatus");
>>>>>>> Stashed changes
    }
}
