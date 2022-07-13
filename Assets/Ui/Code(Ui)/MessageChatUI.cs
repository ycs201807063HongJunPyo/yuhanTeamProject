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

        //��Ŀ�� ����
        if (Input.GetKeyDown(KeyCode.Return) && inputField.isFocused == false) {
            inputField.ActivateInputField();
        }
        //�ݾ��ֱ�(esc)
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gameObject.SetActive(false);
        }
    }

}
