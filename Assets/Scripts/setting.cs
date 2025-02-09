 using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class OpenPanel : MonoBehaviour
{
    // ������, ������� ����� �������
    public GameObject panel;

    // ������ ����������, ������� ���������� ������

    public Move move;
    //  private Soundeffector soundeffector;
    public AudioSource musicSourse, soundSourse;
    public bool isPaused = false;
    public float timer = 0f;
    public TimeWork timeWork = TimeWork.Timer;
    public Text timeText;
    public GameObject Infopanel;

    public GameObject Pause;

    public bool isTimerPaused = false; // �������� ��� ����

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
                timer = 360f; // 6 ����� ��� ������ �������
                break;
            case TimeWork.Stopwatch:
                timer = 0f; // �������� � 0 ��� �����������
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

    // �������� ��������
    // �������� ��������
    private void ShowTutorial()
    {
        if (tutorial != null)
        {
            tutorial.SetActive(true); // ���������� ��������
            // ������������� �� ������� �������� ���������
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

    // ���������� ������� �������� ���������
    private void OnTutorialClosed()
    {
        MarkTutorialAsShown(); // �������� �������� ��� ����������
        ShowInfopanel(); // ���������� Infopanel
    }

    // �������� Infopanel
    private void ShowInfopanel()
    {
        if (Infopanel != null)
        {
            Infopanel.SetActive(true); // ���������� Infopanel
        }
        else
        {
            Debug.LogError("Infopanel reference is missing!");
        }
    }

    // ���������, ��� �� �������� �������
    private bool WasTutorialShown()
    {
        return PlayerPrefs.GetInt("TUTORIAL", 0) == 1;
    }

    // �������� �������� ��� ����������
    private void MarkTutorialAsShown()
    {
        PlayerPrefs.SetInt("TUTORIAL", 1);
        PlayerPrefs.Save(); // ��������� ���������
    }

    // �������� ��������� ��������� (��������, ��� ������������)
    public void ResetTutorialState()
    {
        PlayerPrefs.DeleteKey("TUTORIAL");
        Debug.Log("Tutorial state reset.");
    }


    // �������� ���� �����
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
        

        // ��������� �������/�����������
        if (!isTimerPaused)
        {
            switch (timeWork)
            {
                case TimeWork.Timer: // ����� �������
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

                case TimeWork.Stopwatch: // ����� �����������
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
        musicSourse.Stop(); // ���������������� ������������ ������
        soundSourse.Stop(); // ���������������� ������������ ������
        musicSourse.Pause(); // ���������������� ������������ ������
        soundSourse.Pause(); // ���������������� ������������ ������
        isPaused = false; // ������������� ��������� �����

    }

    public void PauseOn0()
    {


        Time.timeScale = 0f;
        move.enabled = false;
      
        musicSourse.Stop(); // ���������������� ������������ ������
        soundSourse.Stop(); // ���������������� ������������ ������
        musicSourse.Pause(); // ���������������� ������������ ������
        soundSourse.Pause(); // ���������������� ������������ ������
        isPaused = false; // ������������� ��������� �����

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
        // ���� ����� ����� 1 (���� �� �� �����)
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
            musicSourse.Stop(); // ���������������� ������������ ������
            soundSourse.Stop(); // ���������������� ������������ ������
            musicSourse.Pause(); // ���������������� ������������ ������
            soundSourse.Pause(); // ���������������� ������������ ������
            isPaused = false; // ������������� ��������� �����
        }
        // ���� ����� �� ����� 1 (���� �� �����), ������ �� ������


    }
   
    public void PauseOfffff()
    {
        Time.timeScale = 1f; // ��������������� �����
        move.enabled = true; // �������� ��������
        musicSourse.Play(); // ������������ ������������ ������
        soundSourse.Play(); // ������������ ������������ ������

        isPaused = true;
        Pause.SetActive(true); // �������� "Pause"
        Infopanel.SetActive(false); // ���������� "Info"

    }
    public void PauseOf()
    {
        Time.timeScale = 1f; // ��������������� �����
        move.enabled = true; // �������� ��������
        musicSourse.Play(); // ������������ ������������ ������
        soundSourse.Play(); // ������������ ������������ ������
        isTimerPaused = false; // ���������� ��������� ����� �������
        UpdateTimerDisplay(); // ��������� ����������� �������
        isPaused = true;
        // ���������, ��� ������ �� ����� 0
        if (timer <= 0)
        {
           timer = 120f; // ������������� ������ �� 120 ������
            UpdateTimerDisplay();
           
        }

        // ��������, ��� move.tt �� ������������
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
            // ����������� ����� � ����� ����� (������������)
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

