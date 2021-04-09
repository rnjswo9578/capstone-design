using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    float speed = 10f;
    float rotateSpeed = 10f;

    Rigidbody rigidbody;
    Vector3 movement; //물체의 xyz값 담을 변수
    Quaternion newRotation;
    float h, v; //horizontal, vertical

    bool isDash=false;
    float dashTimer = 0f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();//물체의 rigidbody 받아옴
    }
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDash = true;
        }
        if (Input.GetMouseButtonDown(0))//마우스 왼쪽:0 마우스 오른쪽:1
        {

        }
        if (Input.GetMouseButtonDown(1))
        {

        }

    }
    void FixedUpdate() //rigidBody 있는 물체 움직일 때 사용하자
    {
        if (isDash)//대쉬 움직임
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

                //대쉬 방향 바라보기
                newRotation = Quaternion.LookRotation(movement);
                rigidbody.MoveRotation(newRotation);
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
                    movement = movement.normalized * speed * Time.deltaTime*5;//숫자는 대쉬할 때 배속되는 속도
                    rigidbody.MovePosition(transform.position + movement);
                    dashTimer += Time.deltaTime;
                }
            }
        }
        else //기본 움직임
        {
            //플레이어의 움직임 부분
            movement.Set(h, 0f, v);//가로값 세로값 방향 셋
            movement = movement.normalized * speed * Time.deltaTime;
            rigidbody.MovePosition(transform.position + movement);


            //플레이어의 회전 부분
            if (h == 0 && v == 0)
                return;
            newRotation = Quaternion.LookRotation(movement);
            rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, newRotation, rotateSpeed * Time.deltaTime);

        }
    }

}
