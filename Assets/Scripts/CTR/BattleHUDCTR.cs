using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUDCTR : MonoBehaviour
{
    // UI ��ҵ�
    public Text nameText;   // �̸��� ǥ���ϴ� �ؽ�Ʈ
    public Text levelText;  // ������ ǥ���ϴ� �ؽ�Ʈ
    public Text StateText;
    public Slider hpSlider; // ü���� ǥ���ϴ� �����̴�

    public Image g_imagePortrait; // �ʻ�ȭ �̹���

    // HUD�� �����ϴ� �޼���
    public void SetHUD(UnitEntity unit)
    {
        // ������ �̸��� �ؽ�Ʈ�� ����
        nameText.text = unit.m_sUnitName;
        // ������ ������ �ؽ�Ʈ�� ����
        levelText.text = "Lvl " + unit.m_iUnitLevel;
        // �����̴��� �ִ밪�� ������ �ִ� ü������ ����
        hpSlider.maxValue = unit.m_iUnitHP;
        // �����̴��� ��(ü��)�� ������ ���� ü������ ����
        hpSlider.value = unit.m_iCurrentHP;
        //StateText.text = unit.g_UnitState.ToString();
    }

    // ü���� ������Ʈ�ϴ� �޼���
    public void SetHP(int hp)
    {
        // �����̴��� ��(ü��)�� �־��� ������ ����
        hpSlider.value = hp;
    }
}
