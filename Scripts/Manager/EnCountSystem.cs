using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnCountSystem : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] Transform player;  // ĳ���� transform
    Rigidbody2D playerRb;  // ĳ���� �߷�������Ʈ - ������ üũ
    [SerializeField] MonoBehaviour playerControl; // ĳ���� ��Ʈ�ѷ� ��ũ��Ʈ

    [Header("Enemy Rule")]
    [SerializeField] float checkInterval = 1.0f; // 1�ʿ� �ѹ��� ���� ����
    [SerializeField] float encounterProb = 0.10f; // 10% Ȯ��
    [SerializeField] string battleMapName = "Battle";

    [Header("UI / Preview")]
    [SerializeField] LogUI log; // �α� ��� ��ũ��Ʈ
    [SerializeField] GameObject[] enemyPrefab; // ������ �� ��� �����ֱ� ��
    [SerializeField] Sprite[] enemySprite; // ������ �� ��������Ʈ
    [SerializeField] float previewDuration = 2.0f; // �������� 2�� ����
    [SerializeField] Vector3 previewOffset = new Vector3(0, 1.5f, 0);

    bool isMoving;
    bool isEncountering;
    float timer;
    SPUM_Prefabs spum;

    private void Awake()
    {
        isMoving = true;
        spum = player.GetComponent<SPUM_Prefabs>();
    }

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (!isMoving || isEncountering) return; //���� ���� �߰ų� ��������������

        // ������ ���� - ĳ������ �ӵ��� ����
        bool isRunning = playerRb && playerRb.velocity.sqrMagnitude > 0.01f;
        if (!isRunning) return; //�������� �ʰ� �ִ�.

        timer += Time.deltaTime;
        if (timer < checkInterval) return;
        timer = 0f;
        if( Random.value < encounterProb)
        {
            // 10% Ȯ���� ���� ���� ��
            StartCoroutine( DoEncounter() );
        }
    }

    IEnumerator DoEncounter() // ���� ������ ��� ���� �޼���
    {
        //ĳ���� ���� ���� ,  �α� ��� ,  �� �̸����� �ʿ� ���, ��Ʋ�� �̵�
        isEncountering = true;

        // ĳ���� ���� ��� ����
        if(playerControl) playerControl.enabled = false;
        if (playerRb) playerRb.velocity = Vector2.zero;

        // �α� ���
        if (log) log.Print("���� ������ : ���� �غ�...");

        // �� �̸����� ���
        // �� ������ Ȯ���� ����
        int[] enemyWeight = { 60, 25, 12, 3 };

        int index = GetWeightIndex(enemyWeight); // ����ġ �޼���
        Debug.Log("�ε��� : "+index);
        GameObject chosen = enemyPrefab[index]; // ����ġ�� ���� ���� �ε��� ����

        //��Ʋ ���� �Ѱ��� ������ ����
        EnemySnapshot snap = new EnemySnapshot();
        // ���� �̸�, hp�� ������ ���߿� �߰� ���⿡ 

        snap.prefab = chosen;
        Debug.Log("���õ� : " + snap);
        GameManager.Instance.BattleContext.enemy = snap; // ���õ� �������� �������� ����
        
        GameObject preview = Instantiate(chosen);

        var sr = preview.GetComponentInChildren<SpriteRenderer>();
        if( sr && enemySprite != null && enemySprite.Length >0)
        {
            sr.sprite = enemySprite[Random.Range(0, enemySprite.Length)];
        }
            // ĳ���� ��ó�� ��Ÿ����
        preview.transform.position = player.position + previewOffset;

        //===================================================

        var animList = spum.StateAnimationPairs[PlayerState.IDLE.ToString()];
        spum.PlayAnimation(PlayerState.IDLE, 0);

        //================================================
        // 2�� ��� -  2�� ���� �� �̸����� ����� ����
        yield return new WaitForSeconds(previewDuration);

        if (preview) Destroy(preview); // 2�ʵ� �̸����� ����

        SceneManager.LoadScene(battleMapName); // ���� ������ �̵�

    }


    private int GetWeightIndex(int[] enemyWeight)
    {
        int total = 0;
        for(int i =0; i< enemyWeight.Length; i++)
            total += enemyWeight[i];

        int roll = Random.Range(0, total);
        int acc = 0;
        for(int i =0;i< enemyWeight.Length; i++)
        {
            acc += enemyWeight[i];
            if (roll < acc) return i;
        }

        return enemyWeight.Length - 1;
    }


}
