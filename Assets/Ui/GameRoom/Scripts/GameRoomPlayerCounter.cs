using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class GameRoomPlayerCounter : NetworkBehaviour
{
    [SyncVar]
    private int minPlayer;
    [SyncVar]
    private int maxPlayer;

    [SerializeField]
    private Text playerCountText;

    public void UpdatePlayerCount()
    {
        // var manager = NetworkManager.singleton as MafiaRoomManager;
        var players = FindObjectsOfType<MafiaRoomPlayer>();
        bool isStartable = players.Length >= minPlayer;
        playerCountText.color = isStartable ? Color.white : Color.red;
        playerCountText.text = string.Format("{0} /{1}", players.Length, maxPlayer);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            var manager = NetworkManager.singleton as MafiaRoomManager;
            //minPlayer = manager.minPlayerCount;
            maxPlayer = manager.maxConnections;
        }
    }

   
}
