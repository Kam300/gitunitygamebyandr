using UnityEngine;
using UnityEngine.UI;
using YG;

public class TriggerCanvas : MonoBehaviour
{
    public GameObject canvasObject;
    public GameObject canvasObject2;// ������ �� ������ Canvas
    public AudioClip melody; // ������ �� �����-���� �������
    private AudioSource audioSource; // ������ �� AudioSource ���������
// ����, �����������, ������ �� ����� � �������

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // �������� ������ �� AudioSource ���������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") ) // ���������, ���������� �� � �������� � ����� "Player" � �� ������ �� � ������� �����
        {
            ActivateCanvas(); // �������� ����� ��� ��������� Canvas
            PlayMelody(); // �������� ����� ��� ������������ �������
           // ������������� ����, �����������, ��� ����� ��� ������ � �������
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ���������, ����� �� ����� �� ��������
        {
            DeactivateCanvas(); // �������� ����� ��� ����������� Canvas
        }
    }

    private void ActivateCanvas()
    {
        if (YandexGame.EnvironmentData.language == "en")
        {
            canvasObject.SetActive(true); // ���������� ������ Canvas
        }
        else
        {
            canvasObject2.SetActive(true);
        }
       
    }

    private void DeactivateCanvas()
    {
        if (YandexGame.EnvironmentData.language == "en")
        {
            canvasObject.SetActive(false); // ���������� ������ Canvas
        }
        else
        {
            canvasObject2.SetActive(false);
        }
    }

    private void PlayMelody()
    {
        audioSource.clip = melody; // ������������� �����-���� �������
        audioSource.Play(); // ��������� ������������ �������
    }


}
