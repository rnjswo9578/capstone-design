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

        weaponController.twoHandAxe.GetComponent<MeshCollider>().enabled = false;
        weaponController.twoHandSword.GetComponent<MeshCollider>().enabled = false;
        weaponController.twoHandSpear.GetComponent<MeshCollider>().enabled = false;
        weaponController.twoHandBow.GetComponent<BoxCollider>().enabled = false;
        weaponController.twoHandCrossbow.GetComponent<BoxCollider>().enabled = false;
        weaponController.staff.GetComponent<MeshCollider>().enabled = false;
        weaponController.swordL.GetComponent<MeshCollider>().enabled = false;
        weaponController.swordR.GetComponent<MeshCollider>().enabled = false;
        weaponController.maceL.GetComponent<MeshCollider>().enabled = false;
        weaponController.maceR.GetComponent<MeshCollider>().enabled = false;
        weaponController.daggerL.GetComponent<MeshCollider>().enabled = false;
        weaponController.daggerR.GetComponent<MeshCollider>().enabled = false;
        weaponController.itemL.GetComponent<MeshCollider>().enabled = false;
        weaponController.itemR.GetComponent<MeshCollider>().enabled = false;
        weaponController.shield.GetComponent<MeshCollider>().enabled = false;
        weaponController.pistolL.GetComponent<BoxCollider>().enabled = false;
        weaponController.pistolR.GetComponent<BoxCollider>().enabled = false;
        weaponController.rifle.GetComponent<BoxCollider>().enabled = false;
        weaponController.spear.GetComponent<MeshCollider>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (!characterController.CanStartAction("Attack") || characterController.isAttacking)//공격 중일 때
        {

            //양손 공격//양손 공격은 attackside가 running attack때 변경 안되서 따로 검사
            if (characterController.hasTwoHandedWeapon)
            {
                switch (characterController.rightWeapon)//양손 무기 번호가 왼손 무기로 오지만 사용은 그대로 오른쪽무기로 쓴다
                {
                    case (int)Weapon.TwoHandSword: weaponController.twoHandSword.GetComponent<MeshCollider>().enabled = true; break;
                    case (int)Weapon.TwoHandSpear: weaponController.twoHandSpear.GetComponent<MeshCollider>().enabled = true; break;
                    case (int)Weapon.TwoHandAxe: weaponController.twoHandAxe.GetComponent<MeshCollider>().enabled = true; break;
                    case (int)Weapon.TwoHandBow: weaponController.twoHandBow.GetComponent<BoxCollider>().enabled = true; break;
                    case (int)Weapon.TwoHandCrossbow: weaponController.twoHandCrossbow.GetComponent<BoxCollider>().enabled = true; break;
                    case (int)Weapon.TwoHandStaff: weaponController.staff.GetComponent<MeshCollider>().enabled = true; break;
                }
            }
            else if (characterInputController.playerStatus.side==1)//left공격
            {
                if (!characterController.hasLeftWeapon)//왼손 맨주먹일 때
                    handL.GetComponent<BoxCollider>().enabled = true;
                else
                {
                    switch (characterController.leftWeapon)
                    {
                        case (int)Weapon.Shield: weaponController.shield.GetComponent<MeshCollider>().enabled = true; break;
                        case (int)Weapon.LeftSword: weaponController.swordL.GetComponent<MeshCollider>().enabled = true; break;
                        case (int)Weapon.LeftMace: weaponController.maceL.GetComponent<MeshCollider>().enabled = true; break;
                        case (int)Weapon.LeftDagger: weaponController.daggerL.GetComponent<MeshCollider>().enabled = true; break;
                        case (int)Weapon.LeftItem: weaponController.itemL.GetComponent<MeshCollider>().enabled = true; break;
                        case (int)Weapon.LeftPistol: weaponController.pistolL.GetComponent<BoxCollider>().enabled = true; break;
                    }
                }
            }
            else//right공격
            {
                if (!characterController.hasRightWeapon)
                    handR.GetComponent<BoxCollider>().enabled = true;
                else
                {
                    switch (characterController.rightWeapon)
                    {
                        case (int)Weapon.RightSword: weaponController.swordR.GetComponent<MeshCollider>().enabled = true; break;
                        case (int)Weapon.RightMace: weaponController.maceR.GetComponent<MeshCollider>().enabled = true; break;
                        case (int)Weapon.RightDagger: weaponController.daggerR.GetComponent<MeshCollider>().enabled = true; break;
                        case (int)Weapon.RightItem: weaponController.itemR.GetComponent<MeshCollider>().enabled = true; break;
                        case (int)Weapon.RightPistol: weaponController.pistolR.GetComponent<BoxCollider>().enabled = true; break;
                        case (int)Weapon.Rifle: weaponController.rifle.GetComponent<BoxCollider>().enabled = true; break;
                        case (int)Weapon.RightSpear: weaponController.spear.GetComponent<MeshCollider>().enabled = true; break;
                    }
                }
            }

        }
        else //공격이 아닐때 collider 없애기
        {
            if (characterController.hasTwoHandedWeapon)//양손 공격
            {
                handL.GetComponent<BoxCollider>().enabled = false;
                handR.GetComponent<BoxCollider>().enabled = false;

                switch (characterController.rightWeapon)
                {
                    case (int)Weapon.TwoHandSword: weaponController.twoHandSword.GetComponent<MeshCollider>().enabled = false; break;
                    case (int)Weapon.TwoHandSpear: weaponController.twoHandSpear.GetComponent<MeshCollider>().enabled = false; break;
                    case (int)Weapon.TwoHandAxe: weaponController.twoHandAxe.GetComponent<MeshCollider>().enabled = false; break;
                    case (int)Weapon.TwoHandBow: weaponController.twoHandBow.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.TwoHandCrossbow: weaponController.twoHandCrossbow.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.TwoHandStaff: weaponController.staff.GetComponent<MeshCollider>().enabled = false; break;
                }
            }
            else
            {
                handL.GetComponent<BoxCollider>().enabled = false;

                switch (characterController.leftWeapon)
                {
                    case (int)Weapon.Shield: weaponController.shield.GetComponent<MeshCollider>().enabled = false; break;
                    case (int)Weapon.LeftSword: weaponController.swordL.GetComponent<MeshCollider>().enabled = false; break;
                    case (int)Weapon.LeftMace: weaponController.maceL.GetComponent<MeshCollider>().enabled = false; break;
                    case (int)Weapon.LeftDagger: weaponController.daggerL.GetComponent<MeshCollider>().enabled = false; break;
                    case (int)Weapon.LeftItem: weaponController.itemL.GetComponent<MeshCollider>().enabled = false; break;
                    case (int)Weapon.LeftPistol: weaponController.pistolL.GetComponent<BoxCollider>().enabled = false; break;
                }


                handR.GetComponent<BoxCollider>().enabled = false;


                switch (characterController.rightWeapon)
                {
                    case (int)Weapon.RightSword: weaponController.swordR.GetComponent<MeshCollider>().enabled = false; break;
                    case (int)Weapon.RightMace: weaponController.maceR.GetComponent<MeshCollider>().enabled = false; break;
                    case (int)Weapon.RightDagger: weaponController.daggerR.GetComponent<MeshCollider>().enabled = false; break;
                    case (int)Weapon.RightItem: weaponController.itemR.GetComponent<MeshCollider>().enabled = false; break;
                    case (int)Weapon.RightPistol: weaponController.pistolR.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.Rifle: weaponController.rifle.GetComponent<BoxCollider>().enabled = false; break;
                    case (int)Weapon.RightSpear: weaponController.spear.GetComponent<MeshCollider>().enabled = false; break;
                }

            }
        }


    }
}
