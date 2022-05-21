using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunAttack : MonoBehaviour
{
    public GameObject bulletImage;
    public Transform shotTransform;  //ĳ���� ����εǸ� �ѱ� �ֱ�(ĳ���͸� ���ָ� ���� �߰��غ�)

    public float shotDelay;  //���� ��
    public float curShotDelay;  //���� ��
    // Update is called once per frame
    void Update()
    {
        Fire();
        AimDelay();
    }
    void Fire() {
        //������ ����ϱ� Input.GetButton("Fire1")
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
