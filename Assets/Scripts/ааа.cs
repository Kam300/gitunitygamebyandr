using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG.Insides;
using YG;

public class ааа : MonoBehaviour
{
    public GameObject canvasObject;
    public GameObject Game;
    public OpenPanel openPanel; // Добавьте ссылку на OpenPanel

    void Start()
    {
        openPanel = FindObjectOfType<OpenPanel>();
    }

    public cheste Cheste;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int Coins = Cheste.GetCoins();
        if (collision.CompareTag("Player")&& Coins<80) // Проверяем, столкнулся ли с объектом с тегом "Player" и не входил ли в триггер ранее
        {
            OnNolevelClicked();
            ActivateCanvas(); // Вызываем метод для активации Canvas
        }
        else
        {
            OnlevelClicked();
            Destroy(Game);
            // Существующий код выигрыша
            if (openPanel != null)
            {
                openPanel.OnLevelComplete();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Проверяем, вышел ли игрок из триггера
        {
            DeactivateCanvas(); // Вызываем метод для деактивации Canvas
        }
    }

    private void ActivateCanvas()
    {
        canvasObject.SetActive(true); // Активируем объект Canvas
    }

    private void DeactivateCanvas()
    {
        canvasObject.SetActive(false); // Деактивируем объект Canvas
    }

    public void OnlevelClicked()
    {
        // Имя события, которое будет отправлено в Яндекс.Метрику
        string eventName = "trigger";

        // Дополнительные параметры, которые можно передать с событием
        var eventParams = new Dictionary<string, string>
        {
             { "level",  "Уровень пройден" }, // Номер уровня
          
        };

        // Отправка события в Яндекс.Метрику
      //  YandexMetrica.Send(eventName, eventParams);

        // Дополнительно: можно добавить логирование для отладки в редакторе
        Debug.Log($"Level start button clicked. Event: {eventName}, Params: {JsonUtils.ToJson(eventParams)}");
    }

    public void OnNolevelClicked()
    {
        // Имя события, которое будет отправлено в Яндекс.Метрику
        string eventName = "trigger";

        // Дополнительные параметры, которые можно передать с событием
        var eventParams = new Dictionary<string, string>
        {
             { "level",  "Уровень не пройден(огоньков не достаточно)" }, // Номер уровня
          
        };

        // Отправка события в Яндекс.Метрику
     //   YandexMetrica.Send(eventName, eventParams);

        // Дополнительно: можно добавить логирование для отладки в редакторе
        Debug.Log($"Level start button clicked. Event: {eventName}, Params: {JsonUtils.ToJson(eventParams)}");
    }
}
