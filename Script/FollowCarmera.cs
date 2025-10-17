using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowCarmera : MonoBehaviour
{
    public Transform target; // ī�޶� ���� ��� ( ĳ���� )
    public float smooth = 0.15f; // ī�޶� �̵� �ӵ�
                                 //
    private Vector3 velocity = Vector3.zero;
    private float fixedZ; 


    void Start()
    {
        fixedZ = transform.position.z; // position z �� ���� ( -10 )
    }

    private void LateUpdate() // ��� Update �޼��尡 ������ ȣ��, ī�޶� �����ϱ⿡ ����
                              // ( ĳ���� �̵��� �ݿ� �� �� �� �����)
    {
        if (target == null) return;

        Vector3 targetPos = new Vector3(target.position.x, target.position.y,  fixedZ);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smooth);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
