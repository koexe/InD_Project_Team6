using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeSceneTimer : MonoBehaviour
{
    public float changeTime;
    public string sceneName;
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

            // �� ����
            SceneManager.LoadScene("WorldScene");
        }
    }
}
