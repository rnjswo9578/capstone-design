using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lji_coinSpawn : MonoBehaviour
{
    public GameObject coin;

    private void OnDestroy()
    {
        Instantiate(coin, this.gameObject.transform.position, Quaternion.identity);
    }
}