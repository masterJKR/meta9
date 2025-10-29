using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �̵� ����
    Rigidbody2D rb;
    public float speed = 3.0f; // �̵� �ӵ�
    public string upAni = "PlayerUp";
    public string downAni = "PlayerDown";
    public string leftAni = "PlayerLeft";
    public string rightAni = "PlayerRight";
    public string deadAni = "PlayerDead";

    string nowAni = ""; // ���� �ִϸ��̼�
    string oldAni = ""; //���� �ִϸ��̼�

    float axisH , axisV;
    public float angleZ = -90.0f; // ȸ����

    bool isMoving = false; // �̵� ����

    public static int hp = 3; // ĳ���� �����
    public static string gameState; // ���� ���� - ������(playing),���ӿ���(gameOver)
    bool inDamage = false; // ������ �ް� �ֳ�


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        oldAni = downAni;
        gameState = "playing";
        hp = PlayerPrefs.GetInt("PlayerHP");
    }

    
    void Update()
    {
        if (gameState != "playing" || inDamage) return; // ���ӻ��°� �÷��� ���� �ƴϰų�
                    //  ������ �԰� �ִ����̸� �̵� ����
        if(isMoving == false)
        {
            axisH = Input.GetAxisRaw("Horizontal");  // �¿� �̵�Ű
            axisV = Input.GetAxisRaw("Vertical"); // ���� �̵�Ű
        }

        // Ű�Է����� �̵� ������ ���ϰ� ������ ���� �ִϸ��̼� ����
        Vector2 fromPt = transform.position; 
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);

        angleZ = GetAngle(fromPt, toPt);

        if (angleZ >= -45 && angleZ < 45)
        {
            nowAni = rightAni; // ������ ����
        }
        else if (angleZ >= 45 && angleZ <= 135)
            nowAni = upAni; // ���� ����
        else if (angleZ > 135 && angleZ < 225)
            nowAni = leftAni;  // ���� ����
        else
            nowAni = downAni; // �Ʒ� �� ����
        
        // ���� �� �ִϸ��̼� ����
        if( nowAni != oldAni)
        {
            oldAni = nowAni;
            GetComponent<Animator>().Play(nowAni);
        }
    }

    private void FixedUpdate()
    {
        if (gameState != "playing") return; // �������� �ƴϸ� ���� �ȵǰ�
        if(inDamage)
        {
            // ������ �޴� ���̸� ���� ���� �̰� �����
            float val = Mathf.Sin(Time.time * 50);
            if (val > 0)
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            return; // ������ �ް� �������� �̵� �ȵǰ�
        }

        rb.velocity = new Vector2(axisH, axisV) * speed; // �̵�
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if ( collision.gameObject.tag == "Enemy") // ���Ϳ� ���� - ���� ����
        {
            GetDamage(collision.gameObject);
        }
    }

    void GetDamage(GameObject enemy) // ���� �޾�����  hp ���� 
    {
        if( gameState == "playing")
        {
            hp--;
            PlayerPrefs.SetInt("PlayerHP",hp);
        }
    }

    float GetAngle(Vector2 fromPt, Vector2 toPt) // ���� ���ϱ�
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
