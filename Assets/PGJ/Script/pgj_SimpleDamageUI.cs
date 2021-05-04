using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_SimpleDamageUI : MonoBehaviour
{
    private Transform cameraTr;
    private Transform myTr;
    // Start is called before the first frame update
    void Start()
    {
        cameraTr = Camera.main.transform;
        myTr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        myTr.LookAt(cameraTr.position);
    }
}
