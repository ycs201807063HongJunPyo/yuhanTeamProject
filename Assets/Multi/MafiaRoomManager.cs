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
        //IP �������ֱ�
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());  // IP �ּ� �����ֱ�
        //IPv6�ƴ� v4�� �������ֱ�
        string ClientIP = string.Empty;
        for (int i = 0; i < host.AddressList.Length; i++) {
            if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork) {
                ClientIP = host.AddressList[i].ToString();
            }
        }
        base.networkAddress = ClientIP;
        //�ִ� �α��� ����
        base.maxConnections = playerCount;
    }
}
