using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    // g_fCharacterSpeed -> g�� �۷ι�(public) m�� ���(private) ���� f(float)/i(int)/s(string)
    public static Slot g_SInstance;
    public Image g_iitem_Image;
    public Sprite g_snull_item_Image;
    public Item g_Ihave_item;
    public TextMeshProUGUI g_titem_Number_UI;

    public int item_Number; // ȹ���� ������ ����
    Slot_Button s_B;

    private void Awake()
    {
        g_SInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        s_B = GetComponent<Slot_Button>();
    }

    // Update is called once per frame
    void Update()
    {
        Show_Item();
        Empty_Check();
    }

    public void Empty_Check()
    {
        g_titem_Number_UI.text = item_Number.ToString(); // �����ϰ� �ִ� ������ ����
        if (g_Ihave_item != null)
        {
            if (item_Number == 0) // �������� ���ٸ� �������� �� ����ߴٸ�
            {
                g_Ihave_item = null;               // ���� 
                g_iitem_Image.sprite = g_snull_item_Image;       // �ʱ�
                g_titem_Number_UI.text = " ";     // ȭ
            }
            else
            {
                g_iitem_Image.sprite = g_Ihave_item.item_Image;
            }
        }
        else
        {
            g_titem_Number_UI.text = " ";
            item_Number = 0;
        }
    }
    public void Input_Item(Item item, int num = 1) // ������ �ֱ�
    {
        if (item != null) // �޾ƿ� �������� �ִٸ�
        {
            g_Ihave_item = item;  // ���� ������ ������ ������ �������� �ְ�
            item_Number += num;
        }
    }

    void Show_Item()
    {
        if (g_Ihave_item != null)
        {
            g_iitem_Image.sprite = g_Ihave_item.item_Image ; // ������ �̹��� �Ҵ�
        }
        else
        {
            g_iitem_Image.sprite = g_snull_item_Image;
        }
    }
}
