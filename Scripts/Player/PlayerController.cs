using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 3.0f; // �̵� �ӵ�

    float axisH, axisV;
    public static string gameState;

    //SPUM ������  �ִϸ��̼� �����
    SPUM_Prefabs spum;
    Vector2 lastMoveDir = Vector2.down;  // ������ �ٷκ� ���� ǥ��
    const float moveDeadZone = 0.05f; // �̼������� ����
    private PlayerState currentState = PlayerState.IDLE;

    float baseScaleX = 1f;
    float lastSign = -1f;
    Transform visual;

    private void Awake()
    {
        spum = GetComponent<SPUM_Prefabs>();
        spum.OverrideControllerInit();
        visual = spum._anim.transform;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameState = "playing";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != "playing") return; // ���ӻ��°� �÷��� ���� �ƴϰų�
                                                        //  ������ �԰� �ִ����̸� �̵� ����
      
         axisH = Input.GetAxisRaw("Horizontal");  // �¿� �̵�Ű
         axisV = Input.GetAxisRaw("Vertical"); // ���� �̵�Ű


        if (moveDeadZone < Mathf.Abs(axisH))
        {
            //�¿� �̵� ����
            lastSign = axisH > 0 ? -1f : 1f;
            var s = visual.localScale;
            s.x = baseScaleX * lastSign;
            visual.localScale = s;
        }


        Vector2 input;
        input.x= axisH;
        input.y= axisV;
        input.Normalize();

        if (input.magnitude > 0.01f)
            currentState = PlayerState.MOVE;
        else
            currentState = PlayerState.IDLE;

        if (Input.GetKeyDown(KeyCode.Space))
            currentState = PlayerState.ATTACK;

            var animList = spum.StateAnimationPairs[currentState.ToString()];
        int index = 0;
        spum.PlayAnimation(currentState, index);

    }

    private void FixedUpdate()
    {
        if (gameState != "playing") return; // �������� �ƴϸ� ���� �ȵǰ�
       

        rb.velocity = new Vector2(axisH, axisV) * speed; // �̵�
    }
}
