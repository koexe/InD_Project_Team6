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
        questList.Add(10, new QuestData("ó�����ΰ� ����", new int[] { 100, 200 }));
        questList.Add(20, new QuestData("���¿��� ����", new int[] { 300,  500 }));
        questList.Add(30, new QuestData("������ ���������� �̸��� ��������!!", new int[] { 800,1200, 900, 1300 }));
        questList.Add(40, new QuestData("������ ���������� �̸��� ������!!", new int[] { 1500,1600 }));
        questList.Add(50, new QuestData("��ȣ�� ���������� �̸��� �ܿ��!!", new int[] { 2100, 2200}));
        questList.Add(60, new QuestData("Ȳ���� ���������� �̸��� �߾�����!!", new int[] { 2500, 2600, 0 }));
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
                if (questActionIndex == 1)
                {
                    questObject[0].SetActive(false);
                    questObject[1].SetActive(true);
                }
                else if (questActionIndex == 2)
                {
                    questObject[1].SetActive(false);
                }
                break;
            case 20:
                // ����Ʈ ID�� 20�� ���, questActionIndex�� 1�� �� questObject�� ù ��° ��Ҹ� ��Ȱ��ȭ�մϴ�.
                if (questActionIndex == 1)
                {
                    questObject[2].SetActive(false);
                    questObject[4].SetActive(false);
                    questObject[5].SetActive(true);
                }
                else if (questActionIndex == 2)
                {
                    questObject[3].SetActive(false);
                    questObject[5].SetActive(false);

                }
                else if (questActionIndex == 3)
                {
                    questObject[6].SetActive(false);
                }
              
                break;
            case 30:
                if (questActionIndex == 1)
                    questObject[8].SetActive(false);
                else if (questActionIndex == 2)
                {
                    questObject[10].SetActive(false);
                    questObject[17].SetActive(true);
                    questObject[7].SetActive(false);
                    questObject[9].SetActive(false);
                }
                else if (questActionIndex == 3)
                {
                    questObject[11].SetActive(false);
                    questObject[12].SetActive(true);
                    questObject[18].SetActive(true);

                }
                break;

            case 40:
                if (questActionIndex == 1)
                    questObject[13].SetActive(false);
                    questObject[14].SetActive(true);
                    questObject[19].SetActive(true);
                break;

            case 50:
                if (questActionIndex == 1)
                    questObject[15].SetActive(false);
                questObject[16].SetActive(true);
                break;
            case 60:
                if (questActionIndex == 1)
                    questObject[20].SetActive(false);
                questObject[21].SetActive(true);
                break;


        }
    }


}
