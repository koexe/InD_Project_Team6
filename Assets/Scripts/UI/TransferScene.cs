using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스 추가

public class TransferScene : MonoBehaviour
{
    public string targetSceneName; // 이동할 씬 이름

    // 박스 콜라이더에 닿는 순간 이벤트 발생
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 씬 이동
        SceneManager.LoadScene(targetSceneName);
    }
}