using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int hp = 10; 
    public float actionDist = 7.0f; // 캐릭터 감지 거리

    public GameObject bulletFab;  // 총알 객체
    public float shootSpeed = 5.0f; // 총알 속도

    bool inAttack = false; // 공격중 인가?

    
    void Start()
    {
        
    }


    
    void Update()
    {
        if (hp <= 0) return;  // 보스의 hp가 있으면 아래 코드 실행

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 prpos = player.transform.position;

        // 보스와  캐릭터 거리
        float dist = Vector2.Distance(transform.position, prpos);
        if( dist <= actionDist && !inAttack)
        {
            inAttack = true; // 공격중이다.
            GetComponent<Animator>().Play("BossAttack");
        }
        else if( dist > actionDist && inAttack)
        {
            inAttack = false;
            GetComponent<Animator>().Play("BossIdle");
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow") // 충돌한 객체가 화살
        {
            hp--;
            if (hp <= 0)
            {
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<Animator>().Play("BossDead");
                Destroy(gameObject, 1f);
            }
        }
    }


    void Attack()
    {
        Transform tr = transform.Find("gate");
        GameObject gate = tr.gameObject;   // 보스몹 가슴 중앙의 gate 객체 - 총알 발사 위치

        GameObject player = GameObject.FindGameObjectWithTag("Player");

            float dx = player.transform.position.x - gate.transform.position.x;
            float dy = player.transform.position.y - gate.transform.position.y;
            float rad = Mathf.Atan2(dy, dx);
            float angle = rad * Mathf.Rad2Deg;

        Quaternion r = Quaternion.Euler(0, 0, angle);
        GameObject bullet = Instantiate(bulletFab, gate.transform.position, r);

        float x = Mathf.Cos(rad);
        float y = Mathf.Sin(rad);
        Vector3 v = new Vector3(x, y) * shootSpeed;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(v, ForceMode2D.Impulse);

    }
}
