using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lji_playerStatus : MonoBehaviour
{
    public int maxHp;
    public int hp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //hp�� maxHp�� �Ѿ�� �ȵȴ�.
        if (hp > maxHp)
            hp = maxHp;
    }
}
