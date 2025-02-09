
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG; // Добавляем using для YandexGame

public class text : MonoBehaviour
{
    [System.Serializable]
    public class TranslatedLines
    {
        public string[] ru; // Русский текст
        public string[] en; // Английский текст
        public string[] tr; // Турецкий текст
    }

    public TranslatedLines translatedLines; // Массивы строк для разных языков
    private string[] currentLines; // Текущий набор строк в зависимости от языка

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
        // Выбираем нужный массив строк в зависимости от текущего языка
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
                currentLines = translatedLines.en; // По умолчанию используем английский
                break;
        }

        // Если диалог уже начат, обновляем текущий текст
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
