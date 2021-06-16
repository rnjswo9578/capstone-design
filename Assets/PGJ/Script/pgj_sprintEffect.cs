using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_sprintEffect : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject jumpPrefab;
    public GameObject sprintPrefab;

    private GameObject jump;
    private GameObject sprint;

    private ParticleSystem jumpFx;
    private ParticleSystem sprintFx;


    void Start()
    {
        jump = Instantiate(jumpPrefab, this.transform);
        sprint = Instantiate(sprintPrefab, this.transform);

        jumpPrefab.transform.position = new Vector3(0, 0, 0);
        sprintPrefab.transform.position = new Vector3(0, 0.2f, 0);

        jumpPrefab.transform.localScale = new Vector3(2, 2, 2);
        sprintPrefab.transform.localScale = new Vector3(1, 1, 1);

        jumpFx = jump.GetComponent<ParticleSystem>();
        sprintFx = sprint.GetComponent<ParticleSystem>();

        jumpFx.Stop();
        sprintFx.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            jumpFx.Play();
            sprintFx.Play(); 
        }
        if (sprintFx.time >= 0.5f)
            sprintFx.Stop();
    }
}
