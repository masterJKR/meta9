using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowCarmera : MonoBehaviour
{
    public Transform target; // 카메라가 따라갈 대상 ( 캐릭터 )
    public float smooth = 0.15f; // 카메라 이동 속도
                                 //
    private Vector3 velocity = Vector3.zero;
    private float fixedZ; 


    void Start()
    {
        fixedZ = transform.position.z; // position z 값 고정 ( -10 )
    }

    private void LateUpdate() // 모든 Update 메서드가 끝난후 호출, 카메라 추적하기에 적합
                              // ( 캐릭터 이동이 반영 된 뒤 에 따라옴)
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
