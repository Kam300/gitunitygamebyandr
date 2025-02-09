using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG.Insides;
using YG;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    private bool n = true;
    public new GameObject[] gameObject = new GameObject[3];
    private GameObject door;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();

        }
        // Настраиваем AudioSource
        audioSource.playOnAwake = false;
        audioSource.volume = 1f; // Установите нужную громкость
    }

    public void Openstena(GameObject stena)
    {
        stena.SetActive(false);
        if (n)
        {
            OnDoorButtonClicked();

            // Проверяем наличие компонентов звука перед воспроизведением
            if (audioSource != null && audioClip != null)
            {
                audioSource.volume = 1f; // Убедитесь, что громкость не на нуле
                audioSource.PlayOneShot(audioClip);
                Debug.Log("Playing door sound"); // Добавляем лог для отладки
            }
            else
            {
                Debug.LogWarning("AudioSource или AudioClip не назначены!");
            }

            n = false;
            for (int i = 0; i < gameObject.Length; i++)
            {
                Destroy(gameObject[i]);
            }

            door = GameObject.FindGameObjectWithTag("doorr");
            if (door != null)
            {
                Destroy(door);
            }
        }
    }
    public void OnDoorButtonClicked()
    {
        // Имя события, которое будет отправлено в Яндекс.Метрику
        string eventName = "trigger";

        // Дополнительные параметры, которые можно передать с событием
        var eventParams = new Dictionary<string, string>
    {
        { "action",  "какаята дверь открыта"}, // Номер уровня
      
    };

        // Отправка события в Яндекс.Метрику
     //   YandexMetrica.Send(eventName, eventParams);

        // Дополнительно: можно добавить логирование для отладки в редакторе
        Debug.Log($"Level start button clicked. Event: {eventName}, Params: {JsonUtils.ToJson(eventParams)}");
    }


}
