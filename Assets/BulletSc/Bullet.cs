using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Bullet : NetworkBehaviour {

    public Rigidbody2D rig;
    [SyncVar]
    private Vector3 dir;
    [SyncVar]
    public int bulletSpeed;

    public uint attacker;

    void Start() => Destroy(gameObject, 2f);

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "LimitLine") {
            Destroy(gameObject);
        }
        if(collision.tag == "Player"){
            /*
            var roomSlots = (NetworkManager.singleton as MafiaRoomManager).roomSlots;
            foreach(var roomPlayer in roomSlots){
                var mafiaRoomPlayer = roomPlayer as MafiaRoomPlayer;
                // 동일값
                Debug.Log("mafiaRoomPlayer : " + mafiaRoomPlayer.netId);
                Debug.Log("roomPlayer : " + roomPlayer.netId);
                
            }*/
        }
    }
}
