using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.Networking;

// 회원가입 폼
public struct SignUpForm
{
    public string username;
    public string password;
    public string nickname;
}

public class LoginManager : MonoBehaviour {

    public Image signupPanel;
    public InputField usernameInputField;
    public InputField passwordInputField;
    public InputField confirmPasswordInputField;
    public InputField nicknameInputField;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // 회원가입 버튼 이벤트
    public void OnClickSignUpButton()
    {
        signupPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
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
}