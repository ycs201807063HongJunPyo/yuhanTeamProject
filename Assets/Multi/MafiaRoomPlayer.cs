using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MafiaRoomPlayer : NetworkRoomPlayer
{
    //�÷��̾� ��ȣ �������°Ű����� ����Ҷ� ����������
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

    //���Z11 1:52 ����
    public new void Start()
    {
        base.Start();
        //isServer�� �������� ù��° ������ ��������
        //isLocal�� �������� �ι�° ������ ��������
        ///SpawnLobbyPlayerCharacter�̰ɷ� ĳ���� ������ִµ� �г��� ���� ���� ������༭ ��� �и��°Ͱ����� �׷��ٰ� CmdSetNickname�����θ� �ι�°�� ����

        if (isServer)
        {
            SpawnLobbyPlayerCharacter();
            LobbyUIManager.Instance.ActiveStartButton();  // ���� ��Ȱ�� ���ִ� ��쿡�� ȣ�� �����ϵ��� ����, 11�� 8:11
        }

        if (isLocalPlayer)  //isLocalPlayer
        {
            CmdSetNickname(PlayerSetting.playerName);

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
