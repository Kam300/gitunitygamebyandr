
using UnityEngine;
using UnityEngine.UI;

public class ButtonSlide : MonoBehaviour
{
    private RectTransform rt;
    private bool isMoving = false;
    public Vector2 startPos;
    public Vector2 endPos;
    public float speed = 500f; // Уменьшил скорость для более заметного эффекта
    private CanvasGroup canvasGroup; // Для контроля прозрачности

    void Start()
    {
        rt = GetComponent<RectTransform>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        endPos = rt.anchoredPosition;
        rt.anchoredPosition = startPos;
        canvasGroup.alpha = 0f; // Начинаем с полной прозрачности
    }

    void Update()
    {
        if (isMoving)
        {
            // Плавное движение
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, endPos, speed * Time.deltaTime / 1000f);

            // Плавное появление
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1f, speed * Time.deltaTime / 1000f);

            // Проверяем, достигли ли конечной позиции
            if (Vector2.Distance(rt.anchoredPosition, endPos) < 1f && canvasGroup.alpha > 0.99f)
            {
                isMoving = false;
                rt.anchoredPosition = endPos;
                canvasGroup.alpha = 1f;
            }
        }
    }

    void OnEnable()
    {
        rt.anchoredPosition = startPos;
        canvasGroup.alpha = 0f;
        isMoving = true;
    }

    // Метод для ручного запуска анимации
    public void StartSlideAnimation()
    {
        rt.anchoredPosition = startPos;
        canvasGroup.alpha = 0f;
        isMoving = true;
    }
}
