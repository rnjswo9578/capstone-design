using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPGCharacterAnims;
using System.Xml;

public class lji_playerStatus : MonoBehaviour
{
    GameObject playerStatusManger=null;
    //[Header("Xml")]
    //public string loadXml;
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

    public RPGCharacterController characterController;

    public int gold = 0;
    private bool goldInit = false;

    [Header("Weapon")]
    // Weapon SET//3���� ���ָ�
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
        //hp�� maxHp�� �Ѿ�� �ȵȴ�.
        if (hp > maxHp)
            hp = maxHp;
        
        if (hp <= 0)
        {
            death();
            hp = 0;
        }
    }

    //private void LateUpdate()
    //{
    //    int ingold = lji_statusManager.instance.getGold();
    //    if (!goldInit)
    //        if (ingold != gold)
    //        {
    //            goldInit = !goldInit;
    //            gold = ingold;
    //        }
    //}

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

    //void LoadXml(string filename)
    //{
    //    TextAsset textAsset = (TextAsset)Resources.Load(filename);
    //    Debug.Log(textAsset);
    //    XmlDocument xmlDoc = new XmlDocument();
    //    xmlDoc.LoadXml(textAsset.text);

    //    XmlNodeList nodes = xmlDoc.SelectNodes("PlayerInfo/Character");

    //    foreach (XmlNode node in nodes)
    //    {
    //        Debug.Log("name :: " + node.SelectSingleNode("Name").InnerText);
    //        Debug.Log("MaxHp :: " + node.SelectSingleNode("maxHp").InnerText);
    //        Debug.Log("hp :: " + node.SelectSingleNode("hp").InnerText);

    //        Debug.Log("attackSpeed :: " + node.SelectSingleNode("attackSpeed").InnerText);
    //        Debug.Log("attackPower :: " + node.SelectSingleNode("attackPower").InnerText);
    //        Debug.Log("defense :: " + node.SelectSingleNode("defense").InnerText);
    //        Debug.Log("runSpeed :: " + node.SelectSingleNode("runSpeed").InnerText);

    //        Debug.Log("addAttackSpeed :: " + node.SelectSingleNode("addAttackSpeed").InnerText);
    //        Debug.Log("addAttackPower :: " + node.SelectSingleNode("addAttackPower").InnerText);
    //        Debug.Log("NaddDefense :: " + node.SelectSingleNode("addDefense").InnerText);
    //        Debug.Log("addRunSpeed :: " + node.SelectSingleNode("addRunSpeed").InnerText);

    //        Debug.Log("totalAttackSpeed :: " + node.SelectSingleNode("totalAttackSpeed").InnerText);
    //        Debug.Log("totalAttackPower :: " + node.SelectSingleNode("totalAttackPower").InnerText);
    //        Debug.Log("totalDefense :: " + node.SelectSingleNode("totalDefense").InnerText);
    //        Debug.Log("totalRunSpeed :: " + node.SelectSingleNode("totalRunSpeed").InnerText);

    //        maxHp = int.Parse(node.SelectSingleNode("maxHp").InnerText);
    //        hp = int.Parse(node.SelectSingleNode("hp").InnerText);

    //        attackSpeed = float.Parse(node.SelectSingleNode("attackSpeed").InnerText);
    //        attackPower = int.Parse(node.SelectSingleNode("attackPower").InnerText);
    //        defense = int.Parse(node.SelectSingleNode("defense").InnerText);
    //        runSpeed = int.Parse(node.SelectSingleNode("runSpeed").InnerText);

    //        addAttackSpeed = float.Parse(node.SelectSingleNode("addAttackSpeed").InnerText);
    //        addAttackPower = int.Parse(node.SelectSingleNode("addAttackPower").InnerText);
    //        addDefense = int.Parse(node.SelectSingleNode("addDefense").InnerText);
    //        addRunSpeed = int.Parse(node.SelectSingleNode("addRunSpeed").InnerText);

    //        totalAttackSpeed = float.Parse(node.SelectSingleNode("totalAttackSpeed").InnerText);
    //        totalAttackPower = int.Parse(node.SelectSingleNode("totalAttackPower").InnerText);
    //        totalDefense = int.Parse(node.SelectSingleNode("totalDefense").InnerText);
    //        totalRunSpeed = int.Parse(node.SelectSingleNode("totalRunSpeed").InnerText);
    //    }

    //    nodes = xmlDoc.SelectNodes("PlayerInfo/Weapon");

    //    foreach (XmlNode node in nodes)
    //    {
    //        Debug.Log("rightWeapon :: " + node.SelectSingleNode("rightWeapon1").InnerText+" :: " + node.SelectSingleNode("rightWeapon2").InnerText+" :: " + node.SelectSingleNode("rightWeapon3").InnerText);
    //        Debug.Log("leftWeapon :: " + node.SelectSingleNode("leftWeapon1").InnerText+" :: " + node.SelectSingleNode("leftWeapon2").InnerText+" :: " + node.SelectSingleNode("leftWeapon3").InnerText);
    //        Debug.Log("rightWeaponTier :: " + node.SelectSingleNode("rightWeaponTier1").InnerText+" :: " + node.SelectSingleNode("rightWeaponTier2").InnerText+" :: " + node.SelectSingleNode("rightWeaponTier3").InnerText);
    //        Debug.Log("leftWeaponTier :: " + node.SelectSingleNode("leftWeaponTier1").InnerText+" :: " + node.SelectSingleNode("leftWeaponTier2").InnerText+" :: " + node.SelectSingleNode("leftWeaponTier3").InnerText);
    //        Debug.Log("rightWeaponSpeed :: " + node.SelectSingleNode("rightWeaponSpeed1").InnerText+" :: " + node.SelectSingleNode("rightWeaponSpeed2").InnerText+" :: " + node.SelectSingleNode("rightWeaponSpeed3").InnerText);
    //        Debug.Log("leftWeaponSpeed :: " + node.SelectSingleNode("leftWeaponSpeed1").InnerText+ " :: " + node.SelectSingleNode("leftWeaponSpeed2").InnerText+ " :: " + node.SelectSingleNode("leftWeaponSpeed3").InnerText);
            
    //        rightWeapon[0] = int.Parse(node.SelectSingleNode("rightWeapon1").InnerText);
    //        rightWeapon[1] = int.Parse(node.SelectSingleNode("rightWeapon2").InnerText);
    //        rightWeapon[2] = int.Parse(node.SelectSingleNode("rightWeapon3").InnerText);

    //        leftWeapon[0] = int.Parse(node.SelectSingleNode("leftWeapon1").InnerText);
    //        leftWeapon[1] = int.Parse(node.SelectSingleNode("leftWeapon2").InnerText);
    //        leftWeapon[2] = int.Parse(node.SelectSingleNode("leftWeapon3").InnerText);

    //        rightWeaponTier[0] = int.Parse(node.SelectSingleNode("rightWeaponTier1").InnerText);
    //        rightWeaponTier[1] = int.Parse(node.SelectSingleNode("rightWeaponTier2").InnerText);
    //        rightWeaponTier[2] = int.Parse(node.SelectSingleNode("rightWeaponTier3").InnerText);

    //        leftWeaponTier[0] = int.Parse(node.SelectSingleNode("leftWeaponTier1").InnerText);
    //        leftWeaponTier[1] = int.Parse(node.SelectSingleNode("leftWeaponTier2").InnerText);
    //        leftWeaponTier[2] = int.Parse(node.SelectSingleNode("leftWeaponTier3").InnerText);

    //        rightWeaponSpeed[0] = float.Parse(node.SelectSingleNode("rightWeaponSpeed1").InnerText);
    //        rightWeaponSpeed[1] = float.Parse(node.SelectSingleNode("rightWeaponSpeed2").InnerText);
    //        rightWeaponSpeed[2] = float.Parse(node.SelectSingleNode("rightWeaponSpeed3").InnerText);

    //        leftWeaponSpeed[0] = float.Parse(node.SelectSingleNode("leftWeaponSpeed1").InnerText);
    //        leftWeaponSpeed[1] = float.Parse(node.SelectSingleNode("leftWeaponSpeed2").InnerText);
    //        leftWeaponSpeed[2] = float.Parse(node.SelectSingleNode("leftWeaponSpeed3").InnerText);
    //    }
    //}

    //public void OverwriteXml()
    //{
    //    TextAsset textAsset = (TextAsset)Resources.Load("PlayerStatus");
    //    XmlDocument xmlDoc = new XmlDocument();
    //    xmlDoc.LoadXml(textAsset.text);

    //    XmlNodeList nodes = xmlDoc.SelectNodes("PlayerInfo/Character");
    //    XmlNode character = nodes[0];

    //    //character.SelectSingleNode("Name").InnerText = name;
    //    character.SelectSingleNode("maxHp").InnerText = maxHp + "";
    //    character.SelectSingleNode("hp").InnerText = hp + "";

    //    character.SelectSingleNode("attackSpeed").InnerText = attackSpeed + "";
    //    character.SelectSingleNode("attackPower").InnerText = attackPower + "";
    //    character.SelectSingleNode("defense").InnerText = defense + "";
    //    character.SelectSingleNode("runSpeed").InnerText = runSpeed + "";

    //    character.SelectSingleNode("addAttackSpeed").InnerText = addAttackSpeed + "";
    //    character.SelectSingleNode("addAttackPower").InnerText = addAttackPower + "";
    //    character.SelectSingleNode("addDefense").InnerText = addDefense + "";
    //    character.SelectSingleNode("addRunSpeed").InnerText = addRunSpeed + "";

    //    character.SelectSingleNode("totalAttackSpeed").InnerText = totalAttackSpeed + "";
    //    character.SelectSingleNode("totalAttackPower").InnerText = totalAttackPower + "";
    //    character.SelectSingleNode("totalDefense").InnerText = totalDefense + "";
    //    character.SelectSingleNode("totalRunSpeed").InnerText = totalRunSpeed + "";


    //    nodes = xmlDoc.SelectNodes("PlayerInfo/Weapon");
    //    character = nodes[0];

    //    character.SelectSingleNode("rightWeapon1").InnerText = rightWeapon[0] + "";
    //    character.SelectSingleNode("rightWeapon2").InnerText = rightWeapon[1] + "";
    //    character.SelectSingleNode("rightWeapon3").InnerText = rightWeapon[2] + "";

    //    character.SelectSingleNode("leftWeapon1").InnerText = leftWeapon[0] + "";
    //    character.SelectSingleNode("leftWeapon2").InnerText = leftWeapon[1] + "";
    //    character.SelectSingleNode("leftWeapon3").InnerText = leftWeapon[2] + "";

    //    character.SelectSingleNode("rightWeaponTier1").InnerText = rightWeaponTier[0] + "";
    //    character.SelectSingleNode("rightWeaponTier2").InnerText = rightWeaponTier[1] + "";
    //    character.SelectSingleNode("rightWeaponTier3").InnerText = rightWeaponTier[2] + "";

    //    character.SelectSingleNode("leftWeaponTier1").InnerText = leftWeaponTier[0] + "";
    //    character.SelectSingleNode("leftWeaponTier2").InnerText = leftWeaponTier[1] + "";
    //    character.SelectSingleNode("leftWeaponTier3").InnerText = leftWeaponTier[2] + "";

    //    character.SelectSingleNode("rightWeaponSpeed1").InnerText = rightWeaponSpeed[0] + "";
    //    character.SelectSingleNode("rightWeaponSpeed2").InnerText = rightWeaponSpeed[1] + "";
    //    character.SelectSingleNode("rightWeaponSpeed3").InnerText = rightWeaponSpeed[2] + "";

    //    character.SelectSingleNode("leftWeaponSpeed1").InnerText = leftWeaponSpeed[0] + "";
    //    character.SelectSingleNode("leftWeaponSpeed2").InnerText = leftWeaponSpeed[1] + "";
    //    character.SelectSingleNode("leftWeaponSpeed3").InnerText = leftWeaponSpeed[2] + "";

    //    xmlDoc.Save("./Assets/Resources/PlayerStatus.xml");
    //}

    public void SetGold(int addGold)
    {
        gold += addGold;
    }
    
    //IEnumerator WaitLoadXml()
    //{
    //    yield return new WaitForSeconds(2f);
    //    LoadXml(loadXml);
    //}

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

        head = statusManager.head;
        upperArmor = statusManager.upperArmor;
        lowerArmor = statusManager.lowerArmor;
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

        statusManager.head=head;
        statusManager.upperArmor=upperArmor;
        statusManager.lowerArmor=lowerArmor;
    }
}
