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
    void Start()
    {
        characterController = GetComponent<RPGCharacterController>();
        playerStatus = GetComponent<lji_playerStatus>();

        damage = 10;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MONSTER_WEAPON"&& isDamage==false )
        {
            StartCoroutine(IsDamage());
            playerStatus.hp -= damage;
            characterController.StartAction("GetHit",new HitContext());
        }
    }

    IEnumerator IsDamage()
    {
        isDamage = true;
        yield return new WaitForSeconds(damageTimer);
        isDamage = false;
    }
}
