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
        // ��������� ����������� ����
        currentSpriteIndex = PlayerPrefs.GetInt("CurrentLanguageIndex", 0);
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        // ��������� ������
        buttonImage.sprite = sprites[currentSpriteIndex];

        // ��������� �����
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
