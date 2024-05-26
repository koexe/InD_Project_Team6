using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeSceneTimer : MonoBehaviour
{
    public float changeTime;
    public string mainSceneName;
    public string worldSceneName;
    public string cutSceneName;
    public string cutScene2Name;
    public AudioSource bgmAudioSource; // BGM�� ����ϴ� AudioSource

    private void Start()
    {
        // ���� BGM AudioSource�� null�̸�, ���� ���� ������Ʈ���� ã�ƺ��ϴ�.
        if (bgmAudioSource == null)
        {
            bgmAudioSource = GetComponent<AudioSource>();
        }

        // BGM ��� ����
        bgmAudioSource.Play();
    }

    private void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0)
        {
            // BGM ����
            bgmAudioSource.Stop();

            string currentSceneName = SceneManager.GetActiveScene().name;

            // ���� ���� CutScene�� ���
            if (currentSceneName == cutSceneName)
            {
                // WorldScene���� ��ȯ
                SceneManager.LoadScene(worldSceneName);
            }
            // ���� ���� CutScene2�� ���
            else if (currentSceneName == cutScene2Name)
            {
                // MainScene���� ��ȯ
                SceneManager.LoadScene(mainSceneName);
            }
        }
    }
}
