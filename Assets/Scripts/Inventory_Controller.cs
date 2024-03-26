using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory_Controller : MonoBehaviour
{
    // g_fCharacterSpeed -> g�� �۷ι�(public) m�� ���(private) ���� f(float)/i(int)/s(string)
    [Header("�κ��丮 �¿����� ���� �θ� ���� �޾ƿ�")]
    public GameObject g_gin_V;
    [Header("���� �κ��丮")]
    public GameObject g_ginventory;
    public static Inventory_Controller g_ICinstance;
    public Slot[] g_Sslot; // �κ��丮 �ȿ� �ִ� �� ���Ե�
    public Item g_Iget_Item; // ȹ���� ������

    public Slot g_Sselect_Item; // ���� �κ��丮���� ������ ������Ʈ ����
    //public Miri_Slot select_Item_Miri; // �̸����� �κ��丮���� ������ ������Ʈ ����
    public GameObject g_gselect_Item_Ob;

    public Item g_Iclick_Item; // Ŭ���� ������s
    public int g_iclick_Item_Count; // Ŭ���� ������ ����

    public TextMeshProUGUI g_tmoney_View; // ���� ������ �ݾ� UI
    public int g_imoney; // ���� �ݾ�


    // Start is called before the first frame update

    private void Awake()
    {
        g_ICinstance = this;
        for (int i = 0; i < g_ginventory.transform.childCount; i++)  // ����Ƽ â���� ������ �־��ִ°� �ƴϰ� ��ũ��Ʈ���� �־��ִ°�
        {
            g_Sslot[i] = g_ginventory.transform.GetChild(i).GetComponent<Slot>(); // ����Ƽ�󿡼� �κ��丮��� ������Ʈ �ȿ� ���Ե��� �ֱ⶧���� �� ���Ե��� �����ͼ� �迭�� �־���
        }
    }
    private void Start()
    {
       
        //g_gin_V.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        View_Inventory();

        g_tmoney_View.text = g_imoney.ToString();
    }

    public void Check_Slot(int num = 1) // ȹ���� �������� �κ��丮�� �־��ִ� �Լ�
    {
        Item item = null; // ȹ���� �������� �κ��丮�� �ִ��� �������� �Ǵ����ִ� ����
        foreach (Slot slot_B in g_Sslot)
        {
            if (slot_B != null && slot_B.g_Ihave_item != null)  // �κ��丮�� ������� �ʴٸ�
            {
                if (slot_B.g_Ihave_item.item_Name == g_Iget_Item.item_Name) // ���� ������ �ִ� �����۰� �κ��丮�� �ִ� �������� ���ٸ�
                {
                    item = g_Iget_Item; // ������ ���� ������ �ִ� �������� �־���
                    break;
                }
            }
        }
        Put_Invent(item, num);
    }

    public void Put_Invent(Item item, int num = 1)
    {
        for (int i = 0; i <= g_Sslot.Length; i++)
        {
            if (item == null) //�κ��丮�� ���� ������ �ִ� �������� ���ٸ�
            {
                if (g_Sslot[i].g_Ihave_item == null) // �κ��丮�� ���������
                {
                    g_Sslot[i].Input_Item(g_Iget_Item, num); // ����ִ� ���Կ� ���� �������� �־���
                    break;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                if (g_Sslot[i].g_Ihave_item != null) // �κ��丮�� ������� �ʴٸ�
                {
                    if (g_Sslot[i].g_Ihave_item.item_Name == item.item_Name) // �κ��丮�� ���� ������ �ִ� �������� �ִٸ�
                    {
                        g_Sslot[i].Input_Item(g_Iget_Item, num); // ���Կ� �������� �־���
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }
            }
        }
    }
    public void View_Inventory() // �κ��丮 �� ���� �Լ�
    {
        if (g_gin_V.transform.localScale == new Vector3(1, 1, 1)) // �κ��丮�� ��������
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.I))
            {
                for (int i = 0; i < g_ginventory.transform.childCount; i++)  // ����Ƽ â���� ������ �־��ִ°� �ƴϰ� ��ũ��Ʈ���� �־��ִ°�
                {
                    g_Sslot[i].GetComponent<Slot_Button>().Off_Inven();
                }
                g_gin_V.transform.localScale = new Vector3(0, 0, 1); // ����
            }
        }
        else if (g_gin_V.transform.localScale == new Vector3(0, 0, 1))
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                //g_gin_V.gameObject.SetActive(true); // ����
                g_gin_V.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
