using UnityEngine;

public class TutorialController : MonoBehaviour
{
    // �������, ������� ���������� ��� �������� ���������
    public System.Action OnTutorialClosed;

    // ����� ��� �������� ���������
    public void CloseTutorial()
    {
        if (OnTutorialClosed != null)
        {
            OnTutorialClosed(); // �������� �������
        }
        gameObject.SetActive(false); // �������� ��������
    }
}