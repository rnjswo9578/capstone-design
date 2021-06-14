using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_StoreSystem : MonoBehaviour
{
    private bool inPlayer;
    public GameObject player;
    public GameObject inventory;
    public GameObject store1;
    public GameObject store2;

    // Start is called before the first frame update
    void Start()
    {
        inPlayer = false;
        inventory.SetActive(false);
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
                if (transform.name == "store1FX")
                {
                    store1.SetActive(true);
                    store2.SetActive(false);
                }
                else if (transform.name == "store2FX")
                {
                    store1.SetActive(false);
                    store2.SetActive(true);
                }
            }
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
