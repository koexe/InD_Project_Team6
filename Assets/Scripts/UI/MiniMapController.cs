using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public Transform player; // �÷��̾� Transform
    public RectTransform mapRect; // ���� UI Rect Transform
    public RectTransform playerIcon; // �÷��̾� ������ UI Rect Transform
    public BoxCollider2D regionCollider; // ���� ������ �ڽ� �ݶ��̴�
    public RectTransform potalIcon;

    // �̴ϸ��� ����
    public float miniMapMinX = -30f;
    public float miniMapMaxX = 30f;
    public float miniMapMinY = -30f;
    public float miniMapMaxY = -70f;

    void Update()
    {
        if (regionCollider != null)
        {
            // ���� ������ �ڽ� �ݶ��̴��� �������� �÷��̾��� ��ġ�� ����
            Vector2 mapPosition = new Vector2(
                Mathf.Clamp(player.position.x, regionCollider.bounds.min.x, regionCollider.bounds.max.x),
                Mathf.Clamp(player.position.y, regionCollider.bounds.min.y, regionCollider.bounds.max.y)
            );

            // ���� ���� ��ǥ�� UI ������ ��ǥ�� ��ȯ
            Vector2 normalizedPosition = new Vector2(
                Mathf.InverseLerp(regionCollider.bounds.min.x, regionCollider.bounds.max.x, mapPosition.x),
                Mathf.InverseLerp(regionCollider.bounds.min.y, regionCollider.bounds.max.y, mapPosition.y)
            );

            // UI ������ ũ�⿡ �°� ��ȯ
            Vector2 mapRectSize = mapRect.rect.size;
            Vector2 mapPositionPixels = new Vector2(
                Mathf.Lerp(miniMapMinX, miniMapMaxX, normalizedPosition.x),
                Mathf.Lerp(miniMapMinY, miniMapMaxY, 1f - normalizedPosition.y) // ���⼭ ����
            );

            // UI ���� ������ �÷��̾� �������� ��ġ ����
            playerIcon.anchoredPosition = mapPositionPixels;
        }
    }
}
