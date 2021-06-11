using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims;
using RPGCharacterAnims.Actions;

public class lji_damage : MonoBehaviour
{

    RPGCharacterController characterController;
    lji_playerStatus playerStatus;
    // Start is called before the first frame update
    bool isDamage=false;
    public float damageTimer = 1f;
    int damage;
    lji_playerSounds sound;

    void Start()
    {
        characterController = GetComponent<RPGCharacterController>();
        playerStatus = GetComponent<lji_playerStatus>();

        sound = GetComponent<lji_playerSounds>();
        damage = 10;
    }
    
    public void setDamage(int getDamage)//외부에서 damage 수정
    {
        damage = getDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.tag == "MONSTER_WEAPON"|| other.gameObject.tag == "BEAM") && isDamage==false )
        {
            StartCoroutine(IsDamage());
            damage = damage - playerStatus.totalDefense;
            if (damage <= 0)
                damage = 1;
            playerStatus.hp -= damage;
            if (playerStatus.hp < 0)
                playerStatus.hp = 0;
            characterController.StartAction("GetHit",new HitContext());
            sound.PlaySound("Damaged");
        }
    }

    IEnumerator IsDamage()
    {
        isDamage = true;
        yield return new WaitForSeconds(damageTimer);
        isDamage = false;
    }
}
