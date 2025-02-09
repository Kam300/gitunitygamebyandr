using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finish : MonoBehaviour
{
    //   public main Main;


    public int index;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Exit")
        {
            Win(index);
        }
    }
    public void Win(int index)
    {
        //SceneManager.LoadScene("Dialog2");
        SceneManager.LoadScene(index);
        // WinScreen.SetActive(true);

    }
}
