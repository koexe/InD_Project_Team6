using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Transform player; // 플레이어 Transform
    public RectTransform mapRect; // 지도 UI Rect Transform
    public RectTransform playerIcon; // 플레이어 아이콘 UI Rect Transform

    // 게임 맵의 크기 (예를 들어, 가로 200, 세로 200)
    public Vector2 mapSize = new Vector2(200, 200);

    void Update()
    {
        // 'M' 키를 눌렀을 때 맵을 보이거나 숨기도록 처리
        if (Input.GetKeyDown(KeyCode.M))
        {
            // 현재 맵이 보이는 상태면 맵을 숨김
            mapRect.gameObject.SetActive(!mapRect.gameObject.activeSelf);
        }

        // 맵이 보이는 상태일 때만 맵 위치 업데이트
        if (mapRect.gameObject.activeSelf)
        {
            // 플레이어의 위치를 게임 맵의 좌표로 변환
            Vector2 mapPosition = new Vector2(
                Mathf.Clamp(player.position.x, -mapSize.x, mapSize.x), // x 좌표 제한
                Mathf.Clamp(player.position.y, -mapSize.y, mapSize.y) // y 좌표 제한
            );

            // 게임 맵의 좌표를 UI 지도의 좌표로 변환
            Vector2 normalizedPosition = new Vector2(
                mapPosition.x / mapSize.x,
                mapPosition.y / mapSize.y
            );

            // UI 지도의 크기에 맞게 변환
            Vector2 mapRectSize = mapRect.rect.size;
            Vector2 mapPositionPixels = new Vector2(
                normalizedPosition.x * mapRectSize.x,
                normalizedPosition.y * mapRectSize.y
            );

            // UI 지도 내에서 플레이어 아이콘의 위치 조정
            playerIcon.anchoredPosition = mapPositionPixels;
        }
    }
}