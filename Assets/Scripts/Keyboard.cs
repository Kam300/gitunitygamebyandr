using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    public TMP_InputField inputField3;
    public Button uppercaseButton;
    public GameObject keyboardCanvas;
    bool inputFieldEmailClicked=false;
    bool inputFieldPasswordClicked = false;
    bool inputField3Clicked = false;
    bool uppercasePressed = false;
    public GameObject uppercaseText;





    public void Start()
    {

        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }

   
    public void onInputFieldEmailClick()
    {
        inputFieldEmailClicked = true;
        inputFieldPasswordClicked = false;
        inputField3Clicked = false;

        Debug.Log("inputfieldemail clicked");
    }
    public void onInputFieldPasswordClick()
    {
        inputFieldEmailClicked = false;
        inputFieldPasswordClicked = true;
        inputField3Clicked = false;
        Debug.Log("inputfieldpassword clicked");
    }
    public void onInputField3Click()
    {
        inputFieldEmailClicked = false;
        inputFieldPasswordClicked = false;
        inputField3Clicked = true;
        Debug.Log("inputfieldpassword clicked");
    }
    public void onKeyboardButtonClick()
    {
        string buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(buttonPressed);
        if (buttonPressed.Equals("UPPER"))
        {
            if (uppercasePressed)
            {
                uppercaseText.GetComponent<TextMeshProUGUI>().text = "uppercase: disabled";
                uppercaseButton.GetComponent<Image>().color = Color.white;
                uppercasePressed = false;
            }
            else
            {
                uppercaseText.GetComponent<TextMeshProUGUI>().text = "uppercase: enabled";
                uppercaseButton.GetComponent<Image>().color = Color.gray;
                uppercasePressed = true;
            }
        }
        else
        {
            if (uppercasePressed)
            {
                buttonPressed = buttonPressed.ToUpper();
            }
            else
            {
                buttonPressed = buttonPressed.ToLower();
            }

            if (inputFieldEmailClicked)
            {
                if (buttonPressed.Equals("CANC")|| buttonPressed.Equals("canc"))
                {
                    inputField1.text = inputField1.text.Remove(inputField1.text.Length - 1);
                }
                else
                {
                    if (buttonPressed.Equals("ENTER") || buttonPressed.Equals("enter") || buttonPressed.Equals("EMPTY") || buttonPressed.Equals("empty") || buttonPressed.Equals("UPPER")||buttonPressed.Equals("upper"))
                    {
                    }
                    else
                    {
                        inputField1.text += buttonPressed;
                    }
                }

            }

            if (inputFieldPasswordClicked)
            {
                if (buttonPressed.Equals("CANC") || buttonPressed.Equals("canc"))
                {
                    inputField2.text = inputField2.text.Remove(inputField2.text.Length - 1);
                }
                else
                {
                    if (buttonPressed.Equals("ENTER") || buttonPressed.Equals("enter") || buttonPressed.Equals("EMPTY") || buttonPressed.Equals("empty") || buttonPressed.Equals("UPPER") || buttonPressed.Equals("upper"))
                    {
                    }
                    else
                    {
                        inputField2.text += buttonPressed;
                    }
                }
            }
            if (inputField3Clicked)
            {
                if (buttonPressed.Equals("CANC") || buttonPressed.Equals("canc"))
                {
                    inputField3.text = inputField3.text.Remove(inputField3.text.Length - 1);
                }
                else
                {
                    if (buttonPressed.Equals("ENTER") || buttonPressed.Equals("enter") || buttonPressed.Equals("EMPTY") || buttonPressed.Equals("empty") || buttonPressed.Equals("UPPER") || buttonPressed.Equals("upper"))
                    {
                    }
                    else
                    {
                        inputField3.text += buttonPressed;
                    }
                }
            }

        }


    }


}
