using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_invenSceneManager : MonoBehaviour
{
    public bool isInit = false;
    public int[] inventoryID = new int[50] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                                             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                                             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                                             0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] inventory2ID = new int[4] { 0, 0, 0, 0 };


    public static pgj_invenSceneManager INSTANCE = null;

    // Start is called before the first frame update
    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this; 
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            if (INSTANCE != this)
                Destroy(this.gameObject);
        }
    }

    public bool invenInit() 
    {     
        return isInit;
    }

    public void getInstance() 
    {

    }
    public void setInstance()
    {
        isInit = true;
    }

}


