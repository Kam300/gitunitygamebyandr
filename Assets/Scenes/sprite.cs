using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{
    public Sprite[] sprites;
    public Image buttonImage;
    public Button changeButton;

    private int currentSpriteIndex = 0;
    private static SpriteChanger instance;

    void Start()
    {
        changeButton.onClick.AddListener(ChangeSprite);
        LoadCurrentLanguage();
    }

    private void LoadCurrentLanguage()
    {
        // Загружаем сохраненный язык
        currentSpriteIndex = PlayerPrefs.GetInt("CurrentLanguageIndex", 0);
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        // Обновляем спрайт
        buttonImage.sprite = sprites[currentSpriteIndex];

        // Сохраняем выбор
        PlayerPrefs.SetInt("CurrentLanguageIndex", currentSpriteIndex);
        PlayerPrefs.SetInt("lg", currentSpriteIndex);
        PlayerPrefs.Save();
    }

    public void ChangeSprite()
    {
        currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
        UpdateLanguage();
    }
}
