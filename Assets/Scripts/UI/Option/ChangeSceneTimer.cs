using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeSceneTimer : MonoBehaviour
{
    public float changeTime;
    public string mainSceneName;
    public string worldSceneName;
    public string cutSceneName;
    public string cutScene2Name;
    public AudioSource bgmAudioSource; // BGM을 재생하는 AudioSource

    private void Start()
    {
        // 만약 BGM AudioSource가 null이면, 현재 게임 오브젝트에서 찾아봅니다.
        if (bgmAudioSource == null)
        {
            bgmAudioSource = GetComponent<AudioSource>();
        }

        // BGM 재생 시작
        bgmAudioSource.Play();
    }

    private void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0)
        {
            // BGM 정지
            bgmAudioSource.Stop();

            string currentSceneName = SceneManager.GetActiveScene().name;

            // 현재 씬이 CutScene일 경우
            if (currentSceneName == cutSceneName)
            {
                // WorldScene으로 전환
                SceneManager.LoadScene(worldSceneName);
            }
            // 현재 씬이 CutScene2일 경우
            else if (currentSceneName == cutScene2Name)
            {
                // MainScene으로 전환
                SceneManager.LoadScene(mainSceneName);
            }
        }
    }
}
