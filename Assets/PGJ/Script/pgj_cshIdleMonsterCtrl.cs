using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pgj_cshIdleMonsterCtrl : MonoBehaviour
{

    public Transform point;

    public float damping = 5.0f;
    public float crossLoad;

    private Transform tr;
    private Transform playerTr;

    private Vector3 movePos;

    private bool isAttack = false;
    private bool isIdle = true;

    private Animator anim;
    private NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();

        tr = this.GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();

        anim = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(tr.position, playerTr.position);
        float distPoint = Vector3.Distance(tr.position, point.position);

        if (dist <= crossLoad)
        {
            isAttack = true;
            isIdle = false;
            AttackNavSetting();
        }
        else if (dist <= 20.0f)
        {
            movePos = playerTr.position;
            isAttack = false;
            isIdle = false;
            ChaseNavSetting();
        }
        else
        {
            movePos = point.position;
            isAttack = false;
            if (distPoint < 1)
            {
                AttackNavSetting();
                isIdle = true;
            }
            else
            {
                ChaseNavSetting();
            }
        }

        anim.SetBool("isAttack", isAttack);
        anim.SetBool("isIdle", isIdle);

        if (!isAttack && !isIdle)
        {
            Quaternion rot = Quaternion.LookRotation(movePos - tr.position);
            tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);
            nav.SetDestination(movePos);
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
