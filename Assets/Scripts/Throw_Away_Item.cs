using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw_Away_Item : MonoBehaviour
{
    public void Throw_Item() // �κ��丮���� Ŭ���� ������ ����
    {
        if (Inventory_Controller.g_ICinstance.g_Iclick_Item != null)
        {
            Inventory_Controller.g_ICinstance.g_Iclick_Item = null;
            Inventory_Controller.g_ICinstance.g_iclick_Item_Count = 0;
        }
    }
}
