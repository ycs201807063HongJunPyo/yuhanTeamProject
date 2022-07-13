using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageChatController : MonoBehaviour {

    [SerializeField]
    private GameObject textChatPre;
    [SerializeField]
    private Transform parentContent;
    [SerializeField]
    private TMP_InputField inputField;

    private string ID;


    public void OnEndEditEventMethod() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            UpdateChat();
        }
    }


    public void UpdateChat() {
        if (inputField.text.Equals("")) return;  //��������� ����
        GameObject clone = Instantiate(textChatPre, parentContent);  //��ȭ ���� ����� ���� text UI ����
        clone.GetComponent<TextMeshProUGUI>().text = $"{ID} : { inputField.text}";
        inputField.text = "";
    }
}
