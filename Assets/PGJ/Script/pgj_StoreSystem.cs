using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_StoreSystem : MonoBehaviour
{
    private bool inPlayer;
    public GameObject player;
    public GameObject inventory;

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
            Debug.Log("in");
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("F");
                inventory.SetActive(true);
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
