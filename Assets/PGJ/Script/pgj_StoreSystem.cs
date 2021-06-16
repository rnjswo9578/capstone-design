using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_StoreSystem : MonoBehaviour
{
    private bool inPlayer;
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
        inPlayer = false;
        isOn = false;
        inventory.SetActive(false);
        wornButton.SetActive(false);
        subWornButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inPlayer)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("F");
                inventory.SetActive(true);
                sellbutton.SetActive(true);
                inventory2.SetActive(false);
                wornButton.SetActive(false);
                subWornButton.SetActive(false);
                if (transform.name == "store1FX")
                {
                    buy1button.SetActive(true);
                    buy2button.SetActive(false);
                    store1.SetActive(true);
                    store2.SetActive(false);
                }
                else if (transform.name == "store2FX")
                {
                    buy1button.SetActive(false);
                    buy2button.SetActive(true);
                    store1.SetActive(false);
                    store2.SetActive(true);
                }
            }
        }

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PLAYER")
        {
            inPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PLAYER")
        {
            inPlayer = false;
            inventory.SetActive(false);
        }
    }
}
