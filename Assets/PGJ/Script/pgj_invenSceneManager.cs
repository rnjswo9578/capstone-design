using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pgj_invenSceneManager : MonoBehaviour
{
    private string isChange = "N";
    public bool needInven1Inint = false;
    public bool needInven2Inint = false;

    public int[] inventoryID = new int[50] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                                             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                                             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                                             0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] inventory2ID = new int[5] { 440, 450, 460, 098, 010 };


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

    private void Update()
    {
        if (!isChange.Equals(SceneManager.GetActiveScene().name))
        {
            if(needInven1Inint == false)
                needInven1Inint = true;
            if (needInven2Inint == false)
                needInven2Inint = true;
        }
    }

    public string getSceneName() 
    {     
        return isChange;
    }

    public void setInstance(string sceneName)
    {
        isChange = sceneName;
    }

}


