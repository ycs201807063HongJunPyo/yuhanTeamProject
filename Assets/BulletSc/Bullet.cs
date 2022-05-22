using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision) {
        //경계 벽 충돌
        if(collision.gameObject.tag == "LimitLine") {
            Destroy(gameObject);
        }
    }
}
