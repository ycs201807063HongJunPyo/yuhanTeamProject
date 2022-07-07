using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class GameRoomPlayerCounter : NetworkBehaviour
{
    [SyncVar]
    private int maxPlayer;

    [SerializeField]
    private Text playerCountText;
    [SerializeField]
    private Text IpPortText;

    public void UpdatePlayerCount()
    {
        var players = FindObjectsOfType<MafiaRoomPlayer>();
        var manager = NetworkRoomManager.singleton as MafiaRoomManager;
        bool isStartable = players.Length >= 3;
        playerCountText.color = isStartable ? Color.white : Color.red;
        playerCountText.text = string.Format("{0} / {1}", players.Length, maxPlayer);
        // ���� ������ �ο��� ���� �ּ� �ο��̻��� ��쿡 ���� ȣ�� �����ϰ� ���ش�, 11�� 8:20
        LobbyUIManager.Instance.SetInteractableStartButton(isStartable);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            var manager = NetworkManager.singleton as MafiaRoomManager;
            maxPlayer = manager.maxConnections;
            IpPortText.text = string.Format("IP : {0}", manager.networkAddress);
        }
    }

   
}
