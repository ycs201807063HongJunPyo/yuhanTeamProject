using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomSettingUI : MonoBehaviour
{
    public void ExitGameRoom() {
        var manager = MafiaRoomManager.singleton;
        if(manager.mode == Mirror.NetworkManagerMode.Host) {
            manager.StopHost();
        }
        else if (manager.mode == Mirror.NetworkManagerMode.ClientOnly) {
            manager.StopClient();
        }
    }
}
