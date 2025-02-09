using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class cheste : MonoBehaviour
{
   private int coins;
    public Text cointext;
    private GameObject[] chests; // ������ �������� ��������
    public float interactionDistance = 2f; // ������������ ���������� ��� ��������������
    


    public Image[] hearts;
    public Sprite isLife, nonLife;
    public Move move;

    public GameObject Info; // ������ �� ������� ������ ��� ������������ ���������

    private Soundeffector soundeffector;

    void Start()
    {
        chests = GameObject.FindGameObjectsWithTag("chest"); // ����� ��� ������� � ����� "chest"
        soundeffector = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Soundeffector>();

    }

    void Update()
    {

        for (int i = 0; i < hearts.Length; i++)
        {
            // Если индекс элемента меньше текущего значения здоровья, установите спрайт `isLife`
            // В противном случае, установите спрайт `nonLife`
            hearts[i].sprite = (i < move.GetHp()) ? isLife : nonLife;
        }




        cointext.text = coins.ToString();
       


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            
            if (collision.gameObject.tag == "chest")
            {
                coins += 10;
                Destroy(collision.gameObject);
                soundeffector.PlayChestSound();
            }
        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }


    public int GetCoins()
    {
        return coins;
    }

}