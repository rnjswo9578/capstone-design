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

    //���� �հ� ���� ���⼼Ʈ
    public int side = 1;
    public int nowWeaponSet = 0;
    //�̵� �ӵ� ���� �Լ��� movementStat�� �ҷ��� ����;
    //ex) movementStat.runSpeed = 5

    RPGCharacterController characterController;

    public int gold = 0;

    [Header("Weapon")]
    // Weapon SET//3���� ���ָ�
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
        if (instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������ 
        {
            instance = this; //���ڽ��� instance�� �־��ݴϴ�. 
            DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ���� 
        }
        else
        {
            if (instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ� 
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ���� 
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
