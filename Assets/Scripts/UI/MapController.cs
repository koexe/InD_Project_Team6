using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public Transform player; // �÷��̾� Transform
    public RectTransform mapRect; // ���� UI Rect Transform
    public RectTransform playerIcon; // �÷��̾� ������ UI Rect Transform
    public PortalController portalController; // ��Ż ��Ʈ�ѷ�

    // ���� ���� ũ�� (���� ���, ���� 200, ���� 200)
    public Vector2 mapSize = new Vector2(200, 200);

    // ���� ���� Ŭ����
    [System.Serializable]
    public class RegionInfo
    {
        public string mapName; // ���� �̸�
        public BoxCollider2D regionCollider; // ������ ��Ÿ���� �ڽ� �ݶ��̴�
        public GameObject miniMap; // �ش� ������ �̴ϸ�
        // �߰����� ���� ������ ���ϴ´�� ������ �� ����
    }

    public List<RegionInfo> regions = new List<RegionInfo>(); // ���� ���� ����Ʈ
    public Text mapNameText; // ���� �̸��� ǥ���� UI �ؽ�Ʈ

    private string currentMapName = ""; // ���� �÷��̾ ��ġ�� ���� �̸�

    void Update()
    {
        // 'M' Ű�� ������ �� ���� ���̰ų� ���⵵�� ó��
        if (Input.GetKeyDown(KeyCode.M))
        {
            // ���� ���� ���̴� ���¸� ���� ����
            mapRect.gameObject.SetActive(!mapRect.gameObject.activeSelf);
        }

        // ���� ���̴� ������ ���� �� ��ġ ������Ʈ
        if (mapRect.gameObject.activeSelf)
        {
            // �÷��̾��� ��ġ�� ���� ���� ��ǥ�� ��ȯ
            Vector2 mapPosition = new Vector2(
                Mathf.Clamp(player.position.x, -mapSize.x / 2f, mapSize.x / 2f), // x ��ǥ ����
                Mathf.Clamp(player.position.y, -mapSize.y / 2f, mapSize.y / 2f) // y ��ǥ ����
            );

            // ���� ���� ��ǥ�� UI ������ ��ǥ�� ��ȯ
            Vector2 normalizedPosition = new Vector2(
                mapPosition.x / mapSize.x,
                mapPosition.y / mapSize.y
            );

            // UI ������ ũ�⿡ �°� ��ȯ
            Vector2 mapRectSize = mapRect.rect.size;
            Vector2 mapPositionPixels = new Vector2(
                normalizedPosition.x * mapRectSize.x,
                normalizedPosition.y * mapRectSize.y
            );

            // UI ���� ������ �÷��̾� �������� ��ġ ����
            playerIcon.anchoredPosition = mapPositionPixels;

            // �÷��̾ � ������ ���� �ִ��� Ȯ���Ͽ� ���� �̸� ������Ʈ �� �̴ϸ� ǥ��
            UpdateMapAndMiniMap();
        }
    }

    // ���� �̸� ������Ʈ �� �̴ϸ� ǥ�� �Լ�
    void UpdateMapAndMiniMap()
    {
        bool isInAnyRegion = false; // �÷��̾ � ������ ���� �ִ��� ����

        // �÷��̾ � ������ ���� �ִ��� Ȯ��
        foreach (RegionInfo region in regions)
        {
            // �÷��̾��� ��ġ�� ���� ���� �ִ��� �ڽ� �ݶ��̴��� ����Ͽ� Ȯ��
            if (region.regionCollider != null && region.regionCollider.bounds.Contains(player.position))
            {
                // �÷��̾ �ٸ� �������� �̵��� ��쿡�� ���� �̸� �� �̴ϸ��� ������Ʈ
                if (currentMapName != region.mapName)
                {
                    // ���� �̸� ������Ʈ
                    mapNameText.text = region.mapName;

                    // �̴ϸ� ǥ��
                    ShowMiniMap(region.miniMap);

                    currentMapName = region.mapName; // ���� ���� �̸� ������Ʈ
                }
                isInAnyRegion = true; // �÷��̾ � ������ ���� ������ ǥ��
            }
            else
            {
                // �÷��̾ �� ������ ���� ���� ������ �̴ϸ��� ����
                HideMiniMap(region.miniMap);
            }
        }

        // �÷��̾ � ������ ���� ���� ������ ���� �̸��� ���� ��� �̴ϸ��� ����
        if (!isInAnyRegion)
        {
            mapNameText.text = "";
            currentMapName = ""; // ���� ���� �̸� �ʱ�ȭ
        }
    }

    // �̴ϸ� ǥ�� �Լ�
    void ShowMiniMap(GameObject miniMap)
    {
        // Ư�� �̴ϸ��� ������
        if (miniMap != null)
        {
            miniMap.SetActive(true);
        }
    }

    // Ư�� �̴ϸ��� ����� �Լ�
    void HideMiniMap(GameObject miniMap)
    {
        if (miniMap != null)
        {
            miniMap.SetActive(false);
        }
    }
}
