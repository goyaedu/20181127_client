using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;

public class UIManager : MonoBehaviour
{
    public Text ServerMessage;

    public void OnButtonClicked()
    {
        StartCoroutine(GetUserInfo());
    }

    IEnumerator GetUserInfo()
    {
        using (UnityWebRequest www = 
            UnityWebRequest.Get("http://localhost:3000/users/info"))
        {
            string username = PlayerPrefs.GetString("username");

            if (!string.IsNullOrEmpty(username))
            {
                www.SetRequestHeader("Cookie", "username=" + username);
            }

            yield return www.Send();

            ServerMessage.text = www.downloadHandler.text;
        }
    }
}
