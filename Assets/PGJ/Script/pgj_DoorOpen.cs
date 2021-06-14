using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_DoorOpen : MonoBehaviour
{
    public GameObject door;
    public GameObject player;
    private bool inPlayer;
    private bool isOpen;
    private bool isOpening;
    private float a;
    // Start is called before the first frame update
    void Start()
    {
        a = 0;
        inPlayer = false;
        isOpen = false;
        isOpening = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inPlayer && !isOpen)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("F");
                isOpening = true;
            }
        }
        if (isOpening && !isOpen)
        {
            door.transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
            a+=Time.deltaTime;
        }
        if (a > 1.2f)
            isOpen = true;
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
        }
    }
}
