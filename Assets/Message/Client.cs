using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.IO;
using System;
using TMPro;

public class Client : MonoBehaviour {
	//public InputField NickInput;   //���߿� ĳ���� �̸� ��������
	string clientName;

	bool socketReady;
	TcpClient socket;
	NetworkStream stream;
	StreamWriter writer;
	StreamReader reader;


	public void ConnectToServer() {
		// �̹� ����Ǿ��ٸ� �Լ� ����
		if (socketReady) return;
		var manager = MafiaRoomManager.singleton;
		// �⺻ ȣ��Ʈ/ ��Ʈ��ȣ
		string ip = manager.networkAddress;
		int port = 7777;

		// ���� ����
		try {
			socket = new TcpClient(ip, port);
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
