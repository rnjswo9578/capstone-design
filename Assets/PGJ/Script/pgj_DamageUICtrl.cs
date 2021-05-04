using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_DamageUICtrl : MonoBehaviour
{

    public Animator damageUIAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "MONSTER_WEAPON")
        {
            damageUIAnim.SetTrigger("isHit");
        }
    }
}
