using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCTR : MonoBehaviour
{
    public BattleManager g_BattleManager;
    public GameManager.Action g_eAction;
    
    public int g_iIndex;

    // ��������Ʈ ����
    delegate void OnButton(GameManager.Action action, int index);

    private void Start()
    {
        //BattleManager �Ҵ�
        g_BattleManager = GameObject.Find("BattleManager").transform.GetComponent<BattleManager>();
        // ��������Ʈ�� �����ϰ� �Ҵ�
        OnButton buttonDelegate = new OnButton(g_BattleManager.OnButton);

        // Button ������Ʈ�� onClick �̺�Ʈ�� ��������Ʈ ���
        gameObject.GetComponent<Button>().onClick.AddListener(() => buttonDelegate(g_eAction, g_iIndex));
    }
}