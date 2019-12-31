using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Register : MonoBehaviour
{
    public GameObject username;
    public GameObject email;
    public GameObject password;
    public GameObject conpassword;
    private string Username;
    private string Email;
    private string Password;
    private string Confirmpassword;
    private string form;
    private bool ifvalid=false;
    private string[] characters = {"a","b","c","d","e","f","g","h","i","j","k",
    "l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
        "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
    "1","2","3","4","5","6","7","8","9","_","-"};
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void RegistrationButton()
    {
        bool UN = false;
        bool EM = false;
        bool PW = false;
        bool CPW = false;

        if (Username != "")
        {
            if (!System.IO.File.Exists(@"D:/LoginSystemInfo/" + Username + ".txt"))
            {
                UN = true;
            }
            else
            {
                Debug.LogWarning("User name has been taken!");
            }
        }
        else
        {
            Debug.LogWarning("User name is empty!");
        }
        #region
        if (Email != "") {
            EmailValidation();
            if (ifvalid){
                if (Email.Contains("@"))
                {
                    if (Email.Contains("."))
                    {
                        EM = true;
                    }
                    else
                    {
                        Debug.LogWarning("Email is incorrect");
                    }
                }
                else
                {
                    Debug.LogWarning("Email is incorrect!");
                }
            }
            else
            {
                Debug.LogWarning("Email is incorrect!");
            }
        }
        else
        {
            Debug.LogWarning("Email is empty!");
        }
        #endregion
        if (Password != "")
        {
            if (Password.Length >= 6)
            {
                PW = true;
            }else{
                Debug.LogWarning("Password must be at least 6 characters long!");
            }
        }
        else{
            Debug.LogWarning("Password is empty!");
        }
        if (Confirmpassword != "")
        {
            if (Confirmpassword == Password)
            {
                CPW = true;
            }else{
                Debug.LogWarning("Password doesn't match!");
            }
        }else{
            Debug.LogWarning("Confirm Password is empty!");
        }
        if (UN == true && EM == true && PW == true && CPW == true)
        {
            bool check = true;
            int i = 1;
            foreach(char c in Password)
            {
                if (check)
                {
                    Password = "";
                    check = false;
                }
                i++;
                char Encrypted = (char)(c * i);
                Password += Encrypted.ToString();
            }
            form = (Username + "\n" + Password + "\n" + "Email");
            System.IO.File.WriteAllText(@"D:/LoginSystemInfo/" + Username + ".txt",form);
            username.GetComponent<InputField>().text = "";
            email.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            conpassword.GetComponent<InputField>().text = "";
            print("Registration Complete!");
        }
        
        
    }
    void EmailValidation() {
        bool firstchar = false;
        bool lastchar = false;

        for (int i = 0; i <= characters.Length - 1; i++) {
            if (Email.StartsWith(characters[i])){
                firstchar = true;
            }
            if (Email.EndsWith(characters[i]))
            {
                lastchar = true;
            }
        }
        if (firstchar == true && lastchar == true)
        {
            ifvalid = true;
        }else {
            ifvalid = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                email.GetComponent<InputField>().Select();
            }
            if (email.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                conpassword.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Username != "" && Email != "" && Password != "" && Confirmpassword != "")
            {
                RegistrationButton();
            }
        }
        Username = username.GetComponent<InputField>().text;
        Email = email.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        Confirmpassword = conpassword.GetComponent<InputField>().text;
        
    }
}
