using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZone : MonoBehaviour
{
    // g_fCharacterSpeed -> g�� �۷ι�(public) m�� ���(private) ���� f(float)/i(int)/s(string)
    public GameObject[] g_gmonster_List;
    public GameObject g_gconfirmed_Monster; // ������ ����
    public float g_fpercent; // ���� ���� Ȯ��

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopCoroutine("Find_Monster");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine("Find_Monster");
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
                g_gconfirmed_Monster = g_gmonster_List[random_monster_number]; // �������� ���� ���͸� ���� �����ϴ� ���Ϳ� �־���.
                print("�߻��� " + g_gmonster_List[random_monster_number].name + "�����ߴ�!");
                break;
            }
        }
    }
}
