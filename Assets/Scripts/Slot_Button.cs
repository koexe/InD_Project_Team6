using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_Button : MonoBehaviour, IPointerClickHandler
{
    // g_fCharacterSpeed -> g�� �۷ι�(public) m�� ���(private) ���� f(float)/i(int)/s(string)
    public static Slot_Button g_SBinstance;
    public Image g_ishow_Item_Image; // ���콺�� ����ٴ� �̹���
    [SerializeField] Slot m_Sslot;
    [SerializeField] Item m_ISr_S;

    private int m_inum;

    public Canvas canvas;
    public void Awake()
    {
        g_SBinstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Sslot = GetComponent<Slot>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) // ������ �̵�
        {
            m_ISr_S = m_Sslot.g_Ihave_item;
            m_inum = m_Sslot.item_Number;
            if (m_Sslot.g_Ihave_item != null && Inventory_Controller.g_ICinstance.g_Iclick_Item == null)// ���õ� ������Ʈ�� ������
            {
                Inventory_Controller.g_ICinstance.g_Iclick_Item = m_Sslot.g_Ihave_item; // ���� Ŭ���� ������ ������ �κ��丮 ��ũ��Ʈ ������ �Ҵ�
                Inventory_Controller.g_ICinstance.g_iclick_Item_Count = m_Sslot.item_Number; // ���� Ŭ���� ������ ������ �κ��丮  ��ũ��Ʈ ������ �Ҵ�
                m_Sslot.g_Ihave_item = null; // ���� ���Կ� �������� �����
                m_Sslot.item_Number = 0; // ���� ���Կ� ������ ���ڸ� ������
            }

            else if (Inventory_Controller.g_ICinstance.g_Iclick_Item != null && m_Sslot.g_Ihave_item == null)// �������� ������ ���¿��� �� ������ Ŭ������ ��
            {
                m_Sslot.g_Ihave_item = Inventory_Controller.g_ICinstance.g_Iclick_Item; // Ŭ���� ���� �����ۿ� Ŭ���߾��� ������ ������ �Ҵ�
                m_Sslot.item_Number = Inventory_Controller.g_ICinstance.g_iclick_Item_Count; // Ŭ���� ���� ������ ������ Ŭ���ߴ� �������� ������ ������ �Ҵ�
                Inventory_Controller.g_ICinstance.g_Iclick_Item = null; // Ŭ���ߴ� ������ ���� ����
                Inventory_Controller.g_ICinstance.g_iclick_Item_Count = 0; // Ŭ���ߴ� ������ ���� ����
            }
            else if (Inventory_Controller.g_ICinstance.g_Iclick_Item != null && m_Sslot.g_Ihave_item != null) // �������� ������ ���¿��� ������� ���� ������ Ŭ������ ��
            {
                Item temp = Inventory_Controller.g_ICinstance.g_Iclick_Item; // Ŭ���ߴ� ������ ������ �ӽ� ������ �Ҵ�
                Inventory_Controller.g_ICinstance.g_Iclick_Item = m_Sslot.g_Ihave_item; // Ŭ���ߴ� ������ ������ ���� Ŭ���� ������ ���� �Ҵ�
                m_Sslot.g_Ihave_item = temp; // ���� Ŭ���� ���Կ� Ŭ���ߴ� ������ ������ ������ �ִ� �ӽú����� �Ҵ�

                int temp_count = Inventory_Controller.g_ICinstance.g_iclick_Item_Count; // Ŭ���ߴ� ������ ������ ���� ���� �ӽ� ������ �Ҵ�
                Inventory_Controller.g_ICinstance.g_iclick_Item_Count = m_Sslot.item_Number; //Ŭ���ߴ� ������ ������ ���� ������ ���� Ŭ���� ���� ������ ���� �Ҵ�
                m_Sslot.item_Number = temp_count; // ���� Ŭ���� ���� ������ ������ �ӽ� ���� �� �Ҵ�

                temp = null; // �ӽ� ����
                temp_count = 0; // �ʱ�ȭ
            }
        }    
    }

    public void Show_Image(Item Clicked_Obj) // �κ��丮�� �������� �������� �� �̹����� ���콺�� ����ٴϰ� ����
    {
        if (Clicked_Obj ==null) // �̹����� Ŭ������ �ʾҴٸ�
        {
            g_ishow_Item_Image.gameObject.SetActive(false); // ���콺�� ����ٴϴ� �̹��� ����

        }
        else if (Clicked_Obj!=null) // �̹����� Ŭ���ߴٸ�
        {
            g_ishow_Item_Image.gameObject.SetActive(true); // ���콺�� ����ٴϴ� �̹��� Ȱ��ȭ
        }
    }

    public void Mouse_Follow(Image show_Image) // ���콺 ����ٴϴ� ������Ʈ
    {
        show_Image.sprite = Inventory_Controller.g_ICinstance.g_Iclick_Item.item_Image; // ������ �̹����� ������ ������Ʈ �̹��� �Ҵ�

        Vector3 mouseScreenPosition = Input.mousePosition;
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, mouseScreenPosition, null, out localPoint);
        show_Image.transform.localPosition = new Vector2(localPoint.x - 30, localPoint.y + 30);
    }

    public void Off_Inven()
    {
        if (m_ISr_S != null)
        {
            m_Sslot.item_Number = m_inum;
            m_Sslot.g_Ihave_item = m_ISr_S;
            m_inum = 0;
            m_ISr_S = null;
            Inventory_Controller.g_ICinstance.g_Iclick_Item = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
      //  print(m_Sslot.g_Ihave_item.name);
        //
        if (Inventory_Controller.g_ICinstance.g_Iclick_Item != null) // ���õ� �������� �ִٸ�
        {
            Show_Image(Inventory_Controller.g_ICinstance.g_Iclick_Item); // �Լ��� Ŭ���� ������ �Ҵ�
            Mouse_Follow(g_ishow_Item_Image); // �Լ��� ���콺�� ����ٴ� �̹��� ���� �Ҵ�
        }
        else // ���õ� �������� ���ٸ�
        {
            g_ishow_Item_Image.sprite = null; // ���콺�� ����ٴ� �̹��� ���� �ʱ�ȭ
            g_ishow_Item_Image.gameObject.SetActive(false); // ���콺�� ����ٴ� �̹��� ���� ��
            m_inum = 0;
            m_ISr_S = null;
        }
    }
}
