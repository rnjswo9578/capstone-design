using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lji_startButton : MonoBehaviour
{
    public string nextScene="Scene1";

    public AudioClip audioClick;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClick;
    }

    public void LoadSceneButton()
    {
        //Debug.Log("pressButton");
        audioSource.Play();
        StartCoroutine(LoadScene());
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

    public void ExitProgram()
    {
        audioSource.Play();
        Application.Quit();
    }
}
