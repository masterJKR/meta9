using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBoot : MonoBehaviour
{
    [SerializeField] Transform enemyAnchor; // ���� ��Ÿ�� ��ġ
    [SerializeField] GameObject enemyPrefab;

    EnemySnapshot enemyshot;
    public static GameObject enemy;
    private void Awake()
    {
        enemyshot = GameManager.Instance.BattleContext.enemy; // ����ʿ��� ������ ��
        if (enemyshot == null) // �� �������� �ʰ� ��Ʋ�ʿ� �Դٸ�
        {
            SceneManager.LoadScene("WorldMap");
            return;
        }

        enemyPrefab = enemyshot.prefab;
        enemy = Instantiate(enemyPrefab, enemyAnchor.position, Quaternion.identity);
    }
    void Start()
    {
        


    }

    
    void Update()
    {
        
    }
}
