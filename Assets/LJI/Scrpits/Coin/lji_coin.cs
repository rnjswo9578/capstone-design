using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lji_coin : MonoBehaviour
{
    public int minCoin = 0;
    public int maxCoin = 10;
    int coin = 0;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.up*450f);
        coin = Random.Range(minCoin, maxCoin);
        player = GameObject.FindWithTag("PLAYER");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PLAYER")
        {
            player.GetComponent<lji_playerStatus>().SetGold(coin);
            Destroy(this.gameObject);
        }
    }
}
