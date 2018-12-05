using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;

public class TicTacToeNetwork : MonoBehaviour
{
    // Start Panel
    public RectTransform startPanel;
    public Text startMessageText;
    public Button connectButton;
    public Button closeButton;

    // Socket.io
    SocketIOComponent socket;

    public void Connect()
    {
        socket.Connect();
    }

    public void Close()
    {
        socket.Close();
    }

    private void Start()
    {
        GameObject so = GameObject.Find("SocketIO");
        socket = so.GetComponent<SocketIOComponent>();

        socket.On("joinRoom", JoinRoom);
        socket.On("createRoom", CreateRoom);
    }

    void JoinRoom(SocketIOEvent e)
    {
        Debug.Log("Join room.");
    }

    void CreateRoom(SocketIOEvent e)
    {
        Debug.Log("Create room.");
    }
}
