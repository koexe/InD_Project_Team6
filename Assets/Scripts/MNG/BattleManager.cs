using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BattleManager : MonoBehaviour
{
    // 플레이어와 적의 프리팹
    public GameObject[] g_PlayerUnits;
    public GameObject g_EnemyUnit;
    public GameObject g_BattleButtons;
    // 전투 상태를 정의하는 열거형
    public enum BattleState { START, ACTION, PLAYERTURN, PROCESS, ENEMYTURN, RESULT, END }

<<<<<<< Updated upstream
    // 플레이어와 적이 전투하는 위치
    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    public Transform waitStation;
=======
>>>>>>> Stashed changes

    private Coroutine BattleCoroutine;
    private bool isPlayed = false;
    private GameManager.Action m_ePlayerAction;
    private int m_iPlayerActionIndex;

    // 현재 플레이어와 적의 유닛의 스크립트
    public UnitEntity playerUnit;
    UnitEntity enemyUnit;

<<<<<<< Updated upstream
    // 전투 중 발생하는 대화를 표시하는 UI 텍스트
    public Text dialogueText;

    // 플레이어와 적의 HUD(Head-Up Display)를 관리하는 객체
    public BattleHUDCTR playerHUD;
    public BattleHUDCTR enemyHUD;

    // 전투 상태
=======
    //�떎�씠�뼹濡쒓렇 �뀓�뒪�듃
    public Text dialogueText;

  //�뵆�젅�씠�뼱��� �쟻 UI
    public BattleHUDCTR playerHUD;
    public BattleHUDCTR enemyHUD;

    // �쟾�닾 �긽�깭
>>>>>>> Stashed changes
    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        BattleInit();
    }


<<<<<<< Updated upstream

    #region 전투 관련 메서드
    void BattleInit()
    {
        //플레이어 유닛 초기화
        //g_PlayerUnits = GameManager.Instance.m_UnitManager.g_PlayerUnits;
        //적 유닛 초기화
        g_EnemyUnit = GameManager.Instance.m_UnitManager.SetUnitEntityByName("개굴닌자");

        
        state = BattleState.START;
        // 전투 시작 상태로 초기화하고, 전투를 설정하는 코루틴 실행
=======
    #region �쟾�닾 硫붿꽌�뱶
    void BattleInit()
    {
        // �쟻 �쑀�떅 �꽕�젙
        g_EnemyUnit = GameManager.Instance.m_UnitManager.SetUnitEntityByName(GameManager.Instance.g_sEnemyBattleUnit);
        state = BattleState.START;
>>>>>>> Stashed changes
        BattleCoroutine = StartCoroutine(SetupBattle());
    }


<<<<<<< Updated upstream



    // 플레이어 턴 시작 처리   ----------------------------------------------------------------------------------------------------------------------
=======
>>>>>>> Stashed changes
    private void PlayerAction()
    {
        //�뵆�젅�씠�뼱 �븸�뀡
        state = BattleState.ACTION;
<<<<<<< Updated upstream
        dialogueText.text = playerUnit.m_sUnitName + "는 어떻게 할 것 인가..";
    }
    #region 플레이어 Action 처리
=======
        dialogueText.text = playerUnit.m_sUnitName + "혡혬�뒗 臾댁뾿�쓣 �븷源�?";
    }
    #region 혬혣�뵆�젅�씠�뼱 �븸�뀡 泥섎━
>>>>>>> Stashed changes
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


<<<<<<< Updated upstream
    // 전투 종료 처리
    void AfterWin()
    {
        state = BattleState.END;
        dialogueText.text = "승리!";
=======

    void AfterWin()
    {
        state = BattleState.END;
        dialogueText.text = "�듅由ы뻽�떎!";
        SceneManager.UnloadSceneAsync("BattleScene");
        GameManager.Instance.g_GameState = GameManager.GameState.INPROGRESS;

>>>>>>> Stashed changes
    }

    void AfterLost()
    {
        state = BattleState.END;
<<<<<<< Updated upstream
        dialogueText.text = ".......당신은 눈앞이 깜깜해졌다.";
=======
        dialogueText.text = "�뙣諛고뻽�떎.";
        SceneManager.UnloadSceneAsync("BattleScene");
        GameManager.Instance.g_GameState = GameManager.GameState.INPROGRESS;
>>>>>>> Stashed changes
    }

    #endregion

<<<<<<< Updated upstream
    #region 전투 관련 코루틴
    // 전투 설정을 처리하는 코루틴
    IEnumerator SetupBattle()
    {
        // 플레이어와 적의 유닛을 생성하고 배치
        playerUnit = GameManager.Instance.m_UnitManager.g_PlayerUnits[0].transform.GetComponent<UnitEntity>();
        //UI의 이미지로 스프라이트를 지정하기 때문에 Station들을 삭제했습니다.
        //for(int i = 1; i< GameManager.Instance.m_UnitManager.g_PlayerUnits.Length;i++)
            //GameManager.Instance.m_UnitManager.g_PlayerUnits[i].transform.position = waitStation.position;
=======
    #region �쟾�닾 肄붾（�떞
    IEnumerator SetupBattle()
    {
        //�뵆�젅�씠�뼱 泥ル쾲吏� �쑀�떅�쑝濡� �꽕�젙, portrait �씠誘몄�� �꽕�젙
        playerUnit = GameManager.Instance.m_UnitManager.g_PlayerUnits[0].transform.GetComponent<UnitEntity>();
>>>>>>> Stashed changes
        playerHUD.g_imagePortrait.sprite = playerUnit.m_spriteUnitImage;
        //�쟻 �쑀�떅 �꽕�젙, portrait �씠誘몄�� �꽕�젙
        enemyUnit = g_EnemyUnit.GetComponent<UnitEntity>();
        enemyHUD.g_imagePortrait.sprite = enemyUnit.m_spriteUnitImage;

<<<<<<< Updated upstream
        // 대화 텍스트에 적의 이름을 표시
        dialogueText.text = "야생의 " + enemyUnit.m_sUnitName + " 이(가) 나타났다...";

        // 플레이어와 적의 HUD를 업데이트
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        // 전투 설정 후 잠시 대기
        yield return new WaitForSeconds(2f);

        // 플레이어 턴으로 상태 전환
        PlayerAction();
    }
    #region 플레이어 Action 처리
=======
        dialogueText.text = enemyUnit.m_sUnitName + "媛� �굹����궗�떎!";

        //UI �꽕�젙
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        yield return new WaitForSeconds(2f);

        //�븸�뀡 泥섎━濡� �꽆�뼱媛�
        PlayerAction();
    }
    #region 혬혣�뵆�젅�씠�뿬 �븸�뀡 泥섎━遺�遺�
>>>>>>> Stashed changes
    IEnumerator PlayerTurn_Attack()
    {
        state = BattleState.PLAYERTURN;
        //공격 실행
        playerUnit.AttackByIndex(playerUnit, enemyUnit, m_iPlayerActionIndex);
        enemyHUD.SetHP(enemyUnit.m_iCurrentHP);
        dialogueText.text = playerUnit.m_sUnitName + "의 " + playerUnit.GetSkillname(playerUnit,m_iPlayerActionIndex)+" 공격!!";
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

<<<<<<< Updated upstream
        // 플레이어의 체력을 HUD에 업데이트하고 대화 텍스트 표시
        playerHUD.SetHP(playerUnit.m_iCurrentHP);
        dialogueText.text = "체력을 5 회복했다!";
=======
        playerHUD.SetHP(playerUnit.m_iCurrentHP);
        dialogueText.text = "�븘�씠�뀥 �궗�슜!";
>>>>>>> Stashed changes

        yield return new WaitForSeconds(2f);

        Process();
        isPlayed = true;
    }
    IEnumerator PlayerTurn_Change()
    {
        state = BattleState.PLAYERTURN;

<<<<<<< Updated upstream
        // 플레이어 교체
        GameObject newPlayerGO = GameManager.Instance.m_UnitManager.g_PlayerUnits[m_iPlayerActionIndex];
        //playerUnit.transform.position = waitStation.position; // 이전 플레이어 이동
        //newPlayerGO.transform.position = playerBattleStation.position;
        playerUnit = newPlayerGO.GetComponent<UnitEntity>();
        playerHUD.g_imagePortrait.sprite = playerUnit.m_spriteUnitImage;


        // 플레이어의 체력을 HUD에 업데이트
=======
        //�뵆�젅�씠�뼱 �쑀�떅�뿉�꽌 媛��졇�삩 �쑀�떅�쑝濡� �꽕�젙
        GameObject newPlayerGO = GameManager.Instance.m_UnitManager.g_PlayerUnits[m_iPlayerActionIndex];

        //�뒪�겕由쏀듃瑜� 吏��젙�븯怨� 珥덇린�솕
        playerUnit = newPlayerGO.GetComponent<UnitEntity>();
        playerHUD.g_imagePortrait.sprite = playerUnit.m_spriteUnitImage;

        
>>>>>>> Stashed changes
        playerHUD.SetHUD(playerUnit);
        yield return new WaitForSeconds(2f);

        Process();
        isPlayed = true;
    }
    #endregion

<<<<<<< Updated upstream
    // 적의 턴을 처리하는 코루틴
=======
    //�쟻 怨듦꺽 泥섎━
>>>>>>> Stashed changes
    IEnumerator EnemyTurn()
    {
        // �쟾�닾 �긽�깭 蹂�寃�
        state = BattleState.ENEMYTURN;
<<<<<<< Updated upstream
        // 적이 공격하고 대화 텍스트 업데이트
=======
        // �옖�뜡 �씤�뜳�뒪濡� 怨듦꺽 �떎�뻾
>>>>>>> Stashed changes
        int randomAttackIndex = Random.Range(0, 2);
        enemyUnit.AttackByIndex(enemyUnit, playerUnit, randomAttackIndex);
        //�뀓�뒪�듃 泥섎━
        string AttackName = enemyUnit.GetSkillname(enemyUnit,randomAttackIndex);
        playerHUD.SetHP(playerUnit.m_iCurrentHP);
<<<<<<< Updated upstream
        dialogueText.text = enemyUnit.m_sUnitName + " 의 " + AttackName + "공격!";


        // 플레이어가 데미지를 받고 체력 업데이트
=======
        dialogueText.text = enemyUnit.m_sUnitName + "�쓽 " + AttackName + "怨듦꺽!";
>>>>>>> Stashed changes

        yield return new WaitForSeconds(1f);
        //泥대젰 寃��궗 �썑 吏꾪뻾
        if (enemyUnit.m_iCurrentHP <= 0 || playerUnit.m_iCurrentHP <= 0)
            BattleCoroutine = StartCoroutine(Result());
        else if (!isPlayed)
            Process();
        else
            BattleCoroutine = StartCoroutine(Result());
        isPlayed = true;

    }
<<<<<<< Updated upstream
    // 플레이어 회복을 처리하는 코루틴
=======
>>>>>>> Stashed changes


    IEnumerator Result()
    {
<<<<<<< Updated upstream
        dialogueText.text = "턴 실행 완료..";
=======
        dialogueText.text = "혙�꽩 �떎�뻾 �셿猷�";
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
    #region 버튼 클릭 이벤트
    // 공격 버튼 클릭 시 호출되는 메서드
    public void OnButton(GameManager.Action action, int index)
    {
        // 플레이어 턴이 아닌 경우에는 아무 작업도 수행하지 않음
=======
    #region 踰꾪듉 �겢由� 硫붿꽌�뱶
    public void OnButton(GameManager.Action action, int index)
    {
        //踰꾪듉 �룞�옉�쓣 action�쑝濡� 諛쏄퀬 �씠�뿉 �뵲�씪 process�뿉�꽌 吏꾪뻾
>>>>>>> Stashed changes
        if (state != BattleState.ACTION)
            return;
        m_ePlayerAction = action;
        m_iPlayerActionIndex = index;
        //�떎�떆 湲곕낯 UI �솢�꽦�솕
        g_BattleButtons.SetActive(true);
        //�븯�쐞 踰꾪듉 �궘�젣
        GameObject[] destroy = GameObject.FindGameObjectsWithTag("CreatedButtons");
        for (int i = 0; i< destroy.Length;i++)
            Destroy(destroy[i]);
        
        
        

        state = BattleState.PROCESS;
        Process();
    }
    #endregion
}
