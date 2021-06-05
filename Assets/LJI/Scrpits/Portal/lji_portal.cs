using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lji_portal : MonoBehaviour
{
    public string nextScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PLAYER")
        {
            //SceneManager.LoadScene(nextScene);
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(nextScene);
        while (!asyncOper.isDone)
        {
            yield return null;
            Debug.Log(asyncOper.progress);
        }
    }
}
