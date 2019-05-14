using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class LoginButton : MonoBehaviour
{
    public InputField username;
    public InputField password;

    public GameObject loginUI;
    public GameObject gameUI;

    public SessionManager sessionManager;

    public void LogIn()
    {
        Debug.Log("Logging in...");
        sessionManager.loginButton = this;
        sessionManager.Login(username.text, password.text);
    }

    public void LoginSuccessful()
    {
        gameUI.SetActive(true);
        loginUI.SetActive(false);
    }
}
