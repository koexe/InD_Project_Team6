using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransferMap : MonoBehaviour
{
    public Vector3 targetPosition; // �̵��� ��ġ ��ǥ

    public Image blackImage; // ���� ������ �ҷ����� ���� �̹���

    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;
    private bool imageActive = false; // ������ Ȱ��ȭ�Ǿ����� ���θ� �����ϴ� ����

    // �ڽ� �ݶ��̴��� ��� ���� �̺�Ʈ �߻�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾� ��ġ �̵�
        collision.gameObject.transform.position = targetPosition;

        // ���� ������ ��� Ȱ��ȭ�ϰ� ������ ������� �ڷ�ƾ ����
        StartCoroutine(FadeOutImage());
    }

    // ���� ������ ��� Ȱ��ȭ�ϰ� ������ ������� �ϴ� �ڷ�ƾ
    IEnumerator FadeOutImage()
    {
        // �̹����� �̹� Ȱ��ȭ�Ǿ� �ִ� ��� �ߺ��ؼ� ó������ �ʵ��� �մϴ�.
        if (imageActive)
            yield break;

        // ���� ������ Ȱ��ȭ�մϴ�.
        blackImage.gameObject.SetActive(true);
        imageActive = true;

        // ���� ���� ������ ���� ���̵� �ƿ� ȿ�� ����
        for (float i = 1; i >= 0; i -= Time.deltaTime / fadeTime)
        {
            // ���� ������ ���� ���� �����Ͽ� ������� �մϴ�.
            blackImage.color = new Color(0, 0, 0, i);
            yield return null;
        }

        // ������� ȿ���� ���� �� ���� ������ ��Ȱ��ȭ�Ͽ� ȭ�鿡�� ����ϴ�.
        blackImage.gameObject.SetActive(false);
        imageActive = false;
    }
}
