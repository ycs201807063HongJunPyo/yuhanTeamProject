using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Net;
using System.Net.Sockets;
using System;

public class OnlineUI : MonoBehaviour
{
    [SerializeField]
    private InputField playerNameInputField;
    [SerializeField]
    private GameObject createRoomGameUI;
    // Start is called before the first frame update

    [SerializeField]
    public InputField textIp;
    public void OnClickCreateRoomButton() {
        if(playerNameInputField.text != "") {
            PlayerSetting.playerName = playerNameInputField.text;
            createRoomGameUI.SetActive(true);
            gameObject.SetActive(false);
        }
        else {
            Debug.Log("이름 필요");  //애니메이션 못해서 로그로함
        }
    }

    public void onClickEnterGameRoomButton() {
        if (playerNameInputField.text != "") {
            
            PlayerSetting.playerName = playerNameInputField.text;
            var manager = MafiaRoomManager.singleton;
            //Client client = new Client();

            /*
            IPHostEntry hostEntry = Dns.GetHostEntry(manager.networkAddress);
            if (hostEntry.AddressList.Length == 0) {
                throw new Exception("Unable to resolve host: " + manager.networkAddress);
            }
            var endpoint = hostEntry.AddressList[0];
            mSocket = new Socket(endpoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            mSocket.Connect(endpoint, 7777);
            정확히 뭔지 모르겠는데 소켓 통신?
            */
            //uri통해 값 얻고 해당 값으로 연결
            //여기서 수정해주면될듯(클라이언트 주소도 호스트 주소로 옮겨야하는지, 지금처럼 연결만 해주면되는지)
            //https://wergia.tistory.com/107  첨부파일 받아서 해볼듯
            //Uri address = new Uri(manager.networkAddress.ToString());
            //Debug.Log(address);
            manager.networkAddress = textIp.text;
            manager.StartClient();
            //client.ConnectToServer();

        }
        else {
            Debug.Log("이름 필요");  //애니메이션 못해서 로그로함
        }
    }
}
