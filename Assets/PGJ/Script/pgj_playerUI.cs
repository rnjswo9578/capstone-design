using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class pgj_playerUI : MonoBehaviour
{
    public Animator textUIAnim; //text의 애니메이션을 가져다 쓰기 위해서 만듬
    public Text UIText;

    public int hp;
    private int damage;

    //private float timer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MONSTER_WEAPON")
        {
            textUIAnim.SetTrigger("isHit");
            damage = Random.Range(10, 15);
            hp = hp - damage;
            if (hp >= 0)
            {
                UIText.text = "-" + damage;
            }
            else
            {
                UIText.text = " ";
                //죽을때 애니매이션 연결
                StartCoroutine(PlayDeath(2f));//애니메이션 시간동안 타이머 설정
            }
        }
        else if (other.tag == "BEAM")
        {
            textUIAnim.SetTrigger("isHit");
            damage = Random.Range(20, 30);
            hp = hp - damage;
            if (hp >= 0)
            {
                UIText.text = "-" + damage;
            }
            else
            {
                UIText.text = " ";
                //죽을때 애니매이션 연결
                StartCoroutine(PlayDeath(2f));//애니메이션 시간동안 타이머 설정
            }
        }
    }

    IEnumerator PlayDeath(float time) 
    {
        yield return new WaitForSeconds(time);
        GameObject.FindGameObjectWithTag("PLAYER").SetActive(false);
    }
}
