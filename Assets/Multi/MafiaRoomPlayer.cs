using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MafiaRoomPlayer : NetworkRoomPlayer
{
    //플레이어 번호 가져오는거같은데 사격할때 쓸수있을듯
    private static MafiaRoomPlayer myRoomPlayer;
    public static MafiaRoomPlayer MyRoomPlayer {
        get {
            if(myRoomPlayer == null) {
                var players = FindObjectsOfType<MafiaRoomPlayer>();
                foreach(var player in players) {
                    if (player.hasAuthority) {
                        myRoomPlayer = player;
                    }
                }
            }
            return myRoomPlayer;
        }
    }

    [SyncVar]
    public string nickname;

    public PlayerMovement playerCharacter;

    //유튭11 1:52 에러
    public new void Start()
    {
        base.Start();
        //isServer가 위에오면 첫번째 씹히고 다음부터
        //isLocal이 위에오면 두번째 씹히고 다음부터
        ///SpawnLobbyPlayerCharacter이걸로 캐릭터 만들어주는데 닉네임 설정 전에 만들어줘서 계속 밀리는것같은데 그렇다고 CmdSetNickname위에두면 두번째가 씹힘

        if (isServer)
        {
            SpawnLobbyPlayerCharacter();
            LobbyUIManager.Instance.ActiveStartButton();  // 서버 역활을 해주는 경우에만 호출 가능하도록 만듬, 11장 8:11
        }

        if (isLocalPlayer)  //isLocalPlayer
        {
            CmdSetNickname(PlayerSetting.playerName);

        }

        //플레이어 숫자 알려주기
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

        var player = Instantiate(MafiaRoomManager.singleton.spawnPrefabs[0]).GetComponent<PlayerMovement>();
        NetworkServer.Spawn(player.gameObject, connectionToClient);
        player.ownerNetId = netId;
    }

    [Command]
    public void CmdSetNickname(string nick)
    {
        nickname = nick;
        playerCharacter.nickname = nick;
    }
}
