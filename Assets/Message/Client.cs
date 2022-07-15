using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.IO;
using System;
using TMPro;
using Mirror;

public class Client : NetworkBehaviour {
	//public InputField NickInput;   //���߿� ĳ���� �̸� ��������
	string clientName;
	public InputField IpInput;
	bool socketReady;
	TcpClient socket;
	NetworkStream stream;
	StreamWriter writer;
	StreamReader reader;


	public void ConnectToServer() {
		// �̹� ����Ǿ��ٸ� �Լ� ����
		if (socketReady) return;
		// �⺻ ȣ��Ʈ/ ��Ʈ��ȣ
		int port = 7777;

		// ���� ����
		try {
			var manager = NetworkRoomManager.singleton as MafiaRoomManager;
			socket = new TcpClient(manager.networkAddress, port);
			stream = socket.GetStream();
			writer = new StreamWriter(stream);
			reader = new StreamReader(stream);
			socketReady = true;
			Debug.Log("Ŭ���̾�Ʈ ����");
		} catch (Exception e) {
			Chat.instance.ShowMessage($"���Ͽ��� : {e.Message}");
			Debug.Log("Ŭ���̾�Ʈ ����ȵ�");
		}
	}

	void Update() {
		if (socketReady && stream.DataAvailable) {
			string data = reader.ReadLine();
			if (data != null)
				OnIncomingData(data);
		}
	}

	void OnIncomingData(string data) {
		if (data == "%NAME") {
			//clientName = NickInput.text == "" ? "Guest" + UnityEngine.Random.Range(1000, 10000) : NickInput.text;
			clientName = "test" + UnityEngine.Random.Range(1000, 10000);
			Send($"&NAME|{clientName}");
			return;
		}

		Chat.instance.ShowMessage(data);
	}

	void Send(string data) {
		if (!socketReady) return;
		Debug.Log(data + "Send");
		writer.WriteLine(data);
		writer.Flush();
	}

	public void OnSendButton(TMP_InputField SendInput) {
		
		if (!Input.GetButtonDown("Submit")) return;
		SendInput.ActivateInputField();
		if (SendInput.text.Trim() == "") return;
		string message = SendInput.text;
		SendInput.text = "";
		Send(message);
		Debug.Log(message + "On");
	}


	void OnApplicationQuit() {
		CloseSocket();
	}

	void CloseSocket() {
		if (!socketReady) return;

		writer.Close();
		reader.Close();
		socket.Close();
		socketReady = false;
	}
}
