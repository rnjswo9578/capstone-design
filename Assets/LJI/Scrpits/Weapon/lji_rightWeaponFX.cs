using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims;

public class lji_rightWeaponFX : MonoBehaviour
{

    GameObject[] weaponFX = new GameObject[3];
    //public lji_characterInputContoller characterInputContoller;
    public lji_playerStatus playerStatus;

    int nowWeaponTier = 0;
    int newWeaponTier = 0;
    // Start is called before the first frame update
    void Start()
    {
        weaponFX[0] = transform.GetChild(0).gameObject;
        weaponFX[1] = transform.GetChild(1).gameObject;
        weaponFX[2] = transform.GetChild(2).gameObject;

        weaponFX[0].SetActive(false);
        weaponFX[1].SetActive(false);
        weaponFX[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        newWeaponTier = playerStatus.rightWeaponTier[playerStatus.nowWeaponSet];
        //오른손 무기
        //무기 티어가 달라 효과 on off 해야한다
        if (newWeaponTier != nowWeaponTier)
        {
            switch (newWeaponTier)
            {
                case 0:
                    weaponFX[0].SetActive(false);
                    weaponFX[1].SetActive(false);
                    weaponFX[2].SetActive(false);
                    break;
                case 1:
                    weaponFX[0].SetActive(true);
                    weaponFX[1].SetActive(false);
                    weaponFX[2].SetActive(false);
                    break;
                case 2:
                    weaponFX[0].SetActive(true);
                    weaponFX[1].SetActive(true);
                    weaponFX[2].SetActive(false);
                    break;
                case 3:
                    weaponFX[0].SetActive(true);
                    weaponFX[1].SetActive(true);
                    weaponFX[2].SetActive(true);
                    break;
            }

            nowWeaponTier = newWeaponTier;
        }
    }
}
