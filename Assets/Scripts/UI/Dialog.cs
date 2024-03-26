using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Scene ���� ����� ����ϱ� ���� ���ӽ����̽� �߰�

[System.Serializable]
public class Dialogue
{
    [TextArea]
    public string dialogue; // ��ȭ ������ �����ϴ� ����
    public Sprite cg; // ��ȭ �߿� ǥ���� �̹����� �����ϴ� ����
    public bool isLeft; // �̹����� ���ʿ� ǥ������ ���θ� ��Ÿ���� ����
}

public class Dialog : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteStandingCG; // ĳ���� �̹����� ǥ���ϴ� SpriteRenderer
    [SerializeField] private SpriteRenderer m_spriteDialogueBox; // ��ȭ ���ڸ� ǥ���ϴ� SpriteRenderer
    [SerializeField] private Text m_txtDialogue; // ��ȭ �ؽ�Ʈ�� ǥ���ϴ� Text ������Ʈ
    [SerializeField] private Vector2 m_leftPosition; // ���ʿ� ǥ���� ��ǥ
    [SerializeField] private Vector2 m_rightPosition;// �����ʿ� ǥ���� ��ǥ

    [SerializeField] private Dialogue[] m_dialogue; // ��ȭ ������ ��� �ִ� �迭


    private bool m_isDialogue = false; // ��ȭ ������ ���θ� ��Ÿ���� �÷���
    private int m_count = 0; // ���� ��ȭ �ε����� �����ϴ� ����

    // ��ȭ ���� �޼���
    public void ShowDialogue()
    {
        OnOff(true); // ��ȭ ����, ĳ���� �̹���, ��ȭ �ؽ�Ʈ�� ȭ�鿡 ǥ��
        m_count = 0; // ��ȭ �ε��� �ʱ�ȭ
        NextDialogue(); // ù ��° ��ȭ�� �̵�
    }

    // UI ��Ҹ� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�ϴ� �޼���
    private void OnOff(bool _flag)
    {
        m_spriteDialogueBox.gameObject.SetActive(_flag);
        m_spriteStandingCG.gameObject.SetActive(_flag);
        m_txtDialogue.gameObject.SetActive(_flag);
        m_isDialogue = _flag;
    }

    // ���� ��ȭ�� �̵��ϴ� �޼���
    private void NextDialogue()
    {
        m_txtDialogue.text = m_dialogue[m_count].dialogue; // ��ȭ �ؽ�Ʈ ������Ʈ
        m_spriteStandingCG.sprite = m_dialogue[m_count].cg; // ĳ���� �̹��� ������Ʈ

        // �̹��� ��ġ�� ����
        if (m_dialogue[m_count].isLeft)
        {
            m_spriteStandingCG.transform.localPosition = m_leftPosition; // ���ʿ� ǥ��
        }
        else
        {
            m_spriteStandingCG.transform.localPosition = m_rightPosition; // �����ʿ� ǥ��
        }

        m_count++; // ���� ��ȭ�� �̵�
    }

    void Update()
    {
        // ������ ���� �������� Ȯ��
        if (Time.timeScale != 0)
        {
            // ��ȭ ���� ��
            if (m_isDialogue)
            {
                // ���콺 ���� ��ư�� Ŭ������ ��
                if (Input.GetMouseButtonDown(0))
                {
                    // ��ȭ�� �������� ���
                    if (m_count < m_dialogue.Length)
                    {
                        NextDialogue(); // ���� ��ȭ�� �̵�
                    }
                    // ��ȭ�� ������ ���
                    else
                    {
                        OnOff(false); // ��� ��ȭ�� ������ ��ȭ ����
                        SceneManager.LoadScene("BattleScene");
                    }
                }
            }
        }
    }
}
