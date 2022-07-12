using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public static string attacker;
    public static string victim;

    // Start is called before the first frame update
    void Start(){}

    public bool Hit(){
        if(attacker != victim){ // 공격자와 피격자가 다를 때
            Debug.Log(attacker + " -> " + victim);
            return true;
        } else {
            return false;
        }
    }
    
}
