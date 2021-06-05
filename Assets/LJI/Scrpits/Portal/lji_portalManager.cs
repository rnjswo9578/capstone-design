using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lji_portalManager : MonoBehaviour
{
    public GameObject startPortal;
    public GameObject [] endPortal=new GameObject [2];
    public int monsterNum;
    // Start is called before the first frame update
    void Start()
    {
        endPortal[0].SetActive(false);
        if(endPortal[1]!=null)
            endPortal[1].SetActive(false);
        //GameObject.FindGameObjectWithTag("PLAYER").transform.position = startPortal.transform.position;
        Destroy(startPortal, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (monsterNum <= 0)
        {
            endPortal[0].SetActive(true);
            if (endPortal[1] != null)
                endPortal[1].SetActive(true);
        }
            
    }
    
}
