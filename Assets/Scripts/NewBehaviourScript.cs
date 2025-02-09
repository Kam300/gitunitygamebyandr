using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG.Insides;
using YG;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    private bool n = true;
    public new GameObject[] gameObject = new GameObject[3];
    private GameObject door;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();

        }
        // ����������� AudioSource
        audioSource.playOnAwake = false;
        audioSource.volume = 1f; // ���������� ������ ���������
    }

    public void Openstena(GameObject stena)
    {
        stena.SetActive(false);
        if (n)
        {
            OnDoorButtonClicked();

            // ��������� ������� ����������� ����� ����� ����������������
            if (audioSource != null && audioClip != null)
            {
                audioSource.volume = 1f; // ���������, ��� ��������� �� �� ����
                audioSource.PlayOneShot(audioClip);
                Debug.Log("Playing door sound"); // ��������� ��� ��� �������
            }
            else
            {
                Debug.LogWarning("AudioSource ��� AudioClip �� ���������!");
            }

            n = false;
            for (int i = 0; i < gameObject.Length; i++)
            {
                Destroy(gameObject[i]);
            }

            door = GameObject.FindGameObjectWithTag("doorr");
            if (door != null)
            {
                Destroy(door);
            }
        }
    }
    public void OnDoorButtonClicked()
    {
        // ��� �������, ������� ����� ���������� � ������.�������
        string eventName = "trigger";

        // �������������� ���������, ������� ����� �������� � ��������
        var eventParams = new Dictionary<string, string>
    {
        { "action",  "������� ����� �������"}, // ����� ������
      
    };

        // �������� ������� � ������.�������
     //   YandexMetrica.Send(eventName, eventParams);

        // �������������: ����� �������� ����������� ��� ������� � ���������
        Debug.Log($"Level start button clicked. Event: {eventName}, Params: {JsonUtils.ToJson(eventParams)}");
    }


}
