using UnityEngine;
using UnityEngine.UI;

public class MinimapSetup : MonoBehaviour
{
    public Camera minimapCamera; // Камера, которая снимает миникарту
    public RawImage minimapDisplay; // UI элемент, где отображается миникарта
    private RenderTexture minimapTexture;

    void Start()
    {
        // Создаем новый RenderTexture
        minimapTexture = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32)
        {
            antiAliasing = 1,
            filterMode = FilterMode.Bilinear
        };
        minimapTexture.Create();

        // Настраиваем камеру миникарты
        if (minimapCamera != null)
        {
            minimapCamera.targetTexture = minimapTexture;
            minimapCamera.clearFlags = CameraClearFlags.SolidColor;
            minimapCamera.backgroundColor = Color.black; // или любой другой цвет фона
            minimapCamera.allowMSAA = false;
            minimapCamera.allowHDR = false;
        }

        // Назначаем текстуру UI элементу
        if (minimapDisplay != null)
        {
            minimapDisplay.texture = minimapTexture;
        }
    }

    void OnDestroy()
    {
        if (minimapTexture != null)
        {
            minimapTexture.Release();
            Destroy(minimapTexture);
        }
    }
}