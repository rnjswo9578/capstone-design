using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class pgj_cshPlayerMove : MonoBehaviour
{
    public Animator textUIAnim; //text의 애니메이션을 가져다 쓰기 위해서 만듬
    public Text ScriptTxt;


    public int hp;
    private int damage;

    private float timer;

    private void Awake()
    {
        timer = 1f;
        ScriptTxt.text = "";
        hp = 100;
    }
    void Start()
    {

    }
    void Update()
    {



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MONSTER_WEAPON")
        {
            textUIAnim.SetTrigger("hit");
            damage = Random.Range(10, 15);
            hp = hp - damage;
            if (hp >= 0)
            {
                ScriptTxt.text = "-" + damage;
            }
            else
            {
                this.gameObject.tag = "PLAYER_DIE";
                //플레이어 죽음, 애니메이터와 연결시키면 된다.
            }
        }
    }
}
