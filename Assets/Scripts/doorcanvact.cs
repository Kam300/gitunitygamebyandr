using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using YG;

public class TriggerCanvas2 : MonoBehaviour
{
    public GameObject canvasObject;
    public GameObject canvasObject2;// ������ �� ������ Canvas
    public GameObject bulet;
    public GameObject joystick;
    public GameObject check;
    public AudioClip melody; // ������ �� �����-���� �������
    private AudioSource audioSource; // ������ �� AudioSource ���������
                                     // ����, �����������, ������ �� ����� � �������
    public bool doormobil = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // �������� ������ �� AudioSource ���������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ���������, ���������� �� � �������� � ����� "Player" � �� ������ �� � ������� �����
        {
            ActivateCanvas(); // �������� ����� ��� ��������� Canvas
            check.SetActive(true);
           
            PlayMelody(); // �������� ����� ��� ������������ �������
                          // ������������� ����, �����������, ��� ����� ��� ������ � �������
          
        }

            
       
    }
   


    private void OnTriggerExit2D(Collider2D collision)
    {
        doormobil = false;
        if (collision.CompareTag("Player")) // ���������, ����� �� ����� �� ��������
        {
            check.SetActive(false);
            
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
