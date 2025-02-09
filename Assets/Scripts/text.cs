
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG; // ��������� using ��� YandexGame

public class text : MonoBehaviour
{
    [System.Serializable]
    public class TranslatedLines
    {
        public string[] ru; // ������� �����
        public string[] en; // ���������� �����
        public string[] tr; // �������� �����
    }

    public TranslatedLines translatedLines; // ������� ����� ��� ������ ������
    private string[] currentLines; // ������� ����� ����� � ����������� �� �����

    public float speedText;
    public Text dialogText;
    public int index;
    public bool DA;
    public AudioSource musicSource;
    public string a;

    void Start()
    {
        UpdateLanguage();
        index = 0;
        StartDialog();
    }

    private void UpdateLanguage()
    {
        // �������� ������ ������ ����� � ����������� �� �������� �����
        switch (YandexGame.EnvironmentData.language)
        {
            case "ru":
                currentLines = translatedLines.ru;
                break;
            case "en":
                currentLines = translatedLines.en;
                break;
            case "tr":
                currentLines = translatedLines.tr;
                break;
            default:
                currentLines = translatedLines.en; // �� ��������� ���������� ����������
                break;
        }

        // ���� ������ ��� �����, ��������� ������� �����
        if (dialogText != null && index < currentLines.Length)
        {
            StopAllCoroutines();
            dialogText.text = currentLines[index];
        }
    }

    void StartDialog()
    {
        dialogText.text = string.Empty;
        StartCoroutine(TypeLine());
        if (musicSource != null)
        {
            musicSource.Play();
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in currentLines[index].ToCharArray())
        {
            dialogText.text += c;
            yield return new WaitForSeconds(speedText);
        }

        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    public void scipTextClick()
    {
        if (dialogText.text == currentLines[index])
        {
            NextLines();
        }
        else
        {
            StopAllCoroutines();
            dialogText.text = currentLines[index];
            if (musicSource != null)
            {
                musicSource.Stop();
            }
        }
    }

    public void NextLines()
    {
        if (index < currentLines.Length - 1)
        {
            index++;
            StartDialog();
        }
        else
        {
            if (musicSource != null)
            {
                musicSource.Stop();
            }
            SceneManager.LoadScene(a);
        }
    }
}
