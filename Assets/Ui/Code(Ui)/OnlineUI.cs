﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            gameObject.SetActive(true);
        }
        else {
            Debug.Log("이름 필요");  //애니메이션 못해서 로그로함
        }
    }
}
