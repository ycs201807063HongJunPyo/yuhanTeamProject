using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MafiaRoomManager : NetworkRoomManager
{
    public int maxConnetions;
    public int trainTime;
    public int playerCount;
    public override void OnRoomServerConnect(NetworkConnectionToClient conn) {
        base.OnRoomServerConnect(conn);
        //최대 인구수 막기
        base.maxConnections = playerCount;
    }
}
