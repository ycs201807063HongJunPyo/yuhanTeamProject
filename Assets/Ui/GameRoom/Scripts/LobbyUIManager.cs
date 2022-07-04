using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    public static LobbyUIManager Instance;

    [SerializeField]
    private GameRoomPlayerCounter gameRoomPlayerCounter;
    public GameRoomPlayerCounter GameRoomPlayerCounter { get { return gameRoomPlayerCounter; } }

    [SerializeField]
    private Button startButton; //11�� 7:44

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 11��
    public void ActiveStartButton()
    {
        startButton.gameObject.SetActive(true);  // start��ư�� �����ִ� �Լ� Ȱ��ȭ
    }
    // 11��
    public void SetInteractableStartButton(bool isInteractable)
    {
        startButton.interactable = isInteractable; // ��ư�� ��ȣ�ۿ��� ���ų� ���ֵ��� ������ִ� �Լ�
    }
    // 11��
    public void OnClickStartButton()
    {
        var players = FindObjectsOfType<MafiaRoomPlayer>(); // ��� �÷��̾��� readyToBegin�� true�� �����ؼ� �غ�����ش�
        for (int i = 0; i < players.Length; i++)
        {
            players[i].readyToBegin = true;
        }

        // Room Manager�� serverChangeScene�Լ��� �̿��ؼ� Gameplay Scene���� �����Ѵ�
        var manager = NetworkManager.singleton as MafiaRoomManager;
        manager.ServerChangeScene(manager.GameplayScene);
    }

}
