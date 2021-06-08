using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lji_particleLifeTime : MonoBehaviour
{
    public float lifetime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
