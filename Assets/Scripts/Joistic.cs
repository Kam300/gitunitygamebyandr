using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Joistic : MonoBehaviour
{

    private Joystick joystick;
    private GameObject button;  // прыжок
                               // Start is called before the first frame update

    private void Awake()
    {
        joystick = GameObject.FindGameObjectWithTag("JOISTIC").GetComponent<Joystick>();
        button = GameObject.FindGameObjectWithTag("JOISTIC2");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
            joystick.gameObject.SetActive(true);
            button.gameObject.SetActive(true);
        
    }
}
