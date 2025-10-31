using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public enum ActionType { NONE, ATTACK, MAGIC, DEF, AVOID }
    public enum Turn {  PlayerAttack, EnemyAttack }
    SPUM_Prefabs spum, enSpum;
    // �÷��̾� 
    [Header("Player UI")]
    [SerializeField] Button attackBtn; // ��������
    [SerializeField] Button magicBtn; //��������
    [SerializeField] Button defenseBtn; //���
    [SerializeField] Button avoidBtn; // ȸ��

    [SerializeField] Image imgAttack;
    [SerializeField] Image imgMagic;
    [SerializeField] Image imgDef;
    [SerializeField] Image imgAvoid;

    // ��
    [Header("Enemy UI")]
    [SerializeField] Image imgAttacken;
    [SerializeField] Image imgMagicen;
    [SerializeField] Image imgDefen;
    [SerializeField] Image imgAvoiden;


    [Header("HP")]
    [SerializeField] Slider playerHpBar;
    [SerializeField] Slider enemyHpBar;

    [SerializeField] Text countDownText;


    float chooseTime = 5f; // ���ýð�5��

    //�ӽ÷� �ɷ�ġ
    int PhyPower = 10 , magicPower= 10;
    int enPhyPower = 10, enMagicPower=10;

    int counterDamage = 5;

    int playerHp, enemyHp;

    bool playerLocked; // �÷��̾ �����ߴ°�
    bool roundRunning;

    ActionType playerChoice = ActionType.NONE;
    ActionType enemyChoice = ActionType.NONE;
    Turn currentTurn = Turn.PlayerAttack;


    private void Awake()
    {
        playerHp = GameManager.playerMaxHp;  // �÷��̾��� �ִ� hp
        enemyHp = 50; // ���� �⺻ hp
        spum = GameObject.FindGameObjectWithTag("Player").GetComponent<SPUM_Prefabs>();
        
        // �����̵忡 ����
        playerHpBar.maxValue = playerHp; // �����̵� �ִ밪 ����
        playerHpBar.value= playerHp; // ó�� ���۽� �����̵�� �� ä���
        enemyHpBar.maxValue = enemyHp;
        enemyHpBar.value= enemyHp;

        //��ư �ݹ�
        attackBtn.onClick.AddListener( () => PlayerChoose(ActionType.ATTACK));
        magicBtn.onClick.AddListener(() => PlayerChoose(ActionType.MAGIC));
        defenseBtn.onClick.AddListener(() => PlayerChoose(ActionType.DEF));
        avoidBtn.onClick.AddListener(() => PlayerChoose(ActionType.AVOID));
    }

    void Start()
    {
        enSpum = EnemyBoot.enemy.GetComponent<SPUM_Prefabs>();
        enSpum.OverrideControllerInit();
    }

    
    void Update()
    {
        
    }

    void PlayerChoose(ActionType type) // �÷��̾� ĳ���Ͱ� �ڽ��� �ϻ�Ȳ�� �����ϱ�
    {
        if (!roundRunning) return; // �̹� ��ư�� Ŭ���Ѱ�� - ���� ��� �ð� ����

        if( currentTurn == Turn.PlayerAttack)//���� �÷��̾��� �Ͽ� ���絿��
        {
            if (type != ActionType.ATTACK && type != ActionType.MAGIC) return;
        }
        else
        {
            if(type != ActionType.DEF && type != ActionType.AVOID) return;
        }
        Debug.Log(type);
        playerChoice = type;

    }

    private void OnEnable()
    {
        StartCoroutine(battleLoop());
    }

    //  ���� �޼���, ���� <-> ��� ���� ����
    IEnumerator battleLoop()
    {
        while( playerHp >0 && enemyHp > 0)
        {
            // ���� �غ�
            ResetAllParentColors(); // ��ư�� ������ �ʱ���·� ������
            playerChoice = ActionType.NONE;
            enemyChoice= ActionType.NONE;
            roundRunning = true;

            // �Ͽ����� ��ư ��Ȱ��ȭ/Ȱ��ȭ
            if( currentTurn == Turn.PlayerAttack)// �÷��̾��� ���� ��
            {
                EnableButton(att: true, mag: true, def: false, av: false);
                enemyChoice = (Random.value <  0.5f) ? ActionType.DEF : ActionType.AVOID;
            }
            else  // �� ���� ��
            {
                EnableButton(att: false, mag: false, def: true, av: true);
                enemyChoice = (Random.value < 0.5f) ? ActionType.ATTACK : ActionType.MAGIC;
            }
            Debug.Log(playerChoice);
            // ���� ��� �ð� 5��
            float t = 0f; 
            while( t< chooseTime)  // ���� ��� �ð��� 5�ʺ��� �۴ٸ� ��� ����ϱ����� 
                                    // �ݺ���
            {
                t += Time.deltaTime; // deltaTime �� �̿��Ͽ� �ð�����
                float remain = Mathf.Max(0f, chooseTime - t);
                countDownText.text = remain.ToString("F1"); // F1 �Ҽ��� ���ڸ�
                yield return null; //�ݺ����� ����������  ��� ��� �ϱ� 
            }
            Debug.Log("�ð� ����");
            // ���� �ð� ���� 
            //  ���� �� ���� �Ǵ� ��� ��  ��ư ���� ǥ��

            countDownText.text = string.Empty;

            // ���ýð����� �÷��̾ �̼����Ѱ��
            if( playerChoice == ActionType.NONE)
            {
                playerChoice = (currentTurn == Turn.PlayerAttack ) ? ActionType.ATTACK : ActionType.DEF;
            }
            LockSelect(); // ���õ� ��ư ����

            // ���� ������ 
            if (currentTurn == Turn.PlayerAttack)
                DamageResolve(true, playerChoice, enemyChoice);
            else
                DamageResolve(false, playerChoice, enemyChoice);
            // ���ؿ� ���� hp�� ǥ��
            playerHpBar.value = playerHp;
            enemyHpBar.value = enemyHp;

            //���� ����  hp 0 ��Ȳ
            if (playerHp <= 0 || enemyHp <= 0) break;

            // �ϱ���
            currentTurn = (currentTurn == Turn.PlayerAttack) ? Turn.EnemyAttack : Turn.PlayerAttack;
            roundRunning = false;


            yield return new WaitForSeconds(1.5f);// ���� �� �������� 1.5�� ���
        }

        if( playerHp <= 0) // �÷��̾��� �й� - ���ӿ��� ������ �̵�
        {
            SceneManager.LoadScene("GameOver");
        }
        else // �÷��̾��� �¸� ��������� �̵�
        {
            SceneManager.LoadScene("WorldMap");
        }


    }


    void AnimationActive(SPUM_Prefabs target, PlayerState playerState) {
        //===================================================

        var animList = target.StateAnimationPairs[playerState.ToString()];
        target.PlayAnimation(playerState, 0);

        //================================================
    }
    void DamageResolve(bool isPlayerAttack , ActionType pa, ActionType ea)
    {
        if (isPlayerAttack) // �÷��̾� ���� ���̳�
        {
            bool isMagic = (pa == ActionType.MAGIC);
            ActionType counterType = isMagic ? ActionType.AVOID : ActionType.DEF;
            int attackPower = isMagic ? magicPower : PhyPower;

            // ���� ����
            if (ea == counterType)
            {
                playerHp -= counterDamage;
                AnimationActive(spum, PlayerState.DAMAGED);
                AnimationActive(enSpum, PlayerState.ATTACK);
            }
            else
            {
                enemyHp -= attackPower;
                AnimationActive(spum, PlayerState.ATTACK);
                AnimationActive(enSpum, PlayerState.DAMAGED);
            }
        }
        else  // �÷��̾ ������ΰ��
        {
            bool isMagic = (ea == ActionType.MAGIC);
            ActionType counterType = isMagic ? ActionType.AVOID : ActionType.DEF;
            int attackPower = isMagic ? enMagicPower : enPhyPower;

            // ���� ����
            if (pa == counterType)
            {
                enemyHp -= counterDamage;
                AnimationActive(spum, PlayerState.ATTACK);
                AnimationActive(enSpum, PlayerState.DAMAGED);
            }
            else
            {
                playerHp -= attackPower;
                AnimationActive(spum, PlayerState.DAMAGED);
                AnimationActive(enSpum, PlayerState.ATTACK);
            }
        }
    }



    void LockSelect()
    {
        // ��ư�� �ִ°��
        // attackBtn.GetComponent<Image>().color = 
        //    enemyChoice == ActionType.ATTACK ? Color.black : Color.white;

        // �÷��̾� �� ������ ��ư ��������
        imgAttack.color = playerChoice == ActionType.ATTACK ? Color.black : Color.white;
        imgMagic.color = playerChoice == ActionType.MAGIC ? Color.black : Color.white;
        imgDef.color = playerChoice == ActionType.DEF ? Color.black : Color.white;
        imgAvoid.color = playerChoice == ActionType.AVOID ? Color.black : Color.white;
        
        // �� ���� ��������
        imgAttacken.color = enemyChoice == ActionType.ATTACK ? Color.black : Color.white;
        imgMagicen.color = enemyChoice == ActionType.MAGIC ? Color.black : Color.white;
        imgDefen.color = enemyChoice == ActionType.DEF ? Color.black : Color.white;
        imgAvoiden.color = enemyChoice == ActionType.AVOID ? Color.black : Color.white;


        // �÷��̾� ��ư ���� ���
        EnableButton(false, false, false, false);
    }


    void EnableButton(bool att, bool mag, bool def, bool av)  // �÷��̾� ��ư Ȱ�� ��Ȱ��
    {
        attackBtn.interactable = att;
        magicBtn.interactable = mag;
        defenseBtn.interactable = def;
        avoidBtn.interactable = av;
    }

    void ResetAllParentColors() // ��ư ���� ���� ���·� ������
    {
        // ��ư�� �ִ°��
        //attackBtn.GetComponent<Image>().color = Color.white;

        //��ư�� ������� �̹�����ü �ִ°��
        imgAttack.color = Color.white;
        imgMagic.color = Color.white;
        imgDef.color = Color.white;
        imgAvoid.color = Color.white;

        imgAttacken.color = Color.white;
        imgMagicen.color = Color.white;
        imgDefen.color = Color.white;
        imgAvoiden.color = Color.white;
    }

}
/*
    5���� �ð�����  ���� �Ǵ� �� �����ؾ� �Ѵ�. 
    ���õ� ��ɿ� ȭ�� ǥ�� ,
    ����

    ����  �����ϰ� �����Ѵ�.  
    
    ���������  ���������� ������ �ִ°� ����̰�  , ���������� ������ �ִ°� ȸ��
    ���� ���� �������ݼ����ϰ� ���� �� �����ߴٸ�  ���ݽ��з�  �г�Ƽ hp -5 ����
        ���� ������ ���ݷ� ��ŭ hp ����
 */