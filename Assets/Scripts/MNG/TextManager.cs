using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public TalkManager m_TalkManager;
    public QuestManager questManager;
    public GameObject m_talkPanel;
    public Image m_portraitImg;
    public Text m_talkText;
    public GameObject m_scanObject;
    public bool isAct;
    public int talkIndex;
    public Text QuestTalk;

    // ������ ���۵� �� ���� ����Ʈ�� ���¸� Ȯ���Ͽ� �α׿� ����մϴ�.
    void Start()
    {
        QuestTalk.text = questManager.CheckQuest();
    }


    // ��ȣ�ۿ� 
    public void Act(GameObject scanObj)
    {
        // ��ĵ�� ������Ʈ�� �����ϰ� ������Ʈ �����͸� ������
        m_scanObject = scanObj;
        ObjData objData = m_scanObject.GetComponent<ObjData>();
        // ��ȭ �Լ� ȣ��
        Talk(objData.id, objData.isNpc);

        // ��ȭ �г� Ȱ��ȭ
        m_talkPanel.SetActive(isAct);
    }

    #region ��ȭ ����
    void Talk(int id, bool isNpc)
    {
        // ��ȭ ������ ��������
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = m_TalkManager.GetTalk(id + questTalkIndex, talkIndex);

        // ��ȭ �����Ͱ� ������ ��ȣ�ۿ� ����
        if (talkData == null)
        {
            isAct = false;
            talkIndex = 0;
            QuestTalk.text = questManager.CheckQuest();
            Debug.Log(questManager.CheckQuest(id));
            return;
        }

        // NPC�� ���
        if (isNpc)
        {
            // ��ȭ �ؽ�Ʈ ����
            m_talkText.text = talkData.Split(':')[0];
            // �ʻ�ȭ �̹��� ����
            m_portraitImg.sprite = m_TalkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            m_portraitImg.color = new Color(1, 1, 1, 1); // �ʻ�ȭ �̹��� ���� ����
        }
        else // NPC�� �ƴ� ���
        {
            // ��ȭ �ؽ�Ʈ ����
            m_talkText.text = talkData;
            // �ʻ�ȭ �̹��� �����ϰ� ����
            m_portraitImg.color = new Color(1, 1, 1, 0);
        }

        // ��ȣ�ۿ� Ȱ��ȭ �� ��ȭ �ε��� ����
        isAct = true;
        talkIndex++;
    }
    #endregion
}
