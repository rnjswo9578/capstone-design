using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lji_HPBar : MonoBehaviour
{
    public Image hpBar;
    public Text hpText;
    public GameObject character;
    lji_playerStatus status;

    private int maxHp;
    private int hp;
    
    // Start is called before the first frame update
    void Start()
    {
        //이곳에서 체력 초기화 하고
        status = character.GetComponent<lji_playerStatus>();
        maxHp = status.maxHp;
        hp = status.hp;
    }

    // Update is called once per frame
    void Update()
    {
        //여기서 플레이어 hp 계속 받아오면 된다. //character 활용
        if (maxHp != status.maxHp)
            maxHp = status.maxHp;

        hp = status.hp;

        hpBar.fillAmount = (float)hp / (float)maxHp;
        hpText.text = string.Format("HP {0} / {1}", hp, maxHp);
        hpText.color = Color.white;
    }
}
