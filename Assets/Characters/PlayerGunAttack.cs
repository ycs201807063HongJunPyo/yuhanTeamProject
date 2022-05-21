using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunAttack : MonoBehaviour
{
    public GameObject bulletImage;
    public Transform shotTransform;  //캐릭터 제대로되면 총구 주기(캐릭터만 해주면 내가 추가해봄)

    public float shotDelay;  //조준 끝
    public float curShotDelay;  //조준 중
    // Update is called once per frame
    void Update()
    {
        Fire();
        AimDelay();
    }
    void Fire() {
        //누르면 쏘게하기 Input.GetButton("Fire1")
        if (curShotDelay > shotDelay) {
            GameObject bullet = Instantiate(bulletImage, shotTransform.position, shotTransform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            rigid.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            curShotDelay = 0;
        }
        else {
            return;
        }
    }
    void AimDelay() {
        curShotDelay += Time.deltaTime;
    }
}
