using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MafiaRoomPlayer : NetworkRoomPlayer
{
    [SyncVar]
    public string nickname;

    public GameObject lobbyPlayerCharacter;

    public PlayerMovement playerCharacter;

    //���Z11 1:52 ����
    public new void Start()
    {
        base.Start();
        
        
         if (isLocalPlayer)  //isLocalPlayer
         {
            CmdSetNickname(PlayerSetting.playerName);
            Debug.Log(PlayerSetting.playerName);
        }
         
        if (isServer)
        {
            SpawnLobbyPlayerCharacter();
        }
        
        //�÷��̾� ���� �˷��ֱ�
        LobbyUIManager.Instance.GameRoomPlayerCounter.UpdatePlayerCount();

    }

    private void OnDestroy()
    {
        if(LobbyUIManager.Instance != null)
        {
            LobbyUIManager.Instance.GameRoomPlayerCounter.UpdatePlayerCount();
        }
    }

    private void SpawnLobbyPlayerCharacter() {
        var player = Instantiate(MafiaRoomManager.singleton.spawnPrefabs[0]);
        NetworkServer.Spawn(player, connectionToClient);
    }

    
    [Command]
    public void CmdSetNickname(string nick)
    {
        nickname = nick;
        //playerCharacter.nickname = nick;
        //lobbyPlayerCharacter.GetComponent<PlayerMovement>().nickname = nick;
    }
    
}
