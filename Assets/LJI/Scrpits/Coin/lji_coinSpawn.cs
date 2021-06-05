using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lji_coinSpawn : MonoBehaviour
{
    public GameObject coin;
    GameObject portalManager;

    private void OnDestroy()
    {
        portalManager = GameObject.FindGameObjectWithTag("PortalManager");
        portalManager.GetComponent<lji_portalManager>().monsterNum--;
        Instantiate(coin, this.gameObject.transform.position, Quaternion.identity);
    }
}