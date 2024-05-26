using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform[] portals; // ������ Transform �迭
    public MapController mapController; // MapController ����
    public GameObject optionImage; // �ɼ� �̹��� GameObject

    public float activationDistance = 2f; // �÷��̾ ���а��� Ȱ��ȭ �Ÿ�

   // bool optionImageActive = false; // �ɼ� �̹����� Ȱ��ȭ ����

    void Update()
    {
        // �÷��̾ ���� ��ó�� �ְ� �����̽��ٸ� ������ ��
        if (IsPlayerNearPortal() && Input.GetKeyDown(KeyCode.Space))
        {
            if (optionImage.activeSelf == false)
            {
                optionImage.SetActive(true);
                // GameManager�� ���¸� PAUSE�� ����
                GameManager.Instance.SetGameState(GameManager.GameState.PORTAL);


                // �� ��Ʈ�ѷ� ��Ȱ��ȭ
                ToggleMapController(false);

                // �ɼ� �̹��� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ
                if (GameManager.Instance.GetInventoryGO().transform.localScale == new Vector3(1, 1, 1))
                    GameManager.Instance.GetInventoryGO().transform.localScale = new Vector3(0, 0, 1);

                for (int i = 0; i < GameManager.Instance.m_UnitManager.CheckUnitAmount(); i++)
                {
                    GameManager.Instance.m_UnitManager.g_PlayerUnits[i].GetComponent<UnitEntity>().ResetUnit();
                }

                
            }
            else if (optionImage.activeSelf == true)
            {
                optionImage.SetActive(false);

                // GameManager�� ���¸� �ٽ� INPROGRESS�� ���� 
                GameManager.Instance.SetGameState(GameManager.GameState.INPROGRESS);
                
            }
        }
    }


    // �÷��̾ ���� ��ó�� �ִ��� Ȯ���ϴ� �޼���
    bool IsPlayerNearPortal()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            foreach (Transform portal in portals)
            {
                if (Vector3.Distance(playerObject.transform.position, portal.position) <= activationDistance)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // ���з� �÷��̾ �̵���Ű�� �޼���
    public void MovePlayerToPortal(int portalIndex)
    {
        if (portalIndex >= 0 && portalIndex < portals.Length)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                playerObject.transform.position = portals[portalIndex].position;
                ToggleMapController(false);
                optionImage.SetActive(false);
                GameManager.Instance.g_GameState = GameManager.GameState.INPROGRESS;
            }
        }
        else
        {
            Debug.LogError("Invalid portal index: " + portalIndex);
        }
    }

    // UI ��ư���� ȣ���� �Լ�
    public void ToggleMapController(bool mapActive)
    {
        if (mapController != null)
        {
            mapController.gameObject.SetActive(mapActive);
        }
    }

    public void ShowDic()
    {
        GameManager.Instance.GetDictionaryGO().SetActive(true);
        optionImage.SetActive(false);
    }

}
