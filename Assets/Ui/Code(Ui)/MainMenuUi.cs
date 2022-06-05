using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUi : MonoBehaviour
{
    public void OnClickOnlineButton() {
        Debug.Log("GameStart");
    }

    public void OnClickQuitButton() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
