using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Net;
using System.Net.Sockets;
public class MafiaRoomManager : NetworkRoomManager
{
    public int maxConnetions;
    public int trainTime;
    public int playerCount;
    public override void OnRoomServerConnect(NetworkConnectionToClient conn) {
        base.OnRoomServerConnect(conn);
        //IP 설정해주기
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());  // IP 주소 구해주기
        //IPv6아닌 v4로 가져와주기
        string ClientIP = string.Empty;
        for (int i = 0; i < host.AddressList.Length; i++) {
            if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork) {
                
                ClientIP = host.AddressList[i].ToString();
            }
        }
        base.networkAddress = ClientIP;
        //최대 인구수 막기
        base.maxConnections = playerCount;
    }
}
