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
    void Start() => Destroy(gameObject, 2f);
    
    void OnTriggerEnter2D(Collider2D collision) {
        //경계 벽 충돌
        if(collision.gameObject.tag == "LimitLine") {
            Destroy(gameObject);
        }
    }
}
