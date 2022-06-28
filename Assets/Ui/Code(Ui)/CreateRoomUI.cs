using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Net;
using System.Net.Sockets;

public class CreateRoomUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> crewImgs;
    [SerializeField]
    private List<Button> escTimeButtons;
    [SerializeField]
    private List<Image> maxPlayerCountButtons;

    private CreateGameRoomData gameRoomData;

    
    // Start is called before the first frame update
    void Start()
    {
        //필요없으면 지우기
        for (int i = 0; i < crewImgs.Count; i++) {
            Material materialInstance = Instantiate(crewImgs[i].material);
            crewImgs[i].material = materialInstance;
        }
        gameRoomData = new CreateGameRoomData() { escTime = 7, maxPlayerCount = 5 };
        InfestedCivCountUpdate();
    }

    public void EscTrainTimeUpdate(int count) {
        //열차 도착 시간
        gameRoomData.escTime = count;
        Debug.Log(gameRoomData.escTime);  //잘 되나 로그 테스트
    }

    public void MaxPlayerUpdate(int count) {
        //최대 플레이어 숫자
        gameRoomData.maxPlayerCount = count;
        InfestedCivCountUpdate();
    }

    private void InfestedCivCountUpdate() {
        //최대 플레이어 수에 따라 감염자 보이기 생성
        int civCount = gameRoomData.maxPlayerCount;
        Debug.Log(civCount);  //잘 되나 로그 테스트
        //첫 시작 다 가려주기
        for (int i = 0; i < 2; i++) {
            crewImgs[i].gameObject.SetActive(false);
        }

        //보여주기
        if (civCount >= 8) {
            for (int i = 0; i < 3; i++) {
                crewImgs[i].gameObject.SetActive(true);
            }
        }
        else if (civCount >= 6) {
            for (int i = 0; i < 2; i++) {
                crewImgs[i].gameObject.SetActive(true);
            }
        }
        else if (civCount <= 5) {
            crewImgs[0].gameObject.SetActive(true);
        }
    }

    public void CeateRoom()
    {
        var manager = NetworkRoomManager.singleton as MafiaRoomManager;
        manager.trainTime = gameRoomData.escTime;
        manager.playerCount = gameRoomData.maxPlayerCount;
        
        manager.StartHost();

        
    }

}

public class CreateGameRoomData {
    public int escTime;
    public int maxPlayerCount;
}
