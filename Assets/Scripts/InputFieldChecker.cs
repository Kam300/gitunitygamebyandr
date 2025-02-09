using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG.Insides;
using YG;

public class InputFieldChecker : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    public TMP_InputField inputField3;
    public AudioClip failureSound;
    public string failureText;
    public GameObject Door;
    public GameObject Door1;
    public Canvas canvas;
  
   public void CheckInputFields()
    {
        // Получаем значения из input fields
        int value1 = int.Parse(inputField1.text);
        int value2 = int.Parse(inputField2.text);
        int value3 = int.Parse(inputField3.text);

        // Складываем значения
        string a = Convert.ToString(value1) + Convert.ToString(value2) + Convert.ToString(value3);
        Debug.Log((a));
       

        // Проверяем, равно ли значение 666
        if (a== failureText)
        {
            DoSomething();
        }
        else
        {
            PlayFailureSound();
        }
    }

    void DoSomething()
    {
        // Здесь вы можете написать код для выполнения какого-либо действия
        Debug.Log("Значение равно 666!");
        OnDoorButtonClicked();
        Destroy(Door);
        Destroy(Door1);
        canvas.gameObject.SetActive(false);
    }

    void PlayFailureSound()
    {
        // Воспроизводим звук неудачи
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.PlayOneShot(failureSound);
        }
        else
        {
            Debug.LogError("AudioSource not found on the same GameObject as the InputFieldChecker script.");
        }
    }

    public void OnDoorButtonClicked()
    {
        // Имя события, которое будет отправлено в Яндекс.Метрику
        string eventName = "trigger";

        // Дополнительные параметры, которые можно передать с событием
        var eventParams = new Dictionary<string, string>
    {
        { "action",  "дверь к кнопке 2 открыта"}, // Номер уровня
      
    };

        // Отправка события в Яндекс.Метрику
       // YandexMetrica.Send(eventName, eventParams);

        // Дополнительно: можно добавить логирование для отладки в редакторе
        Debug.Log($"Level start button clicked. Event: {eventName}, Params: {JsonUtils.ToJson(eventParams)}");
    }
}
