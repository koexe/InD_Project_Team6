using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class Inventory_Controller : MonoBehaviour
{
    // g_fCharacterSpeed -> g�� �۷ι�(public) m�� ���(private) ���� f(float)/i(int)/s(string)
    [Header("�κ��丮 �¿����� ���� �θ� ���� �޾ƿ�")]
    public GameObject g_gin_V;
    [Header("���� �κ��丮")]
    public GameObject g_ginventory;
    public static Inventory_Controller g_ICinstance;

    [Header("�κ��丮 ����")]
    public List<Slot> g_Sslot; // �κ��丮 �ȿ� �ִ� �� ���Ե�
    public List<int> item_Num_Sort; // �κ��丮 �ȿ� �ִ� �� ���Ե�


    //public Slot[] g_Sslot; // �κ��丮 �ȿ� �ִ� �� ���Ե�
    public Slot g_Sclick_Slot; // ���� ����
    public ItemEntity g_Iget_Item; // ȹ���� ������

    public Slot g_Sselect_Item; // ���� �κ��丮���� ������ ������Ʈ ����
    //public Miri_Slot select_Item_Miri; // �̸����� �κ��丮���� ������ ������Ʈ ����
    public GameObject g_gselect_Item_Ob;

    public ItemEntity g_Iclick_Item; // Ŭ���� ������s
    public int g_iclick_Item_Count; // Ŭ���� ������ ����

    public bool invent_On_Off_Check; // �κ��丮�� �����ִ��� �����ִ��� Ȯ�����ִ� ����
    public bool lock_UI; // Ư���� UI�� ���������� �ٸ��� �ǵ��� ���ϰ� ��
    public GameObject discard_value_View; // ���� ���� �Է� â ����

    [Header("�κ��丮 ������ ǥ�� UI")]
    public Image Img_View;
    public TextMeshProUGUI name_View;
    public TextMeshProUGUI Des_View;
    public GameObject UseButton;
    // Start is called before the first frame update

    private void Awake()
    {
        g_ICinstance = this;
        
    }
    private void Start()
    {
        lock_UI = true;
        //g_Sslot = new Slot[g_ginventory.transform.childCount];
        for (int i = 0; i < g_ginventory.transform.childCount; i++)  // ����Ƽ â���� ������ �־��ִ°� �ƴϰ� ��ũ��Ʈ���� �־��ִ°�
        {
            g_Sslot.Add(g_ginventory.transform.GetChild(i).GetComponent<Slot>()); // ����Ƽ�󿡼� �κ��丮��� ������Ʈ �ȿ� ���Ե��� �ֱ⶧���� �� ���Ե��� �����ͼ� �迭�� �־���
            g_Sslot[i].g_iitem_Number = 0;
        }
        g_gin_V.transform.localScale = new Vector3(0, 0, 1);
    }
    // Update is called once per frame
    void Update()
    {
        View_Inventory();
        if (invent_On_Off_Check)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                item_Num_Sort.Clear();

                for (int i = 0; i < g_Sslot.Count; i++)
                {
                    if (g_Sslot[i].g_Ihave_item != null)
                    {
                        item_Num_Sort.Add(g_Sslot[i].g_Ihave_item.number);
                    }
                }
                item_Num_Sort.Sort();



                for (int i = 0; i < item_Num_Sort.Count; i++)
                {
                    for (int g = 0; g < g_Sslot.Count; g++)
                    {
                        if (g_Sslot[g].g_Ihave_item != null)
                        {
                            if (item_Num_Sort[i] == g_Sslot[g].g_Ihave_item.number)
                            {
                                // Slot temp = g_Sslot[i];
                                Slot temp = new Slot();
                                temp.g_iitem_Image = g_Sslot[i].g_iitem_Image;
                                temp.g_snull_item_Image = g_Sslot[i].g_snull_item_Image;
                                temp.g_Ihave_item = g_Sslot[i].g_Ihave_item;
                                temp.g_iitem_Number = g_Sslot[i].g_iitem_Number;

                                g_ginventory.transform.GetChild(i).gameObject.GetComponent<Slot>().g_iitem_Image.sprite = g_Sslot[g].g_iitem_Image.sprite;
                                g_ginventory.transform.GetChild(i).gameObject.GetComponent<Slot>().g_snull_item_Image = g_Sslot[g].g_snull_item_Image;
                                g_ginventory.transform.GetChild(i).gameObject.GetComponent<Slot>().g_Ihave_item = g_Sslot[g].g_Ihave_item;
                                g_ginventory.transform.GetChild(i).gameObject.GetComponent<Slot>().g_iitem_Number = g_Sslot[g].g_iitem_Number;

                                g_Sslot[g].g_iitem_Image.sprite = temp.g_iitem_Image.sprite;
                                g_Sslot[g].g_snull_item_Image = temp.g_snull_item_Image;
                                g_Sslot[g].g_Ihave_item = temp.g_Ihave_item;
                                g_Sslot[g].g_iitem_Number = temp.g_iitem_Number;
                                temp = null;
                                break;
                            }
                        }
                        else
                        {

                        }
                    }
                }
 
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (discard_value_View.activeSelf == false)
            {
                {
                    discard_value_View.SetActive(true);
                    lock_UI = false;
                }
            }
            else
            {
                discard_value_View.SetActive(false);
                lock_UI = true;
            }
        }

    }

    public void Check_Slot(int num = 1) // ȹ���� �������� �κ��丮�� �־��ִ� �Լ�
    {
        ItemEntity item = null; // ȹ���� �������� �κ��丮�� �ִ��� �������� �Ǵ����ִ� ����
        foreach (Slot slot_B in g_Sslot)
        {
            if (slot_B != null && slot_B.g_Ihave_item != null)  // �κ��丮�� ������� �ʴٸ�
            {
                if (slot_B.g_Ihave_item.m_sItemName == g_Iget_Item.m_sItemName) // ���� ������ �ִ� �����۰� �κ��丮�� �ִ� �������� ���ٸ�
                {
                    item = g_Iget_Item; // ������ ���� ������ �ִ� �������� �־���
                    break;
                }
            }
        }
        Put_Invent(item, num);
    }

    public void Put_Invent(ItemEntity item, int num = 1)
    {
        for (int i = 0; i <= g_Sslot.Count; i++)
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
                    if (g_Sslot[i].g_Ihave_item.m_sItemName == item.m_sItemName) // �κ��丮�� ���� ������ �ִ� �������� �ִٸ�
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (GameManager.Instance.g_GameState == GameManager.GameState.BATTLE)
            {
                if(GameObject.Find("BattleManager") == null)
                {
                    GameManager.Instance.g_GameState = GameManager.GameState.INPROGRESS;
                }
                return;
            }
               
            if (g_gin_V.transform.localScale == new Vector3(1, 1, 1)) // �κ��丮�� ��������
            {
                Hide_Inv();
            }
            else if (g_gin_V.transform.localScale == new Vector3(0, 0, 1))
            {
                Change_UI.instance.ALL_OFF_UI();
                Show_Inv();
            }
        }
    }

    public void Set_GetItem(GameObject itemEntity)
    {
        GameObject entity = Instantiate(itemEntity,GameObject.Find("Inventory_GOs").transform);
        Destroy(entity.transform.GetComponent<SpriteRenderer>());
        Destroy(entity.transform.GetComponent<Collider2D>());
        g_Iget_Item = entity.GetComponent<ItemEntity>();
    }
    public void Show_Inv()
    {
        Img_View.gameObject.SetActive(false);
        name_View.text = " ";
        Des_View.text = " ";
        invent_On_Off_Check = true;
        g_gin_V.transform.localScale = new Vector3(1, 1, 1);
    }
    public void Hide_Inv()
    {
        for (int i = 0; i < g_ginventory.transform.childCount; i++)  // ����Ƽ â���� ������ �־��ִ°� �ƴϰ� ��ũ��Ʈ���� �־��ִ°�
        {
            g_Sslot[i].GetComponent<Slot_Button>().Off_Inven();
        }
        g_gin_V.transform.localScale = new Vector3(0, 0, 1); // ����
        invent_On_Off_Check = false;

        if (GameManager.Instance.g_GameState == GameManager.GameState.BATTLE)
        {
            GameObject.Find("BattleManager").transform.GetComponent<BattleManager>().state = BattleManager.BattleState.ACTION;
        }
        UseButton.SetActive(false);
    }
}
