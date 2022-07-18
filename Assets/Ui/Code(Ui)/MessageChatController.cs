using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;


public class MessageChatController : NetworkBehaviour
{
    public static MessageChatController Instance;

    [SerializeField]
    private GameObject textChatPre;
    [SerializeField]
    private Transform parentContent;
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private Text nickText;

    private string ID;
    private static event Action<string> OnMessage;

    void Start()
    {
        Instance = this;
    }

    public void OnEndEditEventMethod()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Send();
        }
    }

    public void UpdateChat()
    {
        if (inputField.text.Equals("")) return;  //비어있으면 종료
        GameObject clone = Instantiate(textChatPre, parentContent);   //대화 내용 출력을 위해 text UI 생성
        clone.GetComponent<TextMeshProUGUI>().text = $"{ID} : {inputField.text}";
        inputField.text = "";
    }

    // When a client hits the enter button, send the message in the InputField
    [Client]
    public void Send()
    {
        if (!Input.GetKeyDown(KeyCode.Return)) { return; }
        if (string.IsNullOrWhiteSpace(inputField.text)) { return; }
        string tempNick = MafiaRoomPlayer.MyRoomPlayer.nickname;
        CmdSendMessage((tempNick + " : " + inputField.text));
        inputField.text = string.Empty;
    }

    [Command(requiresAuthority = false)]
    private void CmdSendMessage(string message)
    {
        // Validate message
        RpcHandleMessage($"{message}");
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        OnMessage?.Invoke($"\n{message}");
        GameObject clone = Instantiate(textChatPre, parentContent);  //대화 내용 출력을 위해 text UI 생성
        clone.GetComponent<TextMeshProUGUI>().text = $"{message}";
        inputField.text = "";
    }

}/*
using Mirror;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageChatController : NetworkBehaviour {
    [SerializeField] private Text chatText = null;
    [SerializeField] private TMP_InputField inputField = null;
    [SerializeField] private GameObject canvas = null;

    private static event Action<string> OnMessage;

    // Called when the a client is connected to the server
    public override void OnStartAuthority() {
        canvas.SetActive(true);

        OnMessage += HandleNewMessage;
    }

    // Called when a client has exited the server
    [ClientCallback]
    private void OnDestroy() {
        if (!hasAuthority) { return; }

        OnMessage -= HandleNewMessage;
    }

    // When a new message is added, update the Scroll View's Text to include the new message
    private void HandleNewMessage(string message) {
        chatText.text += message;
    }

    // When a client hits the enter button, send the message in the InputField
    [Client]
    public void Send() {
        if (!Input.GetKeyDown(KeyCode.Return)) { return; }
        if (string.IsNullOrWhiteSpace(inputField.text)) { return; }
        CmdSendMessage(inputField.text);
        inputField.text = string.Empty;
    }

    [Command]
    private void CmdSendMessage(string message) {
        // Validate message
        RpcHandleMessage($"[{connectionToClient.connectionId}]: {message}");
    }

    [ClientRpc]
    private void RpcHandleMessage(string message) {
        OnMessage?.Invoke($"\n{message}");
    }

}*/