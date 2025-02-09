using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG.Insides;
using YG;

public class InputFieldChecker : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    public TMP_InputField inputField3;
    public AudioClip failureSound;
    public string failureText;
    public GameObject Door;
    public GameObject Door1;
    public Canvas canvas;
  
   public void CheckInputFields()
    {
        // �������� �������� �� input fields
        int value1 = int.Parse(inputField1.text);
        int value2 = int.Parse(inputField2.text);
        int value3 = int.Parse(inputField3.text);

        // ���������� ��������
        string a = Convert.ToString(value1) + Convert.ToString(value2) + Convert.ToString(value3);
        Debug.Log((a));
       

        // ���������, ����� �� �������� 666
        if (a== failureText)
        {
            DoSomething();
        }
        else
        {
            PlayFailureSound();
        }
    }

    void DoSomething()
    {
        // ����� �� ������ �������� ��� ��� ���������� ������-���� ��������
        Debug.Log("�������� ����� 666!");
        OnDoorButtonClicked();
        Destroy(Door);
        Destroy(Door1);
        canvas.gameObject.SetActive(false);
    }

    void PlayFailureSound()
    {
        // ������������� ���� �������
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.PlayOneShot(failureSound);
        }
        else
        {
            Debug.LogError("AudioSource not found on the same GameObject as the InputFieldChecker script.");
        }
    }

    public void OnDoorButtonClicked()
    {
        // ��� �������, ������� ����� ���������� � ������.�������
        string eventName = "trigger";

        // �������������� ���������, ������� ����� �������� � ��������
        var eventParams = new Dictionary<string, string>
    {
        { "action",  "����� � ������ 2 �������"}, // ����� ������
      
    };

        // �������� ������� � ������.�������
       // YandexMetrica.Send(eventName, eventParams);

        // �������������: ����� �������� ����������� ��� ������� � ���������
        Debug.Log($"Level start button clicked. Event: {eventName}, Params: {JsonUtils.ToJson(eventParams)}");
    }
}
