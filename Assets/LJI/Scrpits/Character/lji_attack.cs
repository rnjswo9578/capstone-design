using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims.Actions;
using RPGCharacterAnims;

public class lji_attack : MonoBehaviour
{
    public GameObject handL;
    public GameObject handR;
    Animator animator;

    Dictionary<int, string> weaponDictionary = new Dictionary<int, string>();

    RPGCharacterController characterController;
    RPGCharacterWeaponController weaponController;
    lji_characterInputContoller characterInputController;

    Attack attack;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<RPGCharacterController>();
        weaponController = GetComponent<RPGCharacterWeaponController>();
        characterInputController = GetComponent<lji_characterInputContoller>();

        handL.GetComponent<BoxCollider>().enabled = false;
        handR.GetComponent<BoxCollider>().enabled = false;

        weaponController.twoHandAxe.GetComponent<BoxCollider>().enabled = false;
        weaponController.twoHandSword.GetComponent<BoxCollider>().enabled = false;
        weaponController.twoHandSpear.GetComponent<BoxCollider>().enabled = false;
        weaponController.twoHandBow.GetComponent<BoxCollider>().enabled = false;
        weaponController.twoHandCrossbow.GetComponent<BoxCollider>().enabled = false;
        weaponController.staff.GetComponent<BoxCollider>().enabled = false;
        weaponController.swordL.GetComponent<BoxCollider>().enabled = false;
        weaponController.swordR.GetComponent<BoxCollider>().enabled = false;
        weaponController.maceL.GetComponent<BoxCollider>().enabled = false;
        weaponController.maceR.GetComponent<BoxCollider>().enabled = false;
        weaponController.daggerL.GetComponent<BoxCollider>().enabled = false;
        weaponController.daggerR.GetComponent<BoxCollider>().enabled = false;
        weaponController.itemL.GetComponent<BoxCollider>().enabled = false;
        weaponController.itemR.GetComponent<BoxCollider>().enabled = false;
        weaponController.shield.GetComponent<BoxCollider>().enabled = false;
        weaponController.pistolL.GetComponent<BoxCollider>().enabled = false;
        weaponController.pistolR.GetComponent<BoxCollider>().enabled = false;
        weaponController.rifle.GetComponent<BoxCollider>().enabled = false;
        weaponController.spear.GetComponent<BoxCollider>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (!characterController.CanStartAction("Attack") || characterController.isAttacking)//���� ���� ��
        {

            //��� ����//��� ������ attackside�� running attack�� ���� �ȵǼ� ���� �˻�
            if (characterController.hasTwoHandedWeapon)
            {
                switch (characterController.rightWeapon)//��� ���� ��ȣ�� �޼� ����� ������ ����� �״�� �����ʹ���� ����
                {
                    case (int)Weapon.TwoHandSword: weaponController.twoHandSword.GetComponent<BoxCollider>().enabled = true; break;
                    case (int)Weapon.TwoHandSpear: weaponController.twoHandSpear.GetComponent<BoxCollider>().enabled = true; break;
                    case (int)Weapon.TwoHandAxe: weaponController.twoHandAxe.GetComponent<BoxCollider>().enabled = true; break;
                    case (int)Weapon.TwoHandBow: weaponController.twoHandBow.GetComponent<BoxCollider>().enabled = true; break;
                    case (int)Weapon.TwoHandCrossbow: weaponController.twoHandCrossbow.GetComponent<BoxCollider>().enabled = true; break;
                    case (int)Weapon.TwoHandStaff: weaponController.staff.GetComponent<BoxCollider>().enabled = true; break;
                }
            }
            else if (characterInputController.side==1)//left����
            {
                if (!characterController.hasLeftWeapon)//�޼� ���ָ��� ��
                    handL.GetComponent<BoxCollider>().enabled = true;
                else
                {
                    switch (characterController.leftWeapon)
                    {
                        case (int)Weapon.Shield: weaponController.shield.GetComponent<BoxCollider>().enabled = true; break;
                        case (int)Weapon.LeftSword: weaponController.swordL.GetComponent<BoxCollider>().enabled = true; break;
                        case (int)Weapon.LeftMace: weaponController.maceL.GetComponent<BoxCollider>().enabled = true; break;
                        case (int)Weapon.LeftDagger: weaponController.daggerL.GetComponent<BoxCollider>().enabled = true; break;
                        case (int)Weapon.LeftItem: weaponController.itemL.GetComponent<BoxCollider>().enabled = true; break;
                        case (int)Weapon.LeftPistol: weaponController.pistolL.GetComponent<BoxCollider>().enabled = true; break;
                    }
                }
            }
            else//right����
            {
                if (!characterController.hasRightWeapon)
                    handR.GetComponent<BoxCollider>().enabled = true;
                else
                {
                    switch (characterController.rightWeapon)
                    {
                        case (int)Weapon.RightSword: weaponController.swordR.GetComponent<BoxCollider>().enabled = true; break;
                        case (int)Weapon.RightMace: weaponController.maceR.GetComponent<BoxCollider>().enabled = true; break;
                        case (int)Weapon.RightDagger: weaponController.daggerR.GetComponent<BoxCollider>().enabled = true; break;
                        case (int)Weapon.RightItem: weaponController.itemR.GetComponent<BoxCollider>().enabled = true; break;
                        case (int)Weapon.RightPistol: weaponController.pistolR.GetComponent<BoxCollider>().enabled = true; break;
                        case (int)Weapon.Rifle: weaponController.rifle.GetComponent<BoxCollider>().enabled = true; break;
                        case (int)Weapon.RightSpear: weaponController.spear.GetComponent<BoxCollider>().enabled = true; break;
                    }
                }
            }

        }
        else //������ �ƴҶ� collider ���ֱ�
        {
            if (characterController.hasTwoHandedWeapon)//��� ����
            {
                handL.GetComponent<BoxCollider>().enabled = false;
                handR.GetComponent<BoxCollider>().enabled = false;

                switch (characterController.rightWeapon)
                {
                    case (int)Weapon.TwoHandSword: weaponController.twoHandSword.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.TwoHandSpear: weaponController.twoHandSpear.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.TwoHandAxe: weaponController.twoHandAxe.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.TwoHandBow: weaponController.twoHandBow.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.TwoHandCrossbow: weaponController.twoHandCrossbow.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.TwoHandStaff: weaponController.staff.GetComponent<BoxCollider>().enabled = false; break;
                }
            }
            else
            {
                handL.GetComponent<BoxCollider>().enabled = false;

                switch (characterController.leftWeapon)
                {
                    case (int)Weapon.Shield: weaponController.shield.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.LeftSword: weaponController.swordL.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.LeftMace: weaponController.maceL.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.LeftDagger: weaponController.daggerL.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.LeftItem: weaponController.itemL.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.LeftPistol: weaponController.pistolL.GetComponent<BoxCollider>().enabled = false; break;
                }


                handR.GetComponent<BoxCollider>().enabled = false;


                switch (characterController.rightWeapon)
                {
                    case (int)Weapon.RightSword: weaponController.swordR.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.RightMace: weaponController.maceR.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.RightDagger: weaponController.daggerR.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.RightItem: weaponController.itemR.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.RightPistol: weaponController.pistolR.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.Rifle: weaponController.rifle.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.RightSpear: weaponController.spear.GetComponent<BoxCollider>().enabled = false; break;
                }

            }
        }


    }
}
