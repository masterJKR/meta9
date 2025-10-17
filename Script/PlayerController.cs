using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 5f;
    private SpriteRenderer sprite;

    private float jumpVel = 7f; // ���� �ӵ� 
    private float groundCheck = 0.15f;  // �ٴ� ���� �ݰ�  
    private LayerMask groundLyer; //  �ٴ� ���̾� ����ũ - � ���̾ �浹�� �������� 
    private Transform groundForm; // �ٴ� ������ 

    //���� ���� ����( ��Ÿ, ��Ÿ)
    private float jumpDuration = 0.9f;
    private float jumpTime;
    private bool isJump=false;


    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();    
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");  // ���� ����  ( ���� : -1.0 ~    ,  ������  : +1.0)
                                                   //float y = Input.GetAxisRaw("Vertical"); // ���� ����

        if (Input.GetButtonDown("Jump") && isGrounded )  // �����̽�Ű�� ������ ���� / ��, �ٴڿ� �ִ°�츸
        {
            jump();
        }

        if (isJump && Input.GetButton("Jump") && Time.time - jumpTime < jumpDuration) // ��� ����
        {
           rb.AddForce( Vector3.up * jumpVel * Time.deltaTime, ForceMode2D.Impulse );
            // AddForce  �߷¿� ���� ���� �ϱ� ���� �޼��� 
            // �ΰ��� �μ��� �־� �ִµ�  ù��° �μ��� Vector3 ,  �ι�° �μ��� Mode 
            // Vector3����  ���� ���� �� ũ�⸦ �����ϰ�  
            // Mode��  ���� �ִ� ��带 ����
            // ForceMode����  force, impulse, accelertion, VelocityChange
            // impulse�� ������ٵ� ��ũ�� * ���� ���ϴ� �ð� �ش�.
            // force�� ª�� �ð��� �߻��ϴ� ����� ��ȭ
        }

        //ĳ���� ���� ��ȯ  �¿����
        reverse(x);

        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y ); // �ٴ������� �¿�� �������� �ϴϱ�
        //���� ĳ������ y�� ����

       isGrounded =  Physics2D.OverlapCircle(groundForm.position, groundCheck, groundLyer);
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true; // ĳ���Ͱ� �ٴڿ� �ִ°��  ��������
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;  // ĳ���Ͱ� �ٴڿ� ���°�� ���� �Ұ�
    }

    private void jump()  //�����ϱ� �޼���
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpVel);
        jumpTime = Time.time;
        isJump = true;
    }


    private void reverse(float x)
    {
        if (x < 0f) // ���� �̵�
        {
            sprite.flipX = true;
        }
        else if (x > 0f)  // ������ �̵�
        {
            sprite.flipX = false;
        }
    }
}
