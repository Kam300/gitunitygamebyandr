using UnityEngine;
using UnityEngine.UI;
using YG;

public class TriggerCanvas : MonoBehaviour
{
    public GameObject canvasObject;
    public GameObject canvasObject2;// Ссылка на объект Canvas
    public AudioClip melody; // Ссылка на аудио-клип мелодии
    private AudioSource audioSource; // Ссылка на AudioSource компонент
// Флаг, указывающий, входил ли игрок в триггер

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Получаем ссылку на AudioSource компонент
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") ) // Проверяем, столкнулся ли с объектом с тегом "Player" и не входил ли в триггер ранее
        {
            ActivateCanvas(); // Вызываем метод для активации Canvas
            PlayMelody(); // Вызываем метод для проигрывания мелодии
           // Устанавливаем флаг, указывающий, что игрок уже входил в триггер
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
        if (YandexGame.EnvironmentData.language == "en")
        {
            canvasObject.SetActive(true); // Активируем объект Canvas
        }
        else
        {
            canvasObject2.SetActive(true);
        }
       
    }

    private void DeactivateCanvas()
    {
        if (YandexGame.EnvironmentData.language == "en")
        {
            canvasObject.SetActive(false); // Активируем объект Canvas
        }
        else
        {
            canvasObject2.SetActive(false);
        }
    }

    private void PlayMelody()
    {
        audioSource.clip = melody; // Устанавливаем аудио-клип мелодии
        audioSource.Play(); // Запускаем проигрывание мелодии
    }


}
