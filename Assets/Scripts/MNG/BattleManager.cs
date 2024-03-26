using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� ���¸� �����ϴ� ������
public enum BattleState { START, ACTION, PLAYERTURN, PROCESS, ENEMYTURN, RESULT, END }


public class BattleManager : MonoBehaviour
{
    // �÷��̾�� ���� ������
    public GameObject[] g_PlayerUnits;
    public GameObject g_EnemyUnit;

    // �÷��̾�� ���� �����ϴ� ��ġ
    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    public Transform waitStation;

    private Coroutine BattleCoroutine;
    private bool isPlayed = false;
    private GameManager.Action m_ePlayerAction;
    private int m_iPlayerActionIndex;

    // ���� �÷��̾�� ���� ������ ��ũ��Ʈ
    UnitEntity playerUnit;
    UnitEntity enemyUnit;

    // ���� �� �߻��ϴ� ��ȭ�� ǥ���ϴ� UI �ؽ�Ʈ
    public Text dialogueText;

    // �÷��̾�� ���� HUD(Head-Up Display)�� �����ϴ� ��ü
    public BattleHUDCTR playerHUD;
    public BattleHUDCTR enemyHUD;

    // ���� ����
    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        BattleInit();
    }

    private void Update()
    {
        //Debug.Log(state);
    }


    #region ���� ���� �޼���
    void BattleInit()
    {
        //�÷��̾� ���� �ʱ�ȭ
        //g_PlayerUnits = GameManager.Instance.m_UnitManager.g_PlayerUnits;
        //�� ���� �ʱ�ȭ
        g_EnemyUnit = GameManager.Instance.m_UnitManager.SetUnitEntityByName("��������");

        
        state = BattleState.START;
        // ���� ���� ���·� �ʱ�ȭ�ϰ�, ������ �����ϴ� �ڷ�ƾ ����
        BattleCoroutine = StartCoroutine(SetupBattle());
    }





    // �÷��̾� �� ���� ó��   ----------------------------------------------------------------------------------------------------------------------
    private void PlayerAction()
    {
        state = BattleState.ACTION;
        dialogueText.text = playerUnit.m_sUnitName + "�� ��� �� �� �ΰ�..";
    }
    #region �÷��̾� Action ó��
    private void Process()
    {
        if (m_ePlayerAction == GameManager.Action.ATTACK)
            AttackProcess();
        else if (m_ePlayerAction == GameManager.Action.ITEM)
            ItemProcess();
        else if (m_ePlayerAction == GameManager.Action.CHANGE)
            ChangeProcess();
        else if (m_ePlayerAction == GameManager.Action.RUN)
            RunProcess();
    }
    private void AttackProcess()
    {
        if (state != BattleState.ENEMYTURN && state != BattleState.PLAYERTURN)
        {
            if (playerUnit.m_iUnitSpeed > enemyUnit.m_iUnitSpeed)
                BattleCoroutine = StartCoroutine(PlayerTurn_Attack());
            else if (playerUnit.m_iUnitSpeed < enemyUnit.m_iUnitSpeed)
                BattleCoroutine = StartCoroutine(EnemyTurn());
            else
            {
                if (playerUnit.m_iUnitLevel < enemyUnit.m_iUnitLevel)
                    StartCoroutine(EnemyTurn());
                else
                    StartCoroutine(PlayerTurn_Attack());
            }
        }
        else if (state == BattleState.ENEMYTURN)
            StartCoroutine(PlayerTurn_Attack());
        else if (state == BattleState.PLAYERTURN)
            StartCoroutine(EnemyTurn());
    }
    private void ItemProcess()
    {
        if (state != BattleState.PLAYERTURN)
            StartCoroutine(PlayerTurn_Item());
        else
            StartCoroutine(EnemyTurn());
    }
    private void ChangeProcess()
    {
        if (state != BattleState.PLAYERTURN)
            StartCoroutine(PlayerTurn_Change());
        else
            StartCoroutine(EnemyTurn());
    }
    private void RunProcess()
    {
        if (state != BattleState.PLAYERTURN)
            StartCoroutine(PlayerTurn_Item());
        else
            StartCoroutine(EnemyTurn());
    }

    #endregion


    // ���� ���� ó��
    void AfterWin()
    {
        state = BattleState.END;
        dialogueText.text = "�¸�!";
    }

    void AfterLost()
    {
        state = BattleState.END;
        dialogueText.text = ".......����� ������ ����������.";
    }

    #endregion

    #region ���� ���� �ڷ�ƾ
    // ���� ������ ó���ϴ� �ڷ�ƾ
    IEnumerator SetupBattle()
    {
        // �÷��̾�� ���� ������ �����ϰ� ��ġ
        playerUnit = GameManager.Instance.m_UnitManager.g_PlayerUnits[0].transform.GetComponent<UnitEntity>();
        for(int i = 1; i< GameManager.Instance.m_UnitManager.g_PlayerUnits.Length;i++)
            GameManager.Instance.m_UnitManager.g_PlayerUnits[i].transform.position = waitStation.position;
        

        enemyUnit = g_EnemyUnit.GetComponent<UnitEntity>();
        enemyUnit.transform.position = enemyBattleStation.position;
        playerUnit.transform.position = playerBattleStation.position;

        // ��ȭ �ؽ�Ʈ�� ���� �̸��� ǥ��
        dialogueText.text = "�߻��� " + enemyUnit.m_sUnitName + " ��(��) ��Ÿ����...";

        // �÷��̾�� ���� HUD�� ������Ʈ
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        // ���� ���� �� ��� ���
        yield return new WaitForSeconds(2f);

        // �÷��̾� ������ ���� ��ȯ
        PlayerAction();
    }
    #region �÷��̾� Action ó��
    IEnumerator PlayerTurn_Attack()
    {
        state = BattleState.PLAYERTURN;
        //���� ����
        playerUnit.AttackByIndex(playerUnit, enemyUnit, m_iPlayerActionIndex);
        enemyHUD.SetHP(enemyUnit.m_iCurrentHP);
        dialogueText.text = playerUnit.m_sUnitName + "�� ����!!";
        yield return new WaitForSeconds(1f);
        if (enemyUnit.m_iCurrentHP <= 0 || playerUnit.m_iCurrentHP <= 0)
            BattleCoroutine = StartCoroutine(Result());
        else if (!isPlayed)
            Process();
        else
            BattleCoroutine = StartCoroutine(Result());
        isPlayed = true;
    }
    IEnumerator PlayerTurn_Item()
    {
        state = BattleState.PLAYERTURN;

        playerUnit.Heal(5);

        // �÷��̾��� ü���� HUD�� ������Ʈ�ϰ� ��ȭ �ؽ�Ʈ ǥ��
        playerHUD.SetHP(playerUnit.m_iCurrentHP);
        dialogueText.text = "ü���� 5 ȸ���ߴ�!";

        yield return new WaitForSeconds(2f);

        Process();
        isPlayed = true;
    }
    IEnumerator PlayerTurn_Change()
    {
        state = BattleState.PLAYERTURN;

        // �÷��̾� ��ü
        GameObject newPlayerGO = GameManager.Instance.m_UnitManager.g_PlayerUnits[m_iPlayerActionIndex];
        playerUnit.transform.position = waitStation.position; // ���� �÷��̾� �̵�
        newPlayerGO.transform.position = playerBattleStation.position;
        playerUnit = newPlayerGO.GetComponent<UnitEntity>();


        // �÷��̾��� ü���� HUD�� ������Ʈ
        playerHUD.SetHUD(playerUnit);
        yield return new WaitForSeconds(2f);

        Process();
        isPlayed = true;
    }
    #endregion

    // ���� ���� ó���ϴ� �ڷ�ƾ
    IEnumerator EnemyTurn()
    {
        state = BattleState.ENEMYTURN;
        // ���� �����ϰ� ��ȭ �ؽ�Ʈ ������Ʈ
        int randomAttackIndex = Random.Range(0, 2);
        string AttackName = enemyUnit.AttackByIndex(enemyUnit, playerUnit,randomAttackIndex);
        playerHUD.SetHP(playerUnit.m_iCurrentHP);
        dialogueText.text = enemyUnit.m_sUnitName + " �� " + AttackName + "����!";


        // �÷��̾ �������� �ް� ü�� ������Ʈ

        yield return new WaitForSeconds(1f);
        if (enemyUnit.m_iCurrentHP <= 0 || playerUnit.m_iCurrentHP <= 0)
            BattleCoroutine = StartCoroutine(Result());
        else if (!isPlayed)
            Process();
        else
            BattleCoroutine = StartCoroutine(Result());
        isPlayed = true;

    }
    // �÷��̾� ȸ���� ó���ϴ� �ڷ�ƾ


    IEnumerator Result()
    {
        dialogueText.text = "�� ���� �Ϸ�..";
        yield return new WaitForSeconds(1f);
        if (playerUnit.m_iCurrentHP <= 0)
            AfterLost();
        else if (enemyUnit.m_iCurrentHP <= 0)
            AfterWin();
        else
        {
            state = BattleState.ACTION;
            isPlayed = false;
            PlayerAction();
        }

    }

    #endregion

    #region ��ư Ŭ�� �̺�Ʈ
    // ���� ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnButton(GameManager.Action action, int index)
    {
        // �÷��̾� ���� �ƴ� ��쿡�� �ƹ� �۾��� �������� ����
        if (state != BattleState.ACTION)
            return;
        m_ePlayerAction = action;
        m_iPlayerActionIndex = index;


        state = BattleState.PROCESS;
        Process();
    }
    #endregion
}
