using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; // ��������� ��� ������ � UI

public class adheart : MonoBehaviour
{
    public Move player; // ������ �� ������ ������
    public Button button; // ������ �� ������, ������� ����� ������������
    public Button button2; // ������ �� ������, ������� ����� ������������

    // Start is called before the first frame update
    void Start()
    {
        // ���������, ��� ������ �� ������ � ������ �����������
        if (player == null)
        {
            Debug.LogError("Player reference is missing!");
        }
        if (button == null)
        {
            Debug.LogError("Button reference is missing!");
        }

        // ���������� ������ ���������
        button.gameObject.SetActive(false);
         button2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // ���������� ������ �������������� �������� ������ ����:
            // 1. �������� ������ 2
            // 2. ���� ADButtonHP �������
            // 3. ����� �� � ��������� ���������
            if (player.curHp < 2 && player.ADButtonHP)
            {
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }

            // ���������� ������ ���� ������ ����:
            // 1. ���������� ���� ������ 1
            // 2. ���� bull �������
            // 3. ���� ADButtonBl �������
            // 4. ����� �� � ��������� ���������
            if (player.bulletCount < 1 && player.bull && player.ADButtonBl)
            {
                button2.gameObject.SetActive(true);
            }
            else
            {
                button2.gameObject.SetActive(false);
            }
        }
    }


}