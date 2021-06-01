using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lji_sendDamage : MonoBehaviour
{
    public int damage = 20;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PLAYER")
        {
            player.GetComponent<lji_damage>().setDamage(damage);
        }
    }
}
