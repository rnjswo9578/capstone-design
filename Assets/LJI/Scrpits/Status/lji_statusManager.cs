using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims;

public class lji_statusManager : MonoBehaviour
{
    private static lji_statusManager instance = null;

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
    }
}