using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lji_GoldText : MonoBehaviour
{
    public Text goldText;
    lji_playerStatus playerStatus;
    // Start is called before the first frame update
    void Start()
    {
        playerStatus =GetComponent<lji_playerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = "GOLD : " + playerStatus.gold.ToString();
    }
}
