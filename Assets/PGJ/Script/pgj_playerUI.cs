using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class pgj_playerUI : MonoBehaviour
{
    public Animator textUIAnim; //text�� �ִϸ��̼��� ������ ���� ���ؼ� ����
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
                //������ �ִϸ��̼� ����
                StartCoroutine(PlayDeath(2f));//�ִϸ��̼� �ð����� Ÿ�̸� ����
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
                //������ �ִϸ��̼� ����
                StartCoroutine(PlayDeath(2f));//�ִϸ��̼� �ð����� Ÿ�̸� ����
            }
        }
    }

    IEnumerator PlayDeath(float time) 
    {
        yield return new WaitForSeconds(time);
        GameObject.FindGameObjectWithTag("PLAYER").SetActive(false);
    }
}
