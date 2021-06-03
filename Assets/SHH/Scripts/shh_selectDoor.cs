using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class shh_selectDoor : MonoBehaviour
{
    public Camera Cam;
    private bool isOpened=false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ClickDoor();
    }

    private void ClickDoor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.tag == "Door")
                {
                    GameObject door = hit.transform.gameObject;
                    if (isOpened == false)
                    {
                       // door.transform.GetChild(0).gameObject.transform.Rotate(0, 90, 0);
                        door.transform.GetChild(0).gameObject.transform.DORotate(new Vector3(0, -90, 0), 1);
                        door.transform.GetChild(1).gameObject.transform.DORotate(new Vector3(0, 90, 0), 1);
                       // door.transform.GetChild(1).gameObject.transform.Rotate(0, -90, 0);
                        isOpened = true;
                    }
                }
            }
        }


    }
}
