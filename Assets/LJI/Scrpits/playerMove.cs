using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    float speed = 10f;
    float rotateSpeed = 10f;

    Rigidbody rigidbody;
    Vector3 movement; //��ü�� xyz�� ���� ����
    Quaternion newRotation;
    float h, v; //horizontal, vertical

    bool isDash=false;
    float dashTimer = 0f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();//��ü�� rigidbody �޾ƿ�
    }
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDash = true;
        }
        if (Input.GetMouseButtonDown(0))//���콺 ����:0 ���콺 ������:1
        {

        }
        if (Input.GetMouseButtonDown(1))
        {

        }

    }
    void FixedUpdate() //rigidBody �ִ� ��ü ������ �� �������
    {
        if (isDash)//�뽬 ������
        {
            if (dashTimer == 0)//�뽬 ������ ��. �̶� ������ ������������� �� ����
            {
                if (h == 0 && v == 0)
                {
                    movement.Set(0, 0f, 1);//�ƹ� Ű �ȴ����� �뽬 ������ ���� �̵�
                }
                else
                {
                    movement.Set(h, 0f, v);
                }

                //�뽬 ���� �ٶ󺸱�
                newRotation = Quaternion.LookRotation(movement);
                rigidbody.MoveRotation(newRotation);
                dashTimer += Time.deltaTime;
            }
            else
            {
                if (dashTimer > 0.15f)//dash �ð� �����ϱ�
                {
                    isDash = false;
                    dashTimer = 0f;
                }
                else //�뽬 ��
                {
                    movement = movement.normalized * speed * Time.deltaTime*5;//���ڴ� �뽬�� �� ��ӵǴ� �ӵ�
                    rigidbody.MovePosition(transform.position + movement);
                    dashTimer += Time.deltaTime;
                }
            }
        }
        else //�⺻ ������
        {
            //�÷��̾��� ������ �κ�
            movement.Set(h, 0f, v);//���ΰ� ���ΰ� ���� ��
            movement = movement.normalized * speed * Time.deltaTime;
            rigidbody.MovePosition(transform.position + movement);


            //�÷��̾��� ȸ�� �κ�
            if (h == 0 && v == 0)
                return;
            newRotation = Quaternion.LookRotation(movement);
            rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, newRotation, rotateSpeed * Time.deltaTime);

        }
    }

}
