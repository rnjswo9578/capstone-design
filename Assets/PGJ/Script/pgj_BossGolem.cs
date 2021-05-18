using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class pgj_BossGolem : MonoBehaviour
{
    public Transform point;

    public float damping = 5.0f;
    public int maxhp = 100;
    private int hp;
    private int pattern = 0;
    private float distPoint;

    private Transform tr;
    private Transform playerTr;
    public BoxCollider myWeapon;
    public BoxCollider beam;
    public Image hpBar;
    public ParticleSystem beamFx;
    public ParticleSystem drawFx;
    public ParticleSystem nextPhaseFX;

    private Vector3 movePos;
    private bool isAttack = false;
    private bool isIdle = true;
    private bool isDead = false;
    private bool nextPhase = false;

    private Animator anim;
    private NavMeshAgent nav;
    private Canvas canvas;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 2, 0);
        hp = maxhp;
        nav = this.GetComponent<NavMeshAgent>();

        tr = this.GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
        canvas = this.GetComponentInChildren<Canvas>();

        anim = this.GetComponentInChildren<Animator>();

        anim.speed =0.8f;
        myWeapon.enabled = false;
        beam.enabled = false;
        beamFx.Stop();
        drawFx.Stop();
        nextPhaseFX.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        distPoint = Vector3.Distance(tr.position, point.position);

        if (hp <= (maxhp / 2) && !nextPhase)
        {
            nextPhase = true;
            anim.SetTrigger("halfHP");
            anim.speed = 0.3f;
            nextPhaseFX.Play();
        }


        if (!isDead)
        {
            float dist = Vector3.Distance(tr.position, playerTr.position);
            beamFx.transform.LookAt(playerTr.position + offset);

            if (dist <= 10f || anim.GetCurrentAnimatorStateInfo(0).IsName("pattern5"))
            {
                isAttack = true;
                isIdle = false;
                AttackNavSetting();
                Attack();
                if(anim.GetCurrentAnimatorStateInfo(0).IsName("pattern5"))
                    this.transform.LookAt(playerTr.position );
            }
            else if (dist <= 30.0f && !anim.GetCurrentAnimatorStateInfo(0).IsName("pattern5"))
            {
                movePos = playerTr.position;
                isAttack = false;
                isIdle = false;
                ChaseNavSetting();
                beamFx.Stop();
                drawFx.Stop();
                
                myWeapon.enabled = false;
            }
            else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("pattern5"))
            {
                movePos = point.position;
                isAttack = false;
                myWeapon.enabled = false;
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
            anim.SetInteger("pattern", pattern);



            Quaternion rot = Quaternion.LookRotation(movePos - tr.position);
            tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);
            nav.SetDestination(movePos);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        int damage;

        if (other.tag == "PLAYER_WEAPON")
        {
            //textUIAnim.SetTrigger("isHit");
            damage = Random.Range(10, 15);
            hp = hp - damage;
            if (hp >= 0)
            {
                hpBar.fillAmount = (float)hp / (float)maxhp;
            }
            else
            {
                hpBar.fillAmount = 0;
                isDead = true;
                anim.SetBool("isDead", isDead);
                AttackNavSetting();
                StartCoroutine(MonsterDeath(2f));
                canvas.gameObject.SetActive(false);
                myWeapon.enabled = false;
            }
        }
    }
    void Attack()
    {
        if (pattern == 0)
        {
            pattern = 1;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("pattern1") && pattern == 1)
        {
            pattern = 2;
            StartCoroutine(AttackTimer(0.5f, 0.5f));
            movePos = playerTr.position;

        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("pattern2") && pattern == 2)
        {
            pattern = 3;
            StartCoroutine(AttackTimer(0.5f, 0.4f));
            movePos = playerTr.position;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("pattern3") && pattern == 3)
        {
            pattern = 4;
            StartCoroutine(AttackTimer(0.1f, 0.6f));
            movePos = playerTr.position;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("pattern4") && pattern == 4)
        {
            pattern = 5;
            StartCoroutine(AttackTimer(0.5f, 0.5f));
            movePos = playerTr.position;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("pattern5") && pattern == 5)
        {
            beamFx.Play();
            drawFx.Play();
            StartCoroutine(BeamAttackTimer(2f, 2f));
            pattern = 1;
            anim.speed = 0.2f;
        }
        else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("pattern5") && pattern != 5)
        {
            ChaseNavSetting();
            anim.speed = 0.8f;
            beamFx.Stop();
            drawFx.Stop();
        }
    }

    IEnumerator MonsterDeath(float time)
    {
        yield return new WaitForSeconds(time);
        //GameObject.FindGameObjectWithTag("PLAYER").SetActive(false);
        anim.speed = 0;
        //myAnimator.speed = 0.0;
    }
    IEnumerator AttackTimer(float firstTime, float secondTime)
    {
        yield return new WaitForSeconds(firstTime);
        myWeapon.enabled = true;
        yield return new WaitForSeconds(secondTime);
        myWeapon.enabled = false;
    }
    IEnumerator BeamAttackTimer(float firstTime, float secondTime)
    {
        yield return new WaitForSeconds(firstTime);
        beam.enabled = true;
        yield return new WaitForSeconds(secondTime);
        beam.enabled = false;
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