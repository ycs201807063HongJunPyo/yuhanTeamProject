using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MafiaRoomPlayer : NetworkRoomPlayer
{
    [SyncVar]
    public string nickname;
    public PlayerMovement lobbyPlayerCharacter;

    //���Z11 1:52 ����
    public new void Start()
    {
        base.Start();
        /*
         if (isLocalPlayer)  //isLocalPlayer
         {
             CmdSetNickname(PlayerSetting.playerName);  // ���� nickname
         }

        if (isServer)
        {
            SpawnLobbyPlayerCharactor();
        }*/
        CmdSetNickname(PlayerSetting.playerName);  // ���� nickname
        LobbyUIManager.Instance.GameRoomPlayerCounter.UpdatePlayerCount();
    }

    private void OnDestroy()
    {
        if(LobbyUIManager.Instance != null)
        {
            LobbyUIManager.Instance.GameRoomPlayerCounter.UpdatePlayerCount();
        }
    }
    
    [Command]
    public void CmdSetNickname(string nick)
    {
        nickname = nick;
        lobbyPlayerCharacter.nickname = nick;  
    }
}
