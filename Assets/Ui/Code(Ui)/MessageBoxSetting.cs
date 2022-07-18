using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBoxSetting : MonoBehaviour
{
    public static bool activeMessageChat;

    public void OnClickMessageButton()
    {
        activeMessageChat = true;
    }
}
