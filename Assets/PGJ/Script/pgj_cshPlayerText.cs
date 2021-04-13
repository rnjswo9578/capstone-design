using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_cshPlayerText : MonoBehaviour
{
    private Transform cameraTr;
    private Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        cameraTr = Camera.main.transform;
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        tr.LookAt(cameraTr.position);
    }
}
