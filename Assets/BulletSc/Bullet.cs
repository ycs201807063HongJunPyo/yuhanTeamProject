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
            Destroy(this);  //this �ϸ� ��1�� �����ְ� gameObject�ϸ� �ƿ� �ȳ���(�̰� �����Ǽ� �׷���)
            //�ϴ� �ΰ� ���߿� ĳ�����Ҷ� �����ϱ�(�� ��Ƽ -> ĳ���� �۾� -> �� ��� ������ �ϱ�)
        }
    }
}
