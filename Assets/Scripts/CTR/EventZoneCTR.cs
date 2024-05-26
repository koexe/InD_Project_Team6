using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZoneCTR: MonoBehaviour
{
    [System.Serializable]
    public class Monster
    {
        public string m_sName;
        public int m_iChance;
    }
    // g_fCharacterSpeed -> g�� �۷ι�(public) m�� ���(private) ���� f(float)/i(int)/s(string)
    [SerializeField] public Monster[] g_gmonster_List;
    public float g_fpercent; // ���� ���� Ȯ��
    public int[] g_iLevelBoundary;
    public Coroutine FindCoroutine;
    int random;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(FindCoroutine != null)
            {
                StopCoroutine(FindCoroutine);
                FindCoroutine = null;
            }

        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameManager.Instance.g_GameState == GameManager.GameState.INPROGRESS)
        {
            if (collision.CompareTag("Player"))
            {
                if (FindCoroutine == null)
                {             
                    FindCoroutine = StartCoroutine("Find_Monster");
                    
                }
            }
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindCoroutine = StartCoroutine("Find_Monster");
        }
    }

    IEnumerator Find_Monster() 
    {
        while (true)
        {
            if (GameManager.Instance.g_GameState == GameManager.GameState.INPROGRESS)
            {
                int random_percent_num = Random.Range(1, 101);
                if (random_percent_num < g_fpercent)
                {
                    GameManager.Instance.g_GameState = GameManager.GameState.BATTLE;
                    int max_chace = 0;
                    for (int i = 0; i < g_gmonster_List.Length; i++)
                    {
                        max_chace += g_gmonster_List[i].m_iChance;
                    }
                    int random_monster_number = Random.Range(0, max_chace);
                    string name = CalMonsterChance(g_gmonster_List, random_monster_number);

                    //FindCoroutine = null;
                    int random_monster_lvl = Random.Range(g_iLevelBoundary[0], g_iLevelBoundary[1]);

                    GameManager.Instance.LoadBattleScene(name, random_monster_lvl);
                    break;
                }
            }
            yield return new WaitForSecondsRealtime(1f);
        }
        FindCoroutine = null;
    }
    

    private string CalMonsterChance(Monster[] mons, int chance)
    {
        int[] chanceArr = new int[mons.Length];
        for (int i = 0; i < mons.Length; i++)
        {
            chanceArr[i] = 0;
            for (int j = 0; j <= i ; j++)
            {
                chanceArr[i] += mons[j].m_iChance;
            }
        }
        for (int i = 0; i < chanceArr.Length; i++)
        {
            if(i == 0)
            {
                if (chanceArr[i] > chance)
                    return mons[i].m_sName;
            }
            else if (chanceArr[i-1] < chance && chance < chanceArr[i])
            {
                return mons[i].m_sName;
            }
        }
        return "";
    }
}
