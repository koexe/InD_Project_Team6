using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoOption : MonoBehaviour
{
    // ��ü ȭ�� ��带 ������ ����
    private FullScreenMode m_screenMode;

    // �ػ� ������ ���� ��Ӵٿ� UI�� ������ ����
    public Dropdown g_resolutionDropdown;

    // ��ü ȭ�� ��� ��ư�� ������ ����
    public Toggle g_fullscreenBtn;

    // ������ �ػ� ����� ������ ����Ʈ
    private List<Resolution> m_resolutions = new List<Resolution>();

    // public���� �����Ͽ� �ν����� â�� ǥ��
    public int m_resolutionNum;

    void Start()
    {
        // UI �ʱ�ȭ �Լ� ȣ��
        InitUI();
    }

    // UI�� �ʱ�ȭ�ϴ� �Լ�
    void InitUI()
    {
        // ȭ�� �ػ� �� �ֻ����� 60�� �͸� �߰�
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 60)
            {
                m_resolutions.Add(Screen.resolutions[i]);
            }
        }

        // ��Ӵٿ� �ɼ� �ʱ�ȭ
        g_resolutionDropdown.options.Clear();

        int optionNum = 0;
        // ������ �ػ� ����� ��Ӵٿ �߰��ϰ� ���� �ػ󵵿� �ش��ϴ� �ɼ��� ����
        foreach (Resolution item in m_resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + "x" + item.height + " " + item.refreshRate + "hz";
            g_resolutionDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
            {
                g_resolutionDropdown.value = optionNum;
            }
            optionNum++;
        }
        // ��Ӵٿ� ���� ����
        g_resolutionDropdown.RefreshShownValue();

        // ��ü ȭ�� ��� ��ư �ʱ�ȭ
        g_fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    // ��Ӵٿ� �ɼ��� ����Ǿ��� �� ȣ��Ǵ� �Լ�
    public void DropboxOptionChange(int x)
    {
        // ���õ� �ػ��� �ε����� ����
        m_resolutionNum = x;
    }

    // ��ü ȭ�� ��� ��ư�� ����Ǿ��� �� ȣ��Ǵ� �Լ�
    public void FullScreenBtn(bool isFull)
    {
        // ��ü ȭ�� ��带 ����
        m_screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    // Ȯ�� ��ư�� Ŭ���Ǿ��� �� ȣ��Ǵ� �Լ�
    public void OkBtnClick()
    {
        // ���õ� �ػ󵵿� ��ü ȭ�� ���� ȭ�� ���� ����
        Screen.SetResolution(m_resolutions[m_resolutionNum].width, m_resolutions[m_resolutionNum].height, m_screenMode);
    }
}
