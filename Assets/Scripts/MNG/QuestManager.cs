using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;
    Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("�������� �̾߱� ������.", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("�������� ȹ���ϱ�.", new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData("Ư������ ����", new int[] { 100 }));
        questList.Add(40, new QuestData("����Ʈ �� Ŭ����.", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        // Control Quest Object
        ControlQuestObject();

        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        return questList[questId].questName;
    }

    public string CheckQuest() // �Ű� ������ ���� �Լ�ȣ��
    {
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlQuestObject()
    {
        // ���� ����Ʈ ID�� ���� Ư�� ����Ʈ ���� Ư�� ������Ʈ�� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�մϴ�.
        switch (questId)
        {
            case 10:
                // ����Ʈ ID�� 10�� ���,
                // questActionIndex�� 2�� �� questObject�� ù ��° ��Ҹ� Ȱ��ȭ�մϴ�.
                if (questActionIndex == 2)
                    questObject[0].SetActive(true);
                break;
            case 20:
                // ����Ʈ ID�� 20�� ���, questActionIndex�� 1�� �� questObject�� ù ��° ��Ҹ� ��Ȱ��ȭ�մϴ�.
                if (questActionIndex == 1)
                    questObject[0].SetActive(false);
                else if (questActionIndex == 2)
                    questObject[1].SetActive(true);
                break;
            case 30:
                // ����Ʈ ID�� 30�� ���, questActionIndex�� 1�� �� questObject�� ù ��° ��Ҹ� ��Ȱ��ȭ�մϴ�.
                if (questActionIndex == 1)
                    questObject[1].SetActive(false);
                break;
        }
    }


}
