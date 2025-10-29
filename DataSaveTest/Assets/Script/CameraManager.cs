using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject otherTarget;

    void Start()
    {
        
    }

    void Update()
    {
        //  ī�޶� �����ϱ� - ������ �������  ī�޶���ġ ������ ���Ͱ� �Ⱥ��δ�. 
        //  �������� �Ÿ��� �̿��ϰų�  �������� ���� �����ȿ� ���� �����ϵ��� �Ѵ�.


        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (otherTarget != null) // ������ �ִ� ���̶��
        {
            float dist = Vector2.Distance(otherTarget.transform.position,
                player.transform.position);  // �������� �Ÿ� ���ϱ�
            if ( dist < 8.0f)
            {
                // Camera.main.orthographicSize = 8; // ī�޶� �Ÿ� �ø���
                //ī�޶� �Ÿ� ������ �ε巴�� ó�� �Ϸ���
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize,
                    8.0f, Time.deltaTime * 2f);
            }
        }
        
            //  ���� ���Ͱ� ���� �ʿ��� ī�޶� ����
            transform.position = new Vector3(player.transform.position.x,
                player.transform.position.y, -10);
        
    }
}
