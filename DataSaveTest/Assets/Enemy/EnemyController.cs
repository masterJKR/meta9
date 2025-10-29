using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int hp = 3; // ���� �����

    // ���� �̵� ����
    float speed = 0.5f;
    public float actionDist = 4.0f; //  ĳ���� ���� �Ÿ�
    public string idleAni = "EnemyIdle";
    public string upAni = "EnemyUp";
    public string downAni = "EnemyDown";
    public string leftAni = "EnemyLeft";
    public string rightAni = "EnemyRight";
    public string deadAni = "EnemyDead";
    string nowAni = "" , oldAni="";

    float axisH, axisV;
    Rigidbody2D rb;

    bool isActive = false; // �÷��̾� �����ϸ� Ȱ��ȭ
    public int arrangeId = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(isActive) // �÷��̾� ���� �߳�?
        {
            float dx = player.transform.position.x - transform.position.x;
            float dy = player.transform.position.y - transform.position.y;
            float rad = Mathf.Atan2(dy, dx);
            float angle = rad * Mathf.Rad2Deg;

            if (angle > -45.0f && angle <= 45.0f) nowAni = rightAni;
            else if (angle > 45.0f && angle <= 135.0f) nowAni = upAni;
            else if (angle > 135.0f && angle < 225.0f) nowAni = leftAni;
            else nowAni = downAni;

            axisH = Mathf.Cos(rad) * speed;
            axisV = Mathf.Sin(rad) * speed;

        }
        else // ���� �÷��̾� ���� ���ߴ�.
        {
            float dist = Vector2.Distance(transform.position, player.transform.position);
            if( dist < actionDist ) isActive = true; // ���� �Ÿ��ȿ� ĳ���� �߰�
        }
    }

    private void FixedUpdate()
    {
        if ( isActive && hp > 0)
        {
            rb.velocity = new Vector2(axisH, axisV);
            if( nowAni != oldAni)
            {
                oldAni = nowAni;
                GetComponent<Animator>().Play(nowAni);
            }
        }
    }
    // ȭ��� �浹�ϸ�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            hp--;
            if(hp <= 0)
            {
                GetComponent<CircleCollider2D>().enabled = false;
                rb.velocity = new Vector2(0, 0);
                GetComponent<Animator>().Play(deadAni);
                Destroy(gameObject, 0.5f);

                SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
            }
        }
    }
}
