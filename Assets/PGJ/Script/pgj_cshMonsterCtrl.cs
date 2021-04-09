using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_cshMonsterCtrl : MonoBehaviour
{
    public Transform[] points;
    public int nextIndex = 1;

    public float speed = 20.0f;
    public float damping = 5.0f;

    private Transform tr;
    private Transform playerTr;

    private Vector3 movePos;
    private bool isAttack = false;
    private bool isObject = false;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {

        tr = this.GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
        points = GameObject.Find("WayPointGroup").GetComponentsInChildren<Transform>();

        anim = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(tr.position, playerTr.position);

        if (dist <= 7.0f)
        {
            isAttack = true;
        }
        else if (dist <= 35.0f)
        {
            movePos = playerTr.position;
            isAttack = false;
        }
        else
        {
            movePos = points[nextIndex].position;
            isAttack = false;
            isObject = false;
        }

        anim.SetBool("isAttack", isAttack);

        if (!isAttack && !isObject) //공격중이거나 오브젝트에 충돌중이 아닐 때 다음 포인트로 움직임
        {
            Quaternion rot = Quaternion.LookRotation(movePos - tr.position);
            tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);
            tr.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "WAY_POINT")
        {
            nextIndex = (++nextIndex == points.Length) ? 1 : nextIndex;
        }
        if (coll.tag == "WALL")
        {
            isObject = true;
        }
    }
}
