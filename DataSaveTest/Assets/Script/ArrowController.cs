using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float deleteTime = 2;
    void Start()
    {
        Destroy(gameObject, deleteTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.SetParent(collision.transform);
        GetComponent<BoxCollider2D>().enabled = false; //�浹 ���� ��Ȱ��ȭ
        GetComponent<Rigidbody2D>().simulated = false; // ���� �ùķ��̼� ��Ȱ��ȭ
    }


    void Update()
    {
        
    }
}
