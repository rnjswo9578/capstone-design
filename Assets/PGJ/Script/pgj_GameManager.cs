using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pgj_GameManager : MonoBehaviour
{
    public static pgj_GameManager instance;

    void Awake()
    {
        instance = this;
    }

    public void MoveToOtherScene(GameObject obj, int sceneNum)
    {
        Scene scene = SceneManager.GetSceneByBuildIndex(sceneNum);
        SceneManager.MoveGameObjectToScene(obj, scene);
    }
}
