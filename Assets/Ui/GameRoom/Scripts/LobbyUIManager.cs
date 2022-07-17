using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Mirror;
public class LobbyUIManager : MonoBehaviour
{
    public static LobbyUIManager Instance;

    [SerializeField]
    private GameRoomPlayerCounter gameRoomPlayerCounter;
    public GameRoomPlayerCounter GameRoomPlayerCounter { get { return gameRoomPlayerCounter; } }

    [SerializeField]
    private Button startButton; //11장 7:44
    [SerializeField]
    private Button settingButton;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveSettingButton() {
        settingButton.gameObject.SetActive(true);  // start버튼을 보여주는 함수 활성화
    }

    // 11장
    public void ActiveStartButton()
    {
        startButton.gameObject.SetActive(true);  // start버튼을 보여주는 함수 활성화
    }
    // 11장
    public void SetInteractableStartButton(bool isInteractable)
    {
        startButton.interactable = isInteractable; // 버튼의 상호작용을 끄거나 켜주도록 만들어주는 함수
    }
    // 11��
    public void OnClickStartButton()
    {
        var players = FindObjectsOfType<MafiaRoomPlayer>(); // 모든 플레이어의 readyToBegin을 true로 변경해서 준비시켜준다
        for (int i = 0; i < players.Length; i++)
        {
            players[i].readyToBegin = true;
        }

        // Room Manager가 serverChangeScene함수를 이용해서 Gameplay Scene으로 변경한다
        var manager = NetworkManager.singleton as MafiaRoomManager;
        manager.ServerChangeScene(manager.GameplayScene);
    }

}
