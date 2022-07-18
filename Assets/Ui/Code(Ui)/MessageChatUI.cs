using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageChatUI : MonoBehaviour
{

    [SerializeField]
    private InputField inputField;
    // Update is called once per frame

    void Update()
    {
        // 엔터쳤을때 포커스
        if (Input.GetKeyDown(KeyCode.Return) && inputField.isFocused == false)
        {
            inputField.ActivateInputField();
        }
        // esc 눌렀을때 닫기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MessageBoxSetting.activeMessageChat = false;
            gameObject.SetActive(false);
        }
    }

}
