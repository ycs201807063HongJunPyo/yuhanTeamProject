using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxBulletLifeTime;
    public float curBulletLifeTime;
    void Update() {
        UpdateLifeTime();
        Dead();
    }
    void UpdateLifeTime() {
        curBulletLifeTime += Time.deltaTime;
    }
    void Dead() {
        if (curBulletLifeTime > maxBulletLifeTime) {
            Destroy(this);  //this 하면 딱1개 지워주고 gameObject하면 아예 안나옴(이게 삭제되서 그런듯)
            //일단 두고 나중에 캐릭터할때 같이하기(선 멀티 -> 캐릭터 작업 -> 총 사격 순서로 하기)
        }
    }
}
