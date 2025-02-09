using UnityEngine;

public class TutorialController : MonoBehaviour
{
    // Событие, которое вызывается при закрытии туториала
    public System.Action OnTutorialClosed;

    // Метод для закрытия туториала
    public void CloseTutorial()
    {
        if (OnTutorialClosed != null)
        {
            OnTutorialClosed(); // Вызываем событие
        }
        gameObject.SetActive(false); // Скрываем туториал
    }
}