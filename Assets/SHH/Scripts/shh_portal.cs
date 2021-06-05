using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class shh_portal : MonoBehaviour
{
    public string sceneName;
    public Slider timewait;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        SceneManager.LoadScene(sceneName);

        
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }
}
