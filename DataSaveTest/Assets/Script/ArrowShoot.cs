using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    public float shootSpeed = 12.0f; // ȭ�� �ӵ�
    public float shootDelay = 0.25f;// �߻� ����

    public GameObject bowFab;  // Ȱ ������
    public GameObject arrowFab; // ȭ�� ������

    bool isAttack = false; // ���� �� �̳� �ƴϳ�
    GameObject bowObj;  //  Ȱ ��ü


    void Start()
    {
        Vector3 pos = transform.position; // ĳ���� ��ǥ�� Ȱ�� ��������� ĳ���Ͱ� ������������
        bowObj = Instantiate(bowFab, pos, Quaternion.identity); // Ȱ ��ü ����
        bowObj.transform.SetParent(transform);// Ȱ��ü��  ĳ������ �ڽ����� 
    }

    
    void Update()
    {
        if((Input.GetButtonDown("Fire1"))) // ���� Ű ������.
                                           // - Fire1(ctrl), Fire2(alt), Fire3(shift)
        {
            Attack();
        }

        float bowZ = -1; // Ȱ�� z��  ĳ���ͺ��� ������
        PlayerController pc = GetComponent<PlayerController>();
        if (pc.angleZ > 30 & pc.angleZ < 150)
            bowZ = 1;  // ���� ���� �̵� �ϴ°��
        
        //  Ȱ ȸ��
        bowObj.transform.rotation = Quaternion.Euler(0, 0, pc.angleZ);

        // Ȱ�� ȭ�� ǥ�� �켱����  z��
        bowObj.transform.position = new Vector3(transform.position.x, 
                        transform.position.y, bowZ);
    }

    public void Attack()
    {
        //ȭ���� ������ 0 �� �ƴѰ��� �̹� ȭ���� �߻�� ��찡 �ƴ϶��
        //  ���ݰ���
        if( ItemKeeper.hasArrows > 0 &&  !isAttack )
        {
            ItemKeeper.hasArrows--; // ȭ�� ���� -1
            isAttack = true; //������

            PlayerController pc = GetComponent<PlayerController>();
            float angleZ = pc.angleZ; // ĳ���Ͱ� �ٶ󺸰� �ִ� ���� (����)

            Quaternion r = Quaternion.Euler(0, 0, angleZ);
            GameObject arrowObj = Instantiate(arrowFab, transform.position, r);

            // ȭ���� �߻�� ��ǥ
            float x = Mathf.Cos(angleZ * Mathf.Deg2Rad);
            float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);
            Vector3 v = new Vector3(x, y) * shootSpeed;

            Rigidbody2D rb = arrowObj.GetComponent<Rigidbody2D>();
            rb.AddForce(v, ForceMode2D.Impulse);

            Invoke("StopAttack", shootDelay); //ȭ�� ��߻� �ð� �ڿ� �������� �ƴ����� ����
        }
    }
    public void StopAttack() { isAttack = false; } // �������� �ƴϴ�.


}
