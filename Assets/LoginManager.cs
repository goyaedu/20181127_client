﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

// 회원가입 폼
public struct SignUpForm
{
    public string username;
    public string password;
    public string nickname;
}

// 로그인
public struct SignInForm
{
    public string username;
    public string password;
}

// 응답
public struct LoginResult
{
    public int result;
}

public enum ResponseType
{
    INVALID_USERNAME = 0,
    INVALID_PASSWORD,
    SUCCESS
}

public class LoginManager : MonoBehaviour {

    public Image signupPanel;
    public InputField usernameInputField;
    public InputField passwordInputField;
    public InputField confirmPasswordInputField;
    public InputField nicknameInputField;

    public InputField loginUsernameInputField;
    public InputField loginPasswordInputField;
    public Button loginButton;

    // Use this for initialization
    void Start () {
        loginButton.interactable = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // 회원가입 버튼 이벤트
    public void OnClickSignUpButton()
    {
        signupPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    // 로그인 버튼 이벤트
    public void OnClickSignInButton()
    {
        loginButton.interactable = false;

        string username = loginUsernameInputField.text;
        string password = loginPasswordInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return;
        }

        // TODO: 서버에 회원가입 정보 전송
        SignInForm signinForm = new SignInForm();
        signinForm.username = username;
        signinForm.password = password;

        StartCoroutine(SignIn(signinForm));
    }

    IEnumerator SignIn(SignInForm form)
    {
        string postData = JsonUtility.ToJson(form);

        using (UnityWebRequest www = 
            UnityWebRequest.Put("http://localhost:3000/users/signin", postData))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.Send();

            loginButton.interactable = true;

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string resultStr = www.downloadHandler.text;

                var result = JsonUtility.FromJson<LoginResult>(resultStr);

                if (result.result == 2)
                {
                    SceneManager.LoadScene("Game");
                }

                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    // 확인 버튼 이벤트
    public void OnClickConfirmButton()
    {
        string password = passwordInputField.text;
        string confirmPassword = confirmPasswordInputField.text;
        string username = usernameInputField.text;
        string nickname = nicknameInputField.text;

        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword)
            || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(nickname))
        {
            return;
        }

        if (password.Equals(confirmPassword))
        {
            // TODO: 서버에 회원가입 정보 전송
            SignUpForm signupForm = new SignUpForm();
            signupForm.username = username;
            signupForm.password = password;
            signupForm.nickname = nickname;

            StartCoroutine(SignUp(signupForm));
        }

    }
    // 취소 버튼 이벤트
    public void OnClickCancelButton()
    {
        signupPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(900, 0);
    }

    IEnumerator SignUp(SignUpForm form)
    {
        string postData = JsonUtility.ToJson(form);
        byte[] sendData = Encoding.UTF8.GetBytes(postData);

        using (UnityWebRequest www = UnityWebRequest.Put("http://localhost:3000/users/add", postData))
        {
            www.method = "POST";
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public void UpdateLoginInputFiled()
    {
        if (!string.IsNullOrEmpty(loginUsernameInputField.text) 
            && !string.IsNullOrEmpty(loginPasswordInputField.text))
        {
            loginButton.interactable = true;
        }
        else
        {
            loginButton.interactable = false;
        }
    }
}