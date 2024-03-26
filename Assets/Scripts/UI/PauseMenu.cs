using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // ���� �Ͻ� ���� UI�� ������ GameObject ����
    public GameObject g_PauseUI;

    // ������ �Ͻ� �����Ǿ������� ��Ÿ���� �Ҹ��� ����
    private bool m_paused = false;

    // Start �Լ��� ó�� �� �� ȣ��˴ϴ�.
    void Start()
    {
        // ���� �� �Ͻ� ���� UI�� ��Ȱ��ȭ�մϴ�.
        g_PauseUI.SetActive(false);
    }

    // Update �Լ��� �� �����Ӹ��� ȣ��˴ϴ�.
    void Update()
    {
        // "Pause" �Է��� �����Ǹ�
        if (Input.GetButtonDown("Pause"))
        {
            // �Ͻ� ���� ���¸� ������ŵ�ϴ�.
            m_paused = !m_paused;
        }

        // ���� ������ �Ͻ� ���� ���¶��
        if (m_paused)
        {
            // �Ͻ� ���� UI�� Ȱ��ȭ�ϰ� �ð��� ����ϴ�.
            g_PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else // �Ͻ� ���� ���°� �ƴ϶��
        {
            // �Ͻ� ���� UI�� ��Ȱ��ȭ�ϰ� �ð��� ���������� �����մϴ�.
            g_PauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    // Resume �Լ��� �簳 ��ư�� ������ �� ȣ��˴ϴ�.
    public void Resume()
    {
        // �Ͻ� ���� ���¸� ������ŵ�ϴ�.
        m_paused = !m_paused;
    }
}
