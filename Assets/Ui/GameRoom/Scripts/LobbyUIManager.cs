using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    public static LobbyUIManager Instance;

    [SerializeField]
    private GameRoomPlayerCounter gameRoomPlayerCounter;
    public GameRoomPlayerCounter GameRoomPlayerCounter { get { return gameRoomPlayerCounter; } }

    [SerializeField]
    private Text playerCountText;
    public void UpdatePlayerCount()
    {
        var manager = NetworkManager.singleton as MafiaRoomManager;
        var players = FindObjectsOfType<MafiaRoomPlayer>();
        playerCountText.text = string.Format("{0} /{1}", players.Length, manager.maxConnetions);
    }
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
