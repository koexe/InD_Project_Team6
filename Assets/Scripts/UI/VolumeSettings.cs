using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    // ����� �ͼ��� �����̴� ������Ʈ�� ���� ���� ������
    [SerializeField] private AudioMixer m_myMixer;
    [SerializeField] private Slider m_sliderMusic;
    [SerializeField] private Slider m_sliderSFX;

    // ���� ���� �� ����Ǵ� �޼���
    private void Start()
    {
        // PlayerPrefs�� "musicVolume" Ű�� ����Ǿ� �ִ��� Ȯ��
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();// ����� ���� ������ ������ ���� �ҷ���
        }
        else
        {
            // ����� ���� ������ ������ �⺻ ���������� ���� �� SFX ���� ����
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    // ���� ������ �����ϴ� �޼���
    public void SetMusicVolume()
    {

        float volume = m_sliderMusic.value; // �����̴����� ���� ���� ���� ������
        m_myMixer.SetFloat("music", Mathf.Log10(volume) * 20);    // �ͼ��� "music" �Ķ���Ϳ� �α� �����Ϸ� ��ȯ�� ���� �� ����
        PlayerPrefs.SetFloat("musicVolume", volume); // PlayerPrefs�� ���� ���� ���� ����
    }

    // SFX ������ �����ϴ� �޼���
    public void SetSFXVolume()
    {
        float volume = m_sliderSFX.value; // �����̴����� SFX ���� ���� ������
        m_myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);// �ͼ��� "SFX" �Ķ���Ϳ� �α� �����Ϸ� ��ȯ�� ���� �� ����
        PlayerPrefs.SetFloat("SFXVolume", volume);// PlayerPrefs�� SFX ���� ���� ����
    }

    // ����� ������ �ε��ϴ� �޼���
    private void LoadVolume()
    {
        // PlayerPrefs���� ����� ���� �� SFX ���� ���� �ε�
        m_sliderMusic.value = PlayerPrefs.GetFloat("musicVolume");
        m_sliderSFX.value = PlayerPrefs.GetFloat("SFXVolume");

        // �ε�� ������ �ͼ��� ����
        SetMusicVolume();
        SetSFXVolume();
    }
}

