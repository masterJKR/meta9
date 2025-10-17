using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor = 0.5f;  //  0: 카메라 , 1: 정지배경 , 0.2 ~ 0.8
    private Transform cam;
    private Vector3 prevCamPos;


    void Start()
    {
        cam = Camera.main.transform;
        prevCamPos = cam.position;
    }

    private void LateUpdate()
    {
        Vector3 delta = cam.position - prevCamPos;
        // 카메라의 속도에 따라  배경이 다르게 이동
        transform.position += new Vector3(delta.x * parallaxFactor, delta.y * parallaxFactor, 0f);
        prevCamPos = cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
