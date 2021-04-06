using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_cshThirdCamera : MonoBehaviour
{
    public GameObject target;
    public Transform target2;

    public float currentZoom = 7.0f;

    public float minZoom = 5.0f;
    public float maxZoom = 10.0f;

    public float offsetX;
    public float offsetY;
    public float offsetZ;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector3(offsetX, offsetY, offsetZ);

        // ���콺 �ٷ� �� �ξƿ�
        currentZoom -= Input.GetAxis("Mouse ScrollWheel");
        // �� �ּ� �� �ִ� ���� 
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

    }

    void LateUpdate()
    {
        // ����� ī�޶� ��ġ ����
        transform.position = target2.position + offset * currentZoom;
        transform.LookAt(target2);
    }
}
