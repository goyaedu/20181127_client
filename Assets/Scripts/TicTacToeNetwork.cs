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

    private Vector2 startPanelPos;

    private PlayerType playerType;

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
        socket.On("exitRoom", ExitRoom);
        socket.On("startGame", StartGame);
    }

    void StartGame(SocketIOEvent e)
    {
        startPanel.gameObject.SetActive(false);
    }

    void ExitRoom(SocketIOEvent e)
    {
        socket.Close();
    }

    void JoinRoom(SocketIOEvent e)
    {
        string roomId = e.data.GetField("room").str;
    }

    void CreateRoom(SocketIOEvent e)
    {
        string roomId = e.data.GetField("room").str;

        playerType = PlayerType.PlayerOne;
    }
}
