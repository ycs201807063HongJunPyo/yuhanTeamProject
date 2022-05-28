using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerMovement : NetworkBehaviour
{
    //이동관련 변수
    private float moveX;
    private float moveY;
    private Animator anim;  // 애니메이션 관련 정보
    public bool isMoving;  //이동 가능한지 확인

    [SyncVar]
    public float moveSpeed;

    //사격관련 변수
    
    private int shotFlag;  //방향 플래그 변수
    [SyncVar]
    public float shotDelay;  //조준 끝(사격)
    [SyncVar]
    public float curShotDelay;  //조준 중(조준)
    




    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //카메라 조정 코드
        if (hasAuthority) {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0f, 0f, -10f);
            cam.orthographicSize = 2.5f;
        }
    }

    void FixedUpdate()
    {
        Move();
        Fire();
        AimDelay();
    }

    // 이동 & 애니메이션 함수
    void Move()
    {
        if (hasAuthority && isMoving) {
            //바뀐 이동 시작
            Vector3 dir = Vector3.ClampMagnitude(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f), 1f);
            //회전?
            if(dir.x < 0f) {
                transform.localScale = new Vector3(-0.75f, 0.75f, 1f);
            }
            else if(dir.x > 0f) {
                transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }
            transform.position += dir * moveSpeed * Time.deltaTime;
            //바뀐 이동 끝

            // 애니메이션 세팅
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");

            // 좌, 우 이동 시
            if (anim.GetInteger("hAxisRaw") != moveX) {
                anim.SetBool("isChange", true);
                anim.SetInteger("hAxisRaw", (int)moveX);
            }
            // 상, 하 이동 시
            else if (anim.GetInteger("vAxisRaw") != moveY) {
                anim.SetBool("isChange", true);
                anim.SetInteger("vAxisRaw", (int)moveY);
            }
            else {
                anim.SetBool("isChange", false);
            }
            // 애니메이션 세팅 끝   
        }
    }

    //사격 함수
    void Fire() {
        //보는 방향이 정확하지 못함(너무 세세하게 shotFlag가 수정되서), 버튼을 천천하고 정확히 1개씩만 눌러서 방향 확인해야함(일단 나가는거에 의의맞춤)
        //누르면 쏘게하기 Input.GetButton("Fire1")
        // 총알 발사 방향
        if (moveX > 0 && moveY == 0) {
            // 오른쪽
            shotFlag = 1;
        }
        else if (moveX < 0 && moveY == 0) {
            // 왼쪽
            shotFlag = 2;
        }
        else if (moveX == 0 && moveY > 0) {
            // 위쪽
            shotFlag = 3;
        }
        else if (moveX == 0 && moveY < 0) {
            // 아래쪽
            shotFlag = 4;
        }

        if (curShotDelay > shotDelay) {
           
            var manager = NetworkRoomManager.singleton as MafiaRoomManager;
            var bullet = Instantiate(manager.spawnPrefabs[1], transform.position, transform.rotation);
            NetworkServer.Spawn(bullet);  // 총알 스폰

            if (manager.mode == Mirror.NetworkManagerMode.Host) {
                bullet.GetComponent<Bullet>().RpcShotUpdate(this.shotFlag);
            }
            else {
                bullet.GetComponent<Bullet>().CmdShotUpdate(this.shotFlag);
            }
            

            curShotDelay = 0;
        }
        else {
            return;
        }
    }

    //사격 재장전 함수
    void AimDelay() {
        curShotDelay = curShotDelay + Time.deltaTime ;
    }


}


