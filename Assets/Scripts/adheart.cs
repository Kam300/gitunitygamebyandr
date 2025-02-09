using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; // Добавляем для работы с UI

public class adheart : MonoBehaviour
{
    public Move player; // Ссылка на скрипт игрока
    public Button button; // Ссылка на кнопку, которую нужно активировать
    public Button button2; // Ссылка на кнопку, которую нужно активировать

    // Start is called before the first frame update
    void Start()
    {
        // Проверяем, что ссылки на игрока и кнопку установлены
        if (player == null)
        {
            Debug.LogError("Player reference is missing!");
        }
        if (button == null)
        {
            Debug.LogError("Button reference is missing!");
        }

        // Изначально кнопка неактивна
        button.gameObject.SetActive(false);
         button2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Показываем кнопку восстановления здоровья только если:
            // 1. Здоровье меньше 2
            // 2. Флаг ADButtonHP активен
            // 3. Игрок не в состоянии проигрыша
            if (player.curHp < 2 && player.ADButtonHP)
            {
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }

            // Показываем кнопку пуль только если:
            // 1. Количество пуль меньше 1
            // 2. Флаг bull активен
            // 3. Флаг ADButtonBl активен
            // 4. Игрок не в состоянии проигрыша
            if (player.bulletCount < 1 && player.bull && player.ADButtonBl)
            {
                button2.gameObject.SetActive(true);
            }
            else
            {
                button2.gameObject.SetActive(false);
            }
        }
    }


}