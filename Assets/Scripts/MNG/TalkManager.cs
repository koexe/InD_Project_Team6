using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ȭ �� ĳ���� �ʻ�ȭ�� �����ϴ� ��ũ��Ʈ
public class TalkManager : MonoBehaviour
{
    // ��ȭ �����͸� ������ ��ųʸ�
    Dictionary<int, string[]> talkData;
    // ĳ���� �ʻ�ȭ�� ������ ��ųʸ�
    Dictionary<int, Sprite> portraitData;

    // �ʻ�ȭ ��������Ʈ �迭
    public Sprite[] portraitArr;

    // �ʱ�ȭ �Լ�
    void Awake()
    {
        // ��ųʸ����� �ʱ�ȭ
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        // ������ ���� �Լ� ȣ��
        GenerateData();
    }

    // ��ȭ �� �ʻ�ȭ ������ ���� �Լ�
    void GenerateData()
    {
        talkData.Add(30 + 100, new string[] { "���ݺ���", "�̺�Ʈ�� �����մϴ�!!" });

        // ��ȭ ������ �߰�
        talkData.Add(1000, new string[] { "�ȳ�!:0", "������ �ݰ���!:0" });
        talkData.Add(2000, new string[] { "���!:0", "������ �ݰ���!:0" });

        // ������ ��ȭ ������ �߰�
        talkData.Add(200, new string[] { "������1 �̴�." });
        talkData.Add(300, new string[] { "������2 �̴�." });

        //Quest Talk
        talkData.Add(10 + 1000, new string[] { "���:0",
                                            "�̸����� ���� ������ �ִٴµ�:0"
                                            ,"�˰��ִ�?:0" });

        talkData.Add(11 + 2000, new string[] { "���:0",
                                            "�ʵ� �̸����� ������ ���� �����ִ�?:0"
                                            ,"�˰������ ������ �ִ� ������ ã�ƿ���:0" });

        // ������ ����Ʈ ��� ----------------------------------
        talkData.Add(20 + 1000, new string[] { "�����ʿ� �ִ� ������� �̾߱�����:0" });
        talkData.Add(20 + 2000, new string[] { "ã���� �����ٸ�����:0" });
        talkData.Add(20 + 5000, new string[] { "������ �ݰ���.:0" });
        talkData.Add(21 + 2000, new string[] { "��������༭ ����:0" });

        // �ʻ�ȭ ������ �߰�
        portraitData.Add(100 + 0, portraitArr[0]);
        portraitData.Add(5000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(2000 + 0, portraitArr[1]);
    }

    // ������ ��ȭ ID�� ��ȭ �ε����� �ش��ϴ� ��ȭ ��ȯ
    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                // ����Ʈ �� ó�� ��縶�� ���� �� �⺻ ��縦 ��ȯ�մϴ�.
                return GetTalk(id - id % 100, talkIndex);
            }
            else
            {
                // �ش� ����Ʈ ���� ���� ��簡 ���� �� ����Ʈ �� ó�� ��縦 ��ȯ�մϴ�.
                return GetTalk(id - id % 10, talkIndex);
            }
        }

        // ��ȭ �ε����� ��ȭ ������ ���̿� ������ null�� ��ȯ�մϴ�.
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            // �׷��� ������ �ش� ��ȭ�� ��ȯ�մϴ�.
            return talkData[id][talkIndex];
        }
    }


    // ������ ID�� �ʻ�ȭ �ε����� �ش��ϴ� �ʻ�ȭ ��ȯ
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        // ID�� �ε����� ����Ͽ� �ʻ�ȭ ��ȯ
        return portraitData[id + portraitIndex];
    }
}
