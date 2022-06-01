using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MafiaRoomPlayer : NetworkRoomPlayer
{
    [SyncVar]
    public string nickname;
    [SerializeField]
    public GameObject lobbyPlayerCharacter;

    //유튭11 1:52 에러
    public new void Start()
    {
        base.Start();
        
         if (isLocalPlayer)  //isLocalPlayer
         {
            CmdSetNickname(PlayerSetting.playerName);  // 오류 nickname
         }
         /*
        if (isServer)
        {
            SpawnLobbyPlayerCharactor();
        }
        */
         //플레이어 숫자 알려주기
        //LobbyUIManager.Instance.GameRoomPlayerCounter.UpdatePlayerCount();
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
        lobbyPlayerCharacter.GetComponent<PlayerMovement>().SetNickname(nickname);
    }
}
