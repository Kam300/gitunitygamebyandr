using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class menu : MonoBehaviour
{
    public Button[] Lvls;
   // public Text coinText;

    public GameObject LoadingScreen;
  
 
    public Sprite star, noStar;
  

    //  [SerializeField] private Button SecondButtonReword;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Lvl"))
            for (int i = 0; i < Lvls.Length; i++)
            {
                if (i <= PlayerPrefs.GetInt("Lvl"))
                    Lvls[i].interactable = true;
                else
                    Lvls[i].interactable = false;
            }



        
        // Ïðåäïîëàãàÿ, ÷òî ó âàñ åñòü ìàññèâ êíîïîê Lvls
        for (int i = 0; i < Lvls.Length; i++)          // ÏÎÊÀ ÍÅÒ ÑÊÐÈÏÒÀ ÊÎÒÎÐÛÉ ÁÓÄÅÒ Ñ×ÈÒÀÒÜ ÐÅÉÒÈÍÃ ÏÐÎÉÄÅÍÍÎÃÎ ÓÐÎÂÍß
        {
            if (PlayerPrefs.HasKey("stars" + (i + 1)))
            {
                int stars = PlayerPrefs.GetInt("stars" + (i + 1));

                for (int j = 0; j < 3; j++)
                {
                    Lvls[i].transform.GetChild(j).GetComponent<Image>().sprite = (j < stars) ? star : noStar;
                }
            }
            else
            {
                for (int j = 0; j < 3; j++)
                {
                    Lvls[i].transform.GetChild(j).gameObject.SetActive(false);
                }
            }
        }
       


    }

    // Update is called once per frame
    void Update()
    {
       /* 

        if (PlayerPrefs.HasKey("coins"))
            coinText.text = PlayerPrefs.GetInt("coins").ToString();
        else
            coinText.text = "0";
       */
    }
    public void OpenScene(int index)
    {
        LoadingScreen.SetActive(true);
        SceneManager.LoadScene(index);
    }
    public void DelKeys()
    {
        PlayerPrefs.DeleteAll();
    }





}


