using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
//lji 수정
using RPGCharacterAnims;

public class pgj_cshBossCtrl : MonoBehaviour
{
    public GameObject bloodFX;
    public Transform point;
    public Transform defaultPoint;

    public float damping = 5.0f;
    public int maxhp = 100;
    private int hp;
    private int pattern = 0;
    private float distPoint;

    private Transform tr;
    private Transform playerTr;
    public BoxCollider myWeapon;
    public Image hpBar;

    private Vector3 movePos;
    private bool isAttack = false;
    private bool isIdle = true;
    private bool isDead = false;

    private Animator anim;
    private NavMeshAgent nav;
    private Canvas canvas;

    //lji 수정
    public GameObject player;
    lji_playerStatus playerStatus;
    private bool isDamage = false;

    lji_monsterSounds sound;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;
        nav = GetComponent<NavMeshAgent>();

        tr = this.GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
        canvas = this.GetComponentInChildren<Canvas>();

        anim = this.GetComponentInChildren<Animator>();


        myWeapon.enabled = false;
        
        //lji 수정
        playerStatus = player.GetComponent<lji_playerStatus>();
        player = GameObject.FindGameObjectWithTag("PLAYER");

        sound = GetComponent<lji_monsterSounds>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
            player = GameObject.FindGameObjectWithTag("PLAYER");
            playerStatus = player.GetComponent<lji_playerStatus>();
        }
        distPoint = Vector3.Distance(tr.position, point.position);

        

        if (!isDead)
        {
            float dist = Vector3.Distance(tr.position, playerTr.position);

            if (dist <= 4f)
            {
                isAttack = true;
                isIdle = false;
                AttackNavSetting();
                Attack();
            }
            else if (dist <= 13.0f)
            {
                pattern = 0;
                movePos = playerTr.position;
                isAttack = false;
                isIdle = false;
                ChaseNavSetting();
                myWeapon.enabled = false;
            }
            else
            {
                pattern = 0;
                movePos = point.position;
                isAttack = false;
                isIdle = false;
                myWeapon.enabled = false;
                if (distPoint < 1)
                {
                    AttackNavSetting();
                    isIdle = true;

                    Transform newPoint = defaultPoint;
                    defaultPoint = point;
                    point = newPoint;

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
        if (other.tag == "PLAYER_WEAPON"&&isDamage==false)
        {
            Instantiate(bloodFX,gameObject.transform.position, Quaternion.identity);
            //textUIAnim.SetTrigger("isHit");
            //damage = Random.Range(10, 15);
            //hp = hp - damage;
            //if (hp >= 0)
            //{
            //    hpBar.fillAmount = (float)hp / (float)maxhp;
            //}
            //else
            //{
            //    hpBar.fillAmount = 0;
            //    isDead = true;
            //    anim.SetBool("isDead", isDead);
            //    AttackNavSetting();
            //    StartCoroutine(MonsterDeath(2f));
            //    canvas.gameObject.SetActive(false);
            //    myWeapon.enabled = false;
            //}
            StartCoroutine(DamageTimer());
            playerAttack();
            sound.PlaySound("Damaged");
        }
    }
    void Attack()
    {
        if (pattern == 0)
        {
            pattern = 1;
            StartCoroutine(AttackTimer(0.7f, 0.5f));
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("pattern1") && pattern == 1)
        {
            pattern = 2;
            StartCoroutine(AttackTimer(0.5f,0.5f));
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("pattern2") && pattern == 2)
        {
            pattern = 3;
            StartCoroutine(AttackTimer(0.2f,0.3f));
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("pattern3") && pattern == 3)
        {
            pattern = 4;
            StartCoroutine(AttackTimer(0.2f, 0.4f));
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("pattern4") && pattern == 4)
        {
            pattern = 1;
            StartCoroutine(AttackTimer(0.7f, 0.5f));
        }
    }

    //lji 수정
    void playerAttack()
    {
        int damage = 10;
        int weapon;
        int weaponTierDamage;
        if (playerStatus.side == 2)//오른손 공격
        {
            weapon = playerStatus.rightWeapon[playerStatus.nowWeaponSet];
            weaponTierDamage = playerStatus.rightWeaponTier[playerStatus.nowWeaponSet] * 5;
        }
        else//왼손 공격
        {
            weapon = playerStatus.leftWeapon[playerStatus.nowWeaponSet];
            weaponTierDamage = playerStatus.leftWeaponTier[playerStatus.nowWeaponSet] * 5;
        }
        switch (weapon)//무기 초기 대미지 값
        {
            case (int)Weapon.Unarmed: damage = 10; break;
            case (int)Weapon.Shield: damage = 10; break;
            case (int)Weapon.LeftDagger: damage = 15; break;
            case (int)Weapon.LeftItem: damage = 15; break;
            case (int)Weapon.LeftMace: damage = 20; break;
            case (int)Weapon.LeftSword: damage = 15; break;
            case (int)Weapon.RightDagger: damage = 15; break;
            case (int)Weapon.RightItem: damage = 15; break;
            case (int)Weapon.RightMace: damage = 25; break;
            case (int)Weapon.RightSword: damage = 20; break;
            case (int)Weapon.RightSpear: damage = 25; break;
            case (int)Weapon.TwoHandAxe: damage = 50; break;
            case (int)Weapon.TwoHandSpear: damage = 40; break;
            case (int)Weapon.TwoHandSword: damage = 45; break;
        }
        Debug.Log("weapon"+weapon);
        Debug.Log("damage"+damage);
        damage += weaponTierDamage + playerStatus.totalAttackPower;

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
    IEnumerator DamageTimer()
    {
        isDamage = true;
        yield return new WaitForSeconds(1f);
        isDamage = false;
    }
    //
    IEnumerator MonsterDeath(float time)
    {
        yield return new WaitForSeconds(time);
        //GameObject.FindGameObjectWithTag("PLAYER").SetActive(false);
        anim.speed = 0;
        Destroy(this.gameObject);
        //myAnimator.speed = 0.0;
    }
    IEnumerator AttackTimer(float firstTime, float secondTime)
    {
        yield return new WaitForSeconds(firstTime);
        myWeapon.enabled = true;
        sound.PlaySound("Attack");
        yield return new WaitForSeconds(secondTime);
        myWeapon.enabled = false;
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
