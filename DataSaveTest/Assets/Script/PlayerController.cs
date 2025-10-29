using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 이동 관련
    Rigidbody2D rb;
    public float speed = 3.0f; // 이동 속도
    public string upAni = "PlayerUp";
    public string downAni = "PlayerDown";
    public string leftAni = "PlayerLeft";
    public string rightAni = "PlayerRight";
    public string deadAni = "PlayerDead";

    string nowAni = ""; // 현재 애니메이션
    string oldAni = ""; //이전 애니메이션

    float axisH , axisV;
    public float angleZ = -90.0f; // 회전각

    bool isMoving = false; // 이동 여부

    public static int hp = 3; // 캐릭터 생명력
    public static string gameState; // 게임 상태 - 게임중(playing),게임오버(gameOver)
    bool inDamage = false; // 데미지 받고 있냐


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        oldAni = downAni;
        gameState = "playing";
        hp = PlayerPrefs.GetInt("PlayerHP");
    }

    
    void Update()
    {
        if (gameState != "playing" || inDamage) return; // 게임상태가 플레이 중이 아니거나
                    //  데미지 입고 있는중이면 이동 못함
        if(isMoving == false)
        {
            axisH = Input.GetAxisRaw("Horizontal");  // 좌우 이동키
            axisV = Input.GetAxisRaw("Vertical"); // 상하 이동키
        }

        // 키입력으로 이동 각도를 구하고 각도에 따라 애니메이션 변경
        Vector2 fromPt = transform.position; 
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);

        angleZ = GetAngle(fromPt, toPt);

        if (angleZ >= -45 && angleZ < 45)
        {
            nowAni = rightAni; // 오른쪽 방향
        }
        else if (angleZ >= 45 && angleZ <= 135)
            nowAni = upAni; // 위쪽 방향
        else if (angleZ > 135 && angleZ < 225)
            nowAni = leftAni;  // 왼쪽 방향
        else
            nowAni = downAni; // 아래 쪽 방향
        
        // 변경 된 애니메이션 적용
        if( nowAni != oldAni)
        {
            oldAni = nowAni;
            GetComponent<Animator>().Play(nowAni);
        }
    }

    private void FixedUpdate()
    {
        if (gameState != "playing") return; // 게임중이 아니면 실행 안되게
        if(inDamage)
        {
            // 데미지 받는 중이면 깜빡 깜빡 이게 만들기
            float val = Mathf.Sin(Time.time * 50);
            if (val > 0)
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            return; // 데미지 받고 있을때난 이동 안되게
        }

        rb.velocity = new Vector2(axisH, axisV) * speed; // 이동
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if ( collision.gameObject.tag == "Enemy") // 몬스터와 접촉 - 공격 받음
        {
            GetDamage(collision.gameObject);
        }
    }

    void GetDamage(GameObject enemy) // 공격 받았으니  hp 감소 
    {
        if( gameState == "playing")
        {
            hp--;
            PlayerPrefs.SetInt("PlayerHP",hp);
        }
    }

    float GetAngle(Vector2 fromPt, Vector2 toPt) // 각도 구하기
    {
        float angle;
        if (axisH != 0 || axisV != 0)
        {
            float dx = toPt.x - fromPt.x;
            float dy = toPt.y - fromPt.y;
            float rad = Mathf.Atan2(dy, dx);

            angle = rad * Mathf.Rad2Deg;
        }
        else angle = angleZ;
        return angle;
    }
}
