using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class OnlineUI : MonoBehaviour
{
    [SerializeField]
    private InputField playerNameInputField;
    [SerializeField]
    private GameObject createRoomGameUI;
    // Start is called before the first frame update
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
            manager.StartClient();
        }
        else {
            Debug.Log("이름 필요");  //애니메이션 못해서 로그로함
        }
    }
}
