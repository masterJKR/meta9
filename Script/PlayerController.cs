using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 5f;
    private SpriteRenderer sprite;

    private float jumpVel = 7f; // 점프 속도 
    private float groundCheck = 0.15f;  // 바닥 판정 반경  
    private LayerMask groundLyer; //  바닥 레이어 마스크 - 어떤 레이어만 충돌로 간주할지 
    private Transform groundForm; // 바닥 판정용 

    //점프 높이 조절( 단타, 장타)
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
        float x = Input.GetAxisRaw("Horizontal");  // 수평 조작  ( 왼쪽 : -1.0 ~    ,  오른쪽  : +1.0)
                                                   //float y = Input.GetAxisRaw("Vertical"); // 수직 조작

        if (Input.GetButtonDown("Jump") && isGrounded )  // 스페이스키를 누르면 점프 / 단, 바닥에 있는경우만
        {
            jump();
        }

        if (isJump && Input.GetButton("Jump") && Time.time - jumpTime < jumpDuration) // 길게 점프
        {
           rb.AddForce( Vector3.up * jumpVel * Time.deltaTime, ForceMode2D.Impulse );
            // AddForce  중력에 힘을 전달 하기 위한 메서드 
            // 두개의 인수를 넣어 주는데  첫번째 인수는 Vector3 ,  두번째 인수는 Mode 
            // Vector3으로  힘의 방향 과 크기를 전달하고  
            // Mode로  힘을 주는 모드를 지정
            // ForceMode에는  force, impulse, accelertion, VelocityChange
            // impulse는 리지드바디에 힘크기 * 힘을 가하는 시간 준다.
            // force는 짧은 시간에 발생하는 운동량의 변화
        }

        //캐릭터 방향 전환  좌우반전
        reverse(x);

        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y ); // 바닥위에서 좌우로 우직여야 하니까
        //현재 캐릭터의 y축 유지

       isGrounded =  Physics2D.OverlapCircle(groundForm.position, groundCheck, groundLyer);
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true; // 캐릭터가 바닥에 있는경우  점프가능
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;  // 캐릭터가 바닥에 없는경우 저프 불가
    }

    private void jump()  //점프하기 메서드
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpVel);
        jumpTime = Time.time;
        isJump = true;
    }


    private void reverse(float x)
    {
        if (x < 0f) // 왼쪽 이동
        {
            sprite.flipX = true;
        }
        else if (x > 0f)  // 오른쪽 이동
        {
            sprite.flipX = false;
        }
    }
}
