
using UnityEngine;
using UnityEngine.UI;

public class MinimapController : MonoBehaviour
{
    public Camera minimapCamera; // Камера мини-карты
    public RawImage minimapDisplay; // UI элемент для отображения мини-карты
    public Transform player; // Ссылка на игрока
    public float height = 10f; // Высота камеры над игроком
    public float updateRate = 0.1f; // Частота обновления (в секундах)

    private RenderTexture minimapTexture;
    private float nextUpdateTime;

    void Start()
    {
        if (minimapCamera == null)
        {
            Debug.LogError("Minimap Camera reference is missing!");
            return;
        }

        if (minimapDisplay == null)
        {
            Debug.LogError("Minimap Display reference is missing!");
            return;
        }

        // Создаем RenderTexture для мини-карты
        minimapTexture = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32)
        {
            antiAliasing = 1,
            filterMode = FilterMode.Bilinear,
            anisoLevel = 0
        };
        minimapTexture.Create();

        // Настраиваем камеру мини-карты
        minimapCamera.targetTexture = minimapTexture;
        minimapCamera.orthographic = true;
        minimapCamera.clearFlags = CameraClearFlags.SolidColor;
        minimapCamera.backgroundColor = Color.black;
        minimapCamera.cullingMask = LayerMask.GetMask("Minimap"); // Убедитесь, что создали слой "Minimap"
        minimapCamera.allowHDR = false;
        minimapCamera.allowMSAA = false;

        // Настраиваем отображение
        minimapDisplay.texture = minimapTexture;
    }

    void LateUpdate()
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateMinimapCamera();
            nextUpdateTime = Time.time + updateRate;
        }
    }

    void UpdateMinimapCamera()
    {
        if (player != null && minimapCamera != null)
        {
            // Обновляем позицию камеры мини-карты
            Vector3 newPosition = player.position;
            newPosition.y = height;
            minimapCamera.transform.position = newPosition;

            // Принудительно обновляем рендер
            minimapCamera.Render();
        }
    }

    // Метод для изменения размера области видимости мини-карты
    public void SetMinimapZoom(float orthoSize)
    {
        if (minimapCamera != null)
        {
            minimapCamera.orthographicSize = orthoSize;
        }
    }

    // Метод для включения/выключения мини-карты
    public void ToggleMinimap(bool state)
    {
        if (minimapDisplay != null)
        {
            minimapDisplay.gameObject.SetActive(state);
        }
        if (minimapCamera != null)
        {
            minimapCamera.gameObject.SetActive(state);
        }
    }

    // Метод для изменения цвета фона мини-карты
    public void SetMinimapBackground(Color color)
    {
        if (minimapCamera != null)
        {
            minimapCamera.backgroundColor = color;
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

    // Метод для обновления слоев видимости
    public void UpdateMinimapLayers(string[] layerNames)
    {
        if (minimapCamera != null)
        {
            int mask = 0;
            foreach (string layerName in layerNames)
            {
                mask |= 1 << LayerMask.NameToLayer(layerName);
            }
            minimapCamera.cullingMask = mask;
        }
    }

    // Метод для оптимизации производительности
    public void SetMinimapQuality(MinimapQuality quality)
    {
        if (minimapTexture == null) return;

        switch (quality)
        {
            case MinimapQuality.Low:
                minimapTexture.width = 128;
                minimapTexture.height = 128;
                updateRate = 0.2f;
                break;
            case MinimapQuality.Medium:
                minimapTexture.width = 256;
                minimapTexture.height = 256;
                updateRate = 0.1f;
                break;
            case MinimapQuality.High:
                minimapTexture.width = 512;
                minimapTexture.height = 512;
                updateRate = 0.05f;
                break;
        }

        minimapTexture.Release();
        minimapTexture.Create();
        minimapDisplay.texture = minimapTexture;
    }
}

// Перечисление для качества мини-карты
public enum MinimapQuality
{
    Low,
    Medium,
    High
}
