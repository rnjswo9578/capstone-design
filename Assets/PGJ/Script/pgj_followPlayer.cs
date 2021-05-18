using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_followPlayer : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = new Vector3(0, 13, -20);
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform);
    }
}
