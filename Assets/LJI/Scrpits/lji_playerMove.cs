using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lji_playerMove : MonoBehaviour
{

    float speed = 10f;
    float rotateSpeed = 10f;

    Animator animator;
    Rigidbody rigidbody;
    Vector3 movement; //물체의 xyz값 담을 변수
    Quaternion newRotation;
    float h, v; //horizontal, vertical

    bool isDash = false;
    float dashTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        PlayerMove();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDash = true;
        }
        if (Input.GetMouseButtonDown(0))//마우스 왼쪽:0 마우스 오른쪽:1
        {
            animator.SetTrigger("Attack");
        }
        if (Input.GetMouseButtonDown(1))
        {

        }
       
    }
    private void PlayerMove()
    {
        CharacterController controller = GetComponent<CharacterController>();
        float gravity = 20.0f;
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        
        if (controller.isGrounded)
        {
            if (isDash)//대쉬 움직임
            {
                Dash(controller);
            }
            else //기본 움직임
            {
                //플레이어의 움직임 부분
                movement = new Vector3(h, 0, v);//가로값 세로값 방향 셋
                movement = movement.normalized;
                animator.SetFloat("Walk", movement.magnitude);
             
                if (movement.magnitude > 0.5)
                {
                    transform.LookAt(transform.position + movement);
                }

                
            }
        }

        movement.y -= gravity * Time.deltaTime;
        controller.Move(movement * speed * Time.deltaTime);
    }

    private void Dash(CharacterController controller)
    {
        if (dashTimer == 0)//대쉬 시작할 때. 이때 방향을 고정시켜줘야할 것 같다
        {
            if (h == 0 && v == 0)
            {
                movement.Set(0, 0f, 1);//아무 키 안누르고 대쉬 누르면 전진 이동
            }
            else
            {
                movement.Set(h, 0f, v);
            }

            movement = movement.normalized;

            animator.SetFloat("Walk", movement.magnitude);

            if (movement.magnitude > 0.5)
            {
                transform.LookAt(transform.position + movement);
            }

            dashTimer += Time.deltaTime;
        }
        else
        {
            if (dashTimer > 0.15f)//dash 시간 설정하기
            {
                isDash = false;
                dashTimer = 0f;
            }
            else //대쉬 중
            {
                movement = movement.normalized;//숫자는 대쉬할 때 배속되는 속도
                
                controller.Move(movement * speed * Time.deltaTime * 5);
                dashTimer += Time.deltaTime;
            }
        }
    }
}
