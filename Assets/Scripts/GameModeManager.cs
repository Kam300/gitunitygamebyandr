using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameModeManager : MonoBehaviour
{
    private const string GAME_MODE_KEY = "GameMode";
    public static TimeWork selectedGameMode = TimeWork.Timer;

    [Header("UI Elements")]
    [SerializeField] private Toggle timerToggle;
    [SerializeField] private Toggle stopwatchToggle;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private void Start()
    {
        if (!ValidateComponents()) return;

        LoadGameMode();
        timerToggle.onValueChanged.AddListener(delegate { OnTimerToggleChanged(); });
        stopwatchToggle.onValueChanged.AddListener(delegate { OnStopwatchToggleChanged(); });

        UpdateLanguageTexts();
    }

    private bool ValidateComponents()
    {
        bool isValid = true;

        if (timerToggle == null)
        {
            Debug.LogError("Timer Toggle is not assigned!");
            isValid = false;
        }

        if (stopwatchToggle == null)
        {
            Debug.LogError("Stopwatch Toggle is not assigned!");
            isValid = false;
        }

        if (descriptionText == null)
        {
            Debug.LogError("Description Text is not assigned!");
            isValid = false;
        }

        return isValid;
    }

    private void UpdateLanguageTexts()
    {
        if (!ValidateComponents()) return;

        switch (YandexGame.savesData.language)
        {
            case "ru":
                if (selectedGameMode == TimeWork.Timer)
                {
                    descriptionText.text = "Обычный: У вас есть достаточно ограниченного времени на прохождение уровня";
                }
                else
                {
                    descriptionText.text = "Легкий: Нет ограниечение по времени, есть секундомер и лидерборд.";
                }
                break;

            case "en":
                if (selectedGameMode == TimeWork.Timer)
                {
                    descriptionText.text = "Normal: You have enough limited time to complete the level.";
                }
                else
                {
                    descriptionText.text = "Easy: There is no time limit, there is a stopwatch and a leaderboard.";
                }
                break;

            default:
                if (selectedGameMode == TimeWork.Timer)
                {
                    descriptionText.text = "Normal: You have enough limited time to complete the level.";
                }
                else
                {
                    descriptionText.text = "Easy: There is no time limit, there is a stopwatch and a leaderboard.";
                }
                break;
        }
    }

    private void LoadGameMode()
    {
        if (!ValidateComponents()) return;

        if (PlayerPrefs.HasKey(GAME_MODE_KEY))
        {
            selectedGameMode = (TimeWork)PlayerPrefs.GetInt(GAME_MODE_KEY);
        }

        timerToggle.isOn = selectedGameMode == TimeWork.Timer;
        stopwatchToggle.isOn = selectedGameMode == TimeWork.Stopwatch;
        UpdateLanguageTexts();
    }

    private void OnTimerToggleChanged()
    {
        if (!ValidateComponents()) return;

        if (timerToggle.isOn)
        {
            selectedGameMode = TimeWork.Timer;
            stopwatchToggle.isOn = false;
            SaveGameMode();
            UpdateLanguageTexts();
        }
    }

    private void OnStopwatchToggleChanged()
    {
        if (!ValidateComponents()) return;

        if (stopwatchToggle.isOn)
        {
            selectedGameMode = TimeWork.Stopwatch;
            timerToggle.isOn = false;
            SaveGameMode();
            UpdateLanguageTexts();
        }
    }

    private void SaveGameMode()
    {
        PlayerPrefs.SetInt(GAME_MODE_KEY, (int)selectedGameMode);
        PlayerPrefs.Save();
    }
}