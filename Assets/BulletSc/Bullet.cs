using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Bullet : NetworkBehaviour {

    public Rigidbody2D rig;
    private Vector3 dir;
    [SyncVar]
    public int bulletSpeed;
    void Start() => Destroy(gameObject, 3f);

    void FixedUpdate() {
        //transform.position += dir * bulletSpeed * Time.deltaTime;
        rig.AddForce(dir * bulletSpeed * Time.deltaTime, ForceMode2D.Impulse);
        
    }
    [Command]
    public void CmdShotUpdate(int shotFlag) {
        RpcShotUpdate(shotFlag);
    }

    [ClientRpc]
    public void RpcShotUpdate(int shotFlag) {
        if (shotFlag == 1) {
            //rig.AddForce(Vector2.right * bulletSpeed * Time.deltaTime, ForceMode2D.Impulse);
            dir = Vector3.right;
        }
        else if (shotFlag == 2) {
            //rig.AddForce(Vector2.left * bulletSpeed * Time.deltaTime, ForceMode2D.Impulse);
            dir = Vector3.left;
        }
        else if (shotFlag == 3) {
            //rig.AddForce(Vector2.up * bulletSpeed * Time.deltaTime, ForceMode2D.Impulse);
            dir = Vector3.up;
        }
        else if (shotFlag == 4){
            //rig.AddForce(Vector2.down * bulletSpeed * Time.deltaTime, ForceMode2D.Impulse);
            dir = Vector3.down;
        }
        Debug.Log(shotFlag);
    }
    void OnTriggerEnter2D(Collider2D collision) {
        //경계 벽 충돌
        if(collision.gameObject.tag == "LimitLine") {
            Destroy(gameObject);
        }
    }
}
