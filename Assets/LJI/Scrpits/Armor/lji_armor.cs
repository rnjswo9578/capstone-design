using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims;

public class lji_armor : MonoBehaviour
{
    lji_playerStatus playerStatus;

    [Header("Head")]
    public GameObject[] head = new GameObject[6];
    int preHead;
    [Header("Upper Armor")]
    public GameObject[] upperArmor = new GameObject[6];
    int preUpperArmor;
    [Header("Lower Armor")]
    public GameObject[] lowerArmor = new GameObject[6];
    int preLowerArmor;
    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GetComponent<lji_playerStatus>();
        ChangeArmor(1);
        ChangeArmor(2);
        ChangeArmor(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (preHead != playerStatus.head)
            ChangeArmor(1);

        if (preUpperArmor != playerStatus.upperArmor)
            ChangeArmor(2);

        if (preLowerArmor != playerStatus.lowerArmor)
            ChangeArmor(3);

    }

    void ChangeArmor(int part)
    {
        if (part == 1)
        {
            head[preHead].SetActive(false);
            head[playerStatus.head].SetActive(true);
            preHead = playerStatus.head;
        }
        else if(part == 2)
        {
            upperArmor[preUpperArmor].SetActive(false);
            upperArmor[playerStatus.upperArmor].SetActive(true);
            preUpperArmor = playerStatus.upperArmor;
        }
        else if(part == 3)
        {
            lowerArmor[preLowerArmor].SetActive(false);
            lowerArmor[playerStatus.lowerArmor].SetActive(true);
            preLowerArmor = playerStatus.lowerArmor;

        }
    }
    
}
