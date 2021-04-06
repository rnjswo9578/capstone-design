using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_cshCharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    private float rote = 0.0f;
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (rote >= 360) {
            rote = rote % 360;
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Rotate(0.0f, rote, 0.0f);
            this.transform.Translate(new Vector3(0.0f, 0.0f, 8f * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                rote = rote + 180;
                this.transform.rotation = Quaternion.Euler(new Vector3(rote, 0, 0));
            }
            this.transform.Translate(new Vector3(0.0f, 0.0f, 8f * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                rote = rote + 90;
                this.transform.rotation = Quaternion.Euler(new Vector3(rote, 0, 0));
            }
            this.transform.Translate(new Vector3(0.0f, 0.0f, 8f * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                rote = rote - 90;
                this.transform.rotation = Quaternion.Euler(new Vector3(rote, 0, 0));
            }
            this.transform.Translate(new Vector3(0.0f, 0.0f, 8f * Time.deltaTime));
        }
    }
}
