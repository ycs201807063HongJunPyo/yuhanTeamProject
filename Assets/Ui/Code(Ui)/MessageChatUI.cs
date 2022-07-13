using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageChatUI : MonoBehaviour {

    [SerializeField]
    private TMP_InputField inputField;
    // Update is called once per frame
    void Update()
    {

        //포커스 설정
        if (Input.GetKeyDown(KeyCode.Return) && inputField.isFocused == false) {
            inputField.ActivateInputField();
        }
        //닫아주기(esc)
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gameObject.SetActive(false);
        }
    }

}
