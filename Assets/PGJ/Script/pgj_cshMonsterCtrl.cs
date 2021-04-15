using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pgj_cshMonsterCtrl : MonoBehaviour
{
    public Transform[] points;
    public int nextIndex = 1;

    public float damping = 5.0f;

    private Transform tr;
    private Transform playerTr;

    private Vector3 movePos;
    private bool targetDie = false;
    private bool isAttack = false;

    private Animator anim;
    private NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();

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
            AttackNavSetting();
        }
        else if (dist <= 40.0f)
        {
            movePos = playerTr.position;
            isAttack = false;
            ChaseNavSetting();
        }
        else
        {
            movePos = points[nextIndex].position;
            isAttack = false;
            ChaseNavSetting();
        }

        anim.SetBool("isAttack", isAttack);

        if (!isAttack) //�������� �ƴ� �� ���� ����Ʈ�� ������
        {
            Quaternion rot = Quaternion.LookRotation(movePos - tr.position);
            tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);
            nav.SetDestination(movePos);
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "WAY_POINT")
        {
            nextIndex = (++nextIndex == points.Length) ? 1 : nextIndex;
            
        }
    }
    void ChaseNavSetting() 
    {
        nav.isStopped = false;
        nav.ResetPath();
    }
    void AttackNavSetting()
    {
        nav.isStopped = true;
        nav.velocity = Vector3.zero;
    }
}
