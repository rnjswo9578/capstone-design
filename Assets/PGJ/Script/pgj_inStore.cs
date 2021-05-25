using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_inStore : MonoBehaviour
{
    public GameObject store;
    public GameObject player;
    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = player.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PLAYER")
        {
            //StartCoroutine("FadeOut");
            store.SetActive(false);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PLAYER")
        {
            //StartCoroutine("FadeIn");
            store.SetActive(true);
        }
    }

    IEnumerator FadeOut()
    {
        int i = 10;
        while (i > 0)
        {
            i -= 1;
            float f = i / 10.0f;
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator FadeIn()
    {
        int i = 0;
        while (i < 10)
        {
            i += 1;
            float f = i / 10.0f;
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
