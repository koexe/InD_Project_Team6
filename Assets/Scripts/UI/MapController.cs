using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        // �߰����� ���� ������ ���ϴ´�� ������ �� ����
    }

    public List<RegionInfo> regions = new List<RegionInfo>(); // ���� ���� ����Ʈ
    public TextMeshProUGUI mapNameText; // ���� �̸��� ǥ���� UI �ؽ�Ʈ

    private string currentMapName = ""; // ���� �÷��̾ ��ġ�� ���� �̸�

    void Update()
    {
        // �� ��ġ ������Ʈ
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

        // �÷��̾ � ������ ���� �ִ��� Ȯ���Ͽ� ���� �̸� ������Ʈ
        UpdateMapName();
    }

    // ���� �̸� ������Ʈ �Լ�
    void UpdateMapName()
    {
        // �÷��̾ � ������ ���� �ִ��� ����
        bool isInAnyRegion = false;

        // �÷��̾ � ������ ���� �ִ��� Ȯ��
        foreach (RegionInfo region in regions)
        {
            // �÷��̾��� ��ġ�� ���� ���� �ִ��� �ڽ� �ݶ��̴��� ����Ͽ� Ȯ��
            if (region.regionCollider != null && region.regionCollider.bounds.Contains(player.position))
            {
                // �÷��̾ �ٸ� �������� �̵��� ��쿡�� ���� �̸� ������Ʈ
                if (currentMapName != region.mapName)
                {
                    // ���� �̸� ������Ʈ
                    mapNameText.text = region.mapName;
                    currentMapName = region.mapName; // ���� ���� �̸� ������Ʈ
                }
                isInAnyRegion = true; // �÷��̾ � ������ ���� ������ ǥ��
            }
        }

        // �÷��̾ � ������ ���� ���� ������ ���� �̸��� ���
        if (!isInAnyRegion)
        {
            mapNameText.text = "";
            currentMapName = ""; // ���� ���� �̸� �ʱ�ȭ
        }
    }
}
