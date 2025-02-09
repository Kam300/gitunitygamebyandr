using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using YG;

public class TriggerCanvas2 : MonoBehaviour
{
    public GameObject canvasObject;
    public GameObject canvasObject2;// Ссылка на объект Canvas
    public GameObject bulet;
    public GameObject joystick;
    public GameObject check;
    public AudioClip melody; // Ссылка на аудио-клип мелодии
    private AudioSource audioSource; // Ссылка на AudioSource компонент
                                     // Флаг, указывающий, входил ли игрок в триггер
    public bool doormobil = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Получаем ссылку на AudioSource компонент
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Проверяем, столкнулся ли с объектом с тегом "Player" и не входил ли в триггер ранее
        {
            ActivateCanvas(); // Вызываем метод для активации Canvas
            check.SetActive(true);
           
            PlayMelody(); // Вызываем метод для проигрывания мелодии
                          // Устанавливаем флаг, указывающий, что игрок уже входил в триггер
          
        }

            
       
    }
   


    private void OnTriggerExit2D(Collider2D collision)
    {
        doormobil = false;
        if (collision.CompareTag("Player")) // Проверяем, вышел ли игрок из триггера
        {
            check.SetActive(false);
            
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
