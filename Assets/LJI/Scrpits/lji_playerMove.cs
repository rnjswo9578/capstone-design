using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lji_playerMove : MonoBehaviour
{

    float speed = 10f;
    float rotateSpeed = 10f;

    Animator animator;
    Rigidbody rigidbody;
    Vector3 movement; //��ü�� xyz�� ���� ����
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
        if (Input.GetMouseButtonDown(0))//���콺 ����:0 ���콺 ������:1
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
            if (isDash)//�뽬 ������
            {
                Dash(controller);
            }
            else //�⺻ ������
            {
                //�÷��̾��� ������ �κ�
                movement = new Vector3(h, 0, v);//���ΰ� ���ΰ� ���� ��
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
            if (dashTimer > 0.15f)//dash �ð� �����ϱ�
            {
                isDash = false;
                dashTimer = 0f;
            }
            else //�뽬 ��
            {
                movement = movement.normalized;//���ڴ� �뽬�� �� ��ӵǴ� �ӵ�
                
                controller.Move(movement * speed * Time.deltaTime * 5);
                dashTimer += Time.deltaTime;
            }
        }
    }
}
