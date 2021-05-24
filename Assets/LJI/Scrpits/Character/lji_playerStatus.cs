using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims;
using System.Xml;

public class lji_playerStatus : MonoBehaviour
{
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
    //이동 속도 관련 함수는 movementStat을 불러서 수정;
    //ex) movementStat.runSpeed = 5

    RPGCharacterController characterController;

    [Header("Weapon")]
    // Weapon SET//3번은 맨주먹
    public int [] rightWeapon = new int[3] { 0, 0, 0 };
    public int [] leftWeapon = new int[3] { 0, 0, 0 };

    public int[] rightWeaponTier = new int[3] { 0, 0, 0 };
    public int[] leftWeaponTier = new int[3] { 0, 0, 0 };

    public float[] rightWeaponSpeed = new float[3] { 0f, 0f, 0f };
    public float[] leftWeaponSpeed = new float[3] { 0f, 0f, 0f };

    // Start is called before the first frame update
    void Start()
    {
        LoadXml();

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




    private void OnDestroy()
    {
        OverwriteXml();
    }

    void LoadXml()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("PlayerStatus");
        Debug.Log(textAsset);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("PlayerInfo/Character");

        foreach (XmlNode node in nodes)
        {
            Debug.Log("name :: " + node.SelectSingleNode("Name").InnerText);

            Debug.Log("MaxHp :: " + node.SelectSingleNode("maxHp").InnerText);
            Debug.Log("hp :: " + node.SelectSingleNode("hp").InnerText);

            Debug.Log("attackSpeed :: " + node.SelectSingleNode("attackSpeed").InnerText);
            Debug.Log("attackPower :: " + node.SelectSingleNode("attackPower").InnerText);
            Debug.Log("defense :: " + node.SelectSingleNode("defense").InnerText);
            Debug.Log("runSpeed :: " + node.SelectSingleNode("runSpeed").InnerText);

            Debug.Log("addAttackSpeed :: " + node.SelectSingleNode("addAttackSpeed").InnerText);
            Debug.Log("addAttackPower :: " + node.SelectSingleNode("addAttackPower").InnerText);
            Debug.Log("NaddDefense :: " + node.SelectSingleNode("addDefense").InnerText);
            Debug.Log("addRunSpeed :: " + node.SelectSingleNode("addRunSpeed").InnerText);

            Debug.Log("totalAttackSpeed :: " + node.SelectSingleNode("totalAttackSpeed").InnerText);
            Debug.Log("totalAttackPower :: " + node.SelectSingleNode("totalAttackPower").InnerText);
            Debug.Log("totalDefense :: " + node.SelectSingleNode("totalDefense").InnerText);
            Debug.Log("totalRunSpeed :: " + node.SelectSingleNode("totalRunSpeed").InnerText);

            maxHp = int.Parse(node.SelectSingleNode("maxHp").InnerText);
            hp = int.Parse(node.SelectSingleNode("hp").InnerText);

            attackSpeed = float.Parse(node.SelectSingleNode("attackSpeed").InnerText);
            attackPower = int.Parse(node.SelectSingleNode("attackPower").InnerText);
            defense = int.Parse(node.SelectSingleNode("defense").InnerText);
            runSpeed = int.Parse(node.SelectSingleNode("runSpeed").InnerText);

            addAttackSpeed = float.Parse(node.SelectSingleNode("addAttackSpeed").InnerText);
            addAttackPower = int.Parse(node.SelectSingleNode("addAttackPower").InnerText);
            addDefense = int.Parse(node.SelectSingleNode("addDefense").InnerText);
            addRunSpeed = int.Parse(node.SelectSingleNode("addRunSpeed").InnerText);

            totalAttackSpeed = int.Parse(node.SelectSingleNode("totalAttackSpeed").InnerText);
            totalAttackPower = int.Parse(node.SelectSingleNode("totalAttackPower").InnerText);
            totalDefense = int.Parse(node.SelectSingleNode("totalDefense").InnerText);
            totalRunSpeed = int.Parse(node.SelectSingleNode("totalRunSpeed").InnerText);

        }

        nodes = xmlDoc.SelectNodes("PlayerInfo/Weapon");

        foreach (XmlNode node in nodes)
        {
            Debug.Log("rightWeapon :: " + node.SelectSingleNode("rightWeapon1").InnerText+" :: " + node.SelectSingleNode("rightWeapon2").InnerText+" :: " + node.SelectSingleNode("rightWeapon3").InnerText);
            Debug.Log("leftWeapon :: " + node.SelectSingleNode("leftWeapon1").InnerText+" :: " + node.SelectSingleNode("leftWeapon2").InnerText+" :: " + node.SelectSingleNode("leftWeapon3").InnerText);
            Debug.Log("rightWeaponTier :: " + node.SelectSingleNode("rightWeaponTier1").InnerText+" :: " + node.SelectSingleNode("rightWeaponTier2").InnerText+" :: " + node.SelectSingleNode("rightWeaponTier3").InnerText);
            Debug.Log("leftWeaponTier :: " + node.SelectSingleNode("leftWeaponTier1").InnerText+" :: " + node.SelectSingleNode("leftWeaponTier2").InnerText+" :: " + node.SelectSingleNode("leftWeaponTier3").InnerText);
            Debug.Log("rightWeaponSpeed :: " + node.SelectSingleNode("rightWeaponSpeed1").InnerText+" :: " + node.SelectSingleNode("rightWeaponSpeed2").InnerText+" :: " + node.SelectSingleNode("rightWeaponSpeed3").InnerText);
            Debug.Log("leftWeaponSpeed :: " + node.SelectSingleNode("leftWeaponSpeed1").InnerText+ " :: " + node.SelectSingleNode("leftWeaponSpeed2").InnerText+ " :: " + node.SelectSingleNode("leftWeaponSpeed3").InnerText);

            rightWeapon[0] = int.Parse(node.SelectSingleNode("rightWeapon1").InnerText);
            rightWeapon[1] = int.Parse(node.SelectSingleNode("rightWeapon2").InnerText);
            rightWeapon[2] = int.Parse(node.SelectSingleNode("rightWeapon3").InnerText);

            leftWeapon[0] = int.Parse(node.SelectSingleNode("leftWeapon1").InnerText);
            leftWeapon[1] = int.Parse(node.SelectSingleNode("leftWeapon2").InnerText);
            leftWeapon[2] = int.Parse(node.SelectSingleNode("leftWeapon3").InnerText);

            rightWeaponTier[0] = int.Parse(node.SelectSingleNode("rightWeaponTier1").InnerText);
            rightWeaponTier[1] = int.Parse(node.SelectSingleNode("rightWeaponTier2").InnerText);
            rightWeaponTier[2] = int.Parse(node.SelectSingleNode("rightWeaponTier3").InnerText);

            leftWeaponTier[0] = int.Parse(node.SelectSingleNode("leftWeaponTier1").InnerText);
            leftWeaponTier[1] = int.Parse(node.SelectSingleNode("leftWeaponTier2").InnerText);
            leftWeaponTier[2] = int.Parse(node.SelectSingleNode("leftWeaponTier3").InnerText);

            rightWeaponSpeed[0] = float.Parse(node.SelectSingleNode("rightWeaponSpeed1").InnerText);
            rightWeaponSpeed[1] = float.Parse(node.SelectSingleNode("rightWeaponSpeed2").InnerText);
            rightWeaponSpeed[2] = float.Parse(node.SelectSingleNode("rightWeaponSpeed3").InnerText);

            leftWeaponSpeed[0] = float.Parse(node.SelectSingleNode("leftWeaponSpeed1").InnerText);
            leftWeaponSpeed[1] = float.Parse(node.SelectSingleNode("leftWeaponSpeed2").InnerText);
            leftWeaponSpeed[2] = float.Parse(node.SelectSingleNode("leftWeaponSpeed3").InnerText);
        }
    }

    void OverwriteXml()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Character");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("PlayerInfo/Character");
        XmlNode character = nodes[0];

        character.SelectSingleNode("Name").InnerText = name;
        character.SelectSingleNode("maxHp").InnerText = maxHp + "";
        character.SelectSingleNode("hp").InnerText = hp + "";

        character.SelectSingleNode("attackSpeed").InnerText = attackSpeed + "";
        character.SelectSingleNode("attackPower").InnerText = attackPower + "";
        character.SelectSingleNode("defense").InnerText = defense + "";
        character.SelectSingleNode("runSpeed").InnerText = runSpeed + "";

        character.SelectSingleNode("addAttackSpeed").InnerText = addAttackSpeed + "";
        character.SelectSingleNode("addAttackPower").InnerText = addAttackPower + "";
        character.SelectSingleNode("addDefense").InnerText = addDefense + "";
        character.SelectSingleNode("addRunSpeed").InnerText = addRunSpeed + "";

        character.SelectSingleNode("totalAttackSpeed").InnerText = totalAttackSpeed + "";
        character.SelectSingleNode("totalAttackPower").InnerText = totalAttackPower + "";
        character.SelectSingleNode("totalDefense").InnerText = totalDefense + "";
        character.SelectSingleNode("totalRunSpeed").InnerText = totalRunSpeed + "";


        nodes = xmlDoc.SelectNodes("PlayerInfo/Weapon");
        character = nodes[0];

        character.SelectSingleNode("rightWeapon1").InnerText = rightWeapon[0] + "";
        character.SelectSingleNode("rightWeapon2").InnerText = rightWeapon[1] + "";
        character.SelectSingleNode("rightWeapon3").InnerText = rightWeapon[2] + "";

        character.SelectSingleNode("leftWeapon1").InnerText = leftWeapon[0] + "";
        character.SelectSingleNode("leftWeapon2").InnerText = leftWeapon[1] + "";
        character.SelectSingleNode("leftWeapon3").InnerText = leftWeapon[2] + "";

        character.SelectSingleNode("rightWeaponTier1").InnerText = rightWeaponTier[0] + "";
        character.SelectSingleNode("rightWeaponTier2").InnerText = rightWeaponTier[1] + "";
        character.SelectSingleNode("rightWeaponTier3").InnerText = rightWeaponTier[2] + "";

        character.SelectSingleNode("leftWeaponTier1").InnerText = leftWeaponTier[0] + "";
        character.SelectSingleNode("leftWeaponTier2").InnerText = leftWeaponTier[1] + "";
        character.SelectSingleNode("leftWeaponTier3").InnerText = leftWeaponTier[2] + "";

        character.SelectSingleNode("rightWeaponSpeed1").InnerText = rightWeaponSpeed[0] + "";
        character.SelectSingleNode("rightWeaponSpeed2").InnerText = rightWeaponSpeed[1] + "";
        character.SelectSingleNode("rightWeaponSpeed3").InnerText = rightWeaponSpeed[2] + "";

        character.SelectSingleNode("leftWeaponSpeed1").InnerText = leftWeaponSpeed[0] + "";
        character.SelectSingleNode("leftWeaponSpeed2").InnerText = leftWeaponSpeed[1] + "";
        character.SelectSingleNode("leftWeaponSpeed3").InnerText = leftWeaponSpeed[2] + "";

        xmlDoc.Save("./Assets/Resources/PlayerStatus.xml");
    }
}
