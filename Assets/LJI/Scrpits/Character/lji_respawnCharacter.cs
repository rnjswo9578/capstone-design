using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lji_respawnCharacter : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject respawnPoint;

    private void OnDestroy()
    {
        Instantiate(playerPrefab, respawnPoint.transform.position, Quaternion.identity);
    }
}
