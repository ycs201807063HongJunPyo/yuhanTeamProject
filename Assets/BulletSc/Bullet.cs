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

    private bool check = true;  // 총알 발사가 본인인지 확인

    void Start() => Destroy(gameObject, 2f);

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "LimitLine") {
            Destroy(gameObject);
        }
        if(collision.tag == "Player"){
            if(!check){
                Destroy(gameObject);
                return;
            }
            check = false;
        }
    }
    
}
