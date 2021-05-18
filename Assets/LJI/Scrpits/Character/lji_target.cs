using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lji_target : MonoBehaviour
{
    public Camera camera;
    public Transform target;

    Vector3 newPosition;
    // Start is called before the first frame update
    void Start()
    {
        newPosition.y = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            newPosition.x = hit.point.x;
            newPosition.z = hit.point.z;
            target.position = newPosition;
        }
    }
}
