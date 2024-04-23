using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform[] portals; // ��Ż���� Transform �迭

    // �� �޼���� ��ư���� ȣ��� ���Դϴ�.
    public void MovePlayerToPortal(int portalIndex)
    {
        // ������ �ε����� �ش��ϴ� ��Ż ��ġ�� �÷��̾ �̵���Ŵ
        if (portalIndex >= 0 && portalIndex < portals.Length)
        {
            // 'Player' �±׸� ���� ���� ������Ʈ�� ã��
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                // �÷��̾��� ��ġ�� ������ ��Ż�� ��ġ�� �̵���Ŵ
                playerObject.transform.position = portals[portalIndex].position;
            }
        }
        else
        {
            // �߸��� ��Ż �ε����� ���� ���� �޽��� ���
            Debug.LogError("Invalid portal index: " + portalIndex);
        }
    }
}
