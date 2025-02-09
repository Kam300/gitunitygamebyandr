 using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class OpenPanel : MonoBehaviour
{
    // Панель, которую нужно открыть
    public GameObject panel;

    // Кнопка клавиатуры, которая активирует панель

    public Move move;
    //  private Soundeffector soundeffector;
    public AudioSource musicSourse, soundSourse;
    public bool isPaused = false;
    public float timer = 0f;
    public TimeWork timeWork = TimeWork.Timer;
    public Text timeText;
    public GameObject Infopanel;

    public GameObject Pause;

    public bool isTimerPaused = false; // Добавьте это поле

    public bool ab;

    public GameObject tutorial;
    public GameObject cont;
    public bool isAdShowing = false;
    public GameObject buttonpause;

    private void InitializeGameMode()
    {
        timeWork = GameModeManager.selectedGameMode;

        switch (timeWork)
        {
            case TimeWork.Timer:
                timer = 360f; // 6 минут для режима таймера
                break;
            case TimeWork.Stopwatch:
                timer = 0f; // Начинаем с 0 для секундомера
                break;
        }

        UpdateTimerDisplay();
    }
    void Start()
    {

        InitializeGameMode();
        

        if (timeText == null)
        {
            Debug.LogError("TimeText reference is missing!");
        }
        
        if (move == null)
        {
            Debug.LogError("Move reference is missing!");
        }
        
        Debug.Log($"Initial timer value: {timer}");
        Debug.Log($"TimeWork value: {timeWork}");
        
        Infopanel.SetActive(true);
        Pause.SetActive(false);
        Time.timeScale = 0f;
        move.enabled = false;
        panel.SetActive(false);
        
        if (musicSourse != null) musicSourse.Stop();
        if (soundSourse != null) soundSourse.Stop();
        
        isPaused = false;
        ab = true;
        
        UpdateTimerDisplay();
    }
    private void UpdateTimerDisplay()
    {
        if (timeText != null)
        {
            int minutes = Mathf.Max(0, (int)timer / 60);
            int seconds = Mathf.Max(0, (int)timer % 60);
            timeText.text = $"{minutes}:{seconds:D2}";
          //  Debug.Log($"Timer Display Updated: {timeText.text}, Raw timer value: {timer}");
        }
    }
    private void OnAdStarted()
    {
        isAdShowing = true;
    }

    private void OnAdFinished(bool wasSuccessful)
    {
        isAdShowing = false;
    }

    // Показать туториал
    // Показать туториал
    private void ShowTutorial()
    {
        if (tutorial != null)
        {
            tutorial.SetActive(true); // Показываем туториал
            // Подписываемся на событие закрытия туториала
            var tutorialController = tutorial.GetComponent<TutorialController>();
            if (tutorialController != null)
            {
                tutorialController.OnTutorialClosed += OnTutorialClosed;
            }
            else
            {
                Debug.LogError("TutorialController component is missing on the tutorial object!");
            }
        }
        else
        {
            Debug.LogError("Tutorial reference is missing!");
        }
    }

    // Обработчик события закрытия туториала
    private void OnTutorialClosed()
    {
        MarkTutorialAsShown(); // Помечаем туториал как показанный
        ShowInfopanel(); // Показываем Infopanel
    }

    // Показать Infopanel
    private void ShowInfopanel()
    {
        if (Infopanel != null)
        {
            Infopanel.SetActive(true); // Показываем Infopanel
        }
        else
        {
            Debug.LogError("Infopanel reference is missing!");
        }
    }

    // Проверить, был ли туториал показан
    private bool WasTutorialShown()
    {
        return PlayerPrefs.GetInt("TUTORIAL", 0) == 1;
    }

    // Пометить туториал как показанный
    private void MarkTutorialAsShown()
    {
        PlayerPrefs.SetInt("TUTORIAL", 1);
        PlayerPrefs.Save(); // Сохраняем изменения
    }

    // Сбросить состояние туториала (например, для тестирования)
    public void ResetTutorialState()
    {
        PlayerPrefs.DeleteKey("TUTORIAL");
        Debug.Log("Tutorial state reset.");
    }


    // Добавьте этот метод
    public void ResumeTimer()
    {
        isTimerPaused = false;
        Time.timeScale = 1f;
        UpdateTimerDisplay();
    }
    public void ResetGameState()
    {
        isTimerPaused = false;
        Time.timeScale = 1f;
        if (move != null)
        {
            move.enabled = true;
        }
        if (musicSourse != null) musicSourse.Play();
        if (soundSourse != null) soundSourse.Play();
        UpdateTimerDisplay();
    }


    void Update()
    {
        

        // Обработка таймера/секундомера
        if (!isTimerPaused)
        {
            switch (timeWork)
            {
                case TimeWork.Timer: // Режим таймера
                    if (timer > 0)
                    {
                        timer -= Time.deltaTime;
                        UpdateTimerDisplay();

                        if (timer <= 0)
                        {
                            timer = 0;
                            UpdateTimerDisplay();

                            if (!move.tt)
                            {
                                isTimerPaused = true;
                                move.startPosition = transform.position;
                                move.Lose();
                            }
                            else
                            {
                                move.tt = false;
                            }
                        }
                    }
                    if (timer <= 0)
                    {
                        timer = 0;
                        UpdateTimerDisplay();

                        if (!move.tt)
                        {
                            isTimerPaused = true;
                            move.startPosition = transform.position;
                            move.Lose();
                        }
                        else
                        {
                            move.tt = false;
                        }
                    }
                    break;

                case TimeWork.Stopwatch: // Режим секундомера
                    timer += Time.deltaTime;
                    UpdateTimerDisplay();
                    break;
            }
        }

        if (ab == false)
        {
            panel.SetActive(true);
        }
    }

    public void PauseOn()
    {


        Time.timeScale = 0f;
        move.enabled = false;
        panel.SetActive(true);
        musicSourse.Stop(); // Приостанавливаем проигрывание музыки
        soundSourse.Stop(); // Приостанавливаем проигрывание звуков
        musicSourse.Pause(); // Приостанавливаем проигрывание музыки
        soundSourse.Pause(); // Приостанавливаем проигрывание звуков
        isPaused = false; // устанавливаем состояние паузы

    }

    public void PauseOn0()
    {


        Time.timeScale = 0f;
        move.enabled = false;
      
        musicSourse.Stop(); // Приостанавливаем проигрывание музыки
        soundSourse.Stop(); // Приостанавливаем проигрывание звуков
        musicSourse.Pause(); // Приостанавливаем проигрывание музыки
        soundSourse.Pause(); // Приостанавливаем проигрывание звуков
        isPaused = false; // устанавливаем состояние паузы

    }
    public void PauseOff()
    {
        ResetGameState();
        panel.SetActive(false);
        Pause.SetActive(true);
        Infopanel.SetActive(false);
        isPaused = true;
    }
    public void PauseOf1()
    {
        // Если время равно 1 (игра не на паузе)
        if (isPaused == true)
        {
            isPaused = true;
            ResetGameState();
            panel.SetActive(false);
            move.enabled = true;
            Infopanel.SetActive(false);
        }
        else if (isPaused == false) 
        {
            Time.timeScale = 0f;
            move.enabled = false;
            panel.SetActive(true);
            musicSourse.Stop(); // Приостанавливаем проигрывание музыки
            soundSourse.Stop(); // Приостанавливаем проигрывание звуков
            musicSourse.Pause(); // Приостанавливаем проигрывание музыки
            soundSourse.Pause(); // Приостанавливаем проигрывание звуков
            isPaused = false; // устанавливаем состояние паузы
        }
        // Если время не равно 1 (игра на паузе), ничего не делаем


    }
   
    public void PauseOfffff()
    {
        Time.timeScale = 1f; // Восстанавливаем время
        move.enabled = true; // Включаем движение
        musicSourse.Play(); // Возобновляем проигрывание музыки
        soundSourse.Play(); // Возобновляем проигрывание звуков

        isPaused = true;
        Pause.SetActive(true); // Скрываем "Pause"
        Infopanel.SetActive(false); // Показываем "Info"

    }
    public void PauseOf()
    {
        Time.timeScale = 1f; // Восстанавливаем время
        move.enabled = true; // Включаем движение
        musicSourse.Play(); // Возобновляем проигрывание музыки
        soundSourse.Play(); // Возобновляем проигрывание звуков
        isTimerPaused = false; // Сбрасываем состояние паузы таймера
        UpdateTimerDisplay(); // Обновляем отображение таймера
        isPaused = true;
        // Проверяем, что таймер не равен 0
        if (timer <= 0)
        {
           timer = 120f; // Устанавливаем таймер на 120 секунд
            UpdateTimerDisplay();
           
        }

        // Убедимся, что move.tt не сбрасывается
        move.tt = true;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        move.enabled = true;

    }
    private void SendScoreToLeaderboard()
    {
        if (timeWork == TimeWork.Stopwatch)
        {
            // Преобразуем время в целые числа (миллисекунды)
            int scoreInMilliseconds = Mathf.RoundToInt(timer * 1000);
            YandexGame.NewLeaderboardScores("TimeTrialBoard", scoreInMilliseconds);
        }
    }

    public void OnLevelComplete()
    {
        isTimerPaused = true;
        SendScoreToLeaderboard();
    }


}
public enum TimeWork
{
    None,
    Stopwatch,
    Timer
}

