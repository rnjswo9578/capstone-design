using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_inputCrtl : MonoBehaviour
{
    private bool isOn;

    public GameObject player;
    public GameObject inventory;
    public GameObject inventory2;
    public GameObject store1;
    public GameObject store2;
    public GameObject wornButton;
    public GameObject subWornButton;
    public GameObject sellbutton;
    public GameObject buy1button;
    public GameObject buy2button;

    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        inventory.SetActive(false);
        wornButton.SetActive(false);
        subWornButton.SetActive(false);
        player = GameObject.FindWithTag("PLAYER").GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("i");
            if (!isOn)
            {
                inventory.SetActive(true);
                inventory2.SetActive(true);
                wornButton.SetActive(true);
                subWornButton.SetActive(true);
                store1.SetActive(false);
                store2.SetActive(false);
                sellbutton.SetActive(false);
                buy1button.SetActive(false);
                buy2button.SetActive(false);
            }
            else
            {
                inventory.SetActive(false);
            }
            isOn = !isOn;
        }
    }
}
