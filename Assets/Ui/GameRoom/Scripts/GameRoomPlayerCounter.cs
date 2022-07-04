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

    public void UpdatePlayerCount()
    {
        var players = FindObjectsOfType<MafiaRoomPlayer>();
        var manager = NetworkRoomManager.singleton as MafiaRoomManager;
        bool isStartable = players.Length >= 3;
        playerCountText.color = isStartable ? Color.white : Color.red;
        playerCountText.text = string.Format("{0} / {1} \n{2}", players.Length, maxPlayer, manager.networkAddress);
        // 현재 접속한 인원의 수가 최소 인원이상의 경우에 따라 호출 가능하게 해준다, 11장 8:20
        LobbyUIManager.Instance.SetInteractableStartButton(isStartable);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            var manager = NetworkManager.singleton as MafiaRoomManager;
            maxPlayer = manager.maxConnections;
        }
    }

   
}
