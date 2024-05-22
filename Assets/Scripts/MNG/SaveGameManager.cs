using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameManager : MonoBehaviour
{
    public GameObject player;
    public QuestManager questManager;
    public Text QuestTalk; // Text ���� ����� ���� ����

    public GameObject menuSet; // menuSet ���� ����

    void Start()
    {
        GameLoad();
        QuestTalk.text = questManager.CheckQuest();
    }

    // ������ �����ϴ� �Լ�
    public void SaveGame()
    {
        // �÷��̾��� ���� ��ġ ����
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);

        // ����Ʈ ���� ��Ȳ ����
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);

        // PlayerPrefs ����
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
    }
}
