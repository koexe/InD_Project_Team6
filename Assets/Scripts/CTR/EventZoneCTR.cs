using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZoneCTR: MonoBehaviour
{
    // g_fCharacterSpeed -> g�� �۷ι�(public) m�� ���(private) ���� f(float)/i(int)/s(string)
    public string[] g_gmonster_List;
    public float g_fpercent; // ���� ���� Ȯ��
    public int[] g_iLevelBoundary;
    public Coroutine FindCoroutine;


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopCoroutine(FindCoroutine);
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

    IEnumerator Find_Monster() // Ư�� �����ȿ� ���Ϳ� ��������
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);

            int random_percent_num = Random.Range(1, 101); // �ۼ�Ʈ���� ���� �̱�

            if (random_percent_num <= g_fpercent) // percent�� ���� �ȿ� ����ִ� ���� ��ŭ�� �ۼ�Ʈ�� �̺�Ʈ �߻�
            {
                int random_monster_number = Random.Range(0, g_gmonster_List.Length); // ���� �̱�
                int random_monster_Level = Random.Range(g_iLevelBoundary[0], g_iLevelBoundary[1]);
                GameManager.Instance.LoadBattleScene(g_gmonster_List[random_monster_number],random_monster_Level);
                FindCoroutine = null;
                break;
            }
        }
    }
}
