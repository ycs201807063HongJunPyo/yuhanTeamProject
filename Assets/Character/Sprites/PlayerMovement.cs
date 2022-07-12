using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerMovement : NetworkBehaviour
{
    public Rigidbody2D rig;
    private Animator anim;  // 애니메이션 관련 정보

    //이동관련 변수
    private float moveX;
    private float moveY;
    public bool isMoving;  //이동 가능한지 확인
    
    [SerializeField]
    private Transform playerTransform;

    [SyncVar]
    public float moveSpeed;

    //사격관련 변수
    [SyncVar]
    private int shotFlag;  //방향 플래그 변수

    private GameObject bullet;
    public GameObject BulletPrefab;

    //총알 장전속도 느리게함
    private float shotDelay;  //조준 끝(사격)
    private float curShotDelay;  //조준 중(조준)
    private int shotSpeed;  // 총알 속도 조정
    
    public int hp; // 플레이어 체력
    private bool SpawnBullet;   // 총알 발사 가능 여부
    public string attacker;    // 공격자

    // 이름 관련
    [SyncVar(hook = nameof(SetOwnerNetId_Hook))]
    public uint ownerNetId;
    public void SetOwnerNetId_Hook(uint _, uint newOwnerId) {
        var players = FindObjectsOfType<MafiaRoomPlayer>();
        foreach(var player in players) {
            if(newOwnerId == player.netId) {
                player.playerCharacter = this;
                break;
            }
        }
    }

    [SyncVar(hook = nameof(SetNickname_Hook))]
    public string nickname;
    [SerializeField]
    private Text nicknameText;
    public void SetNickname_Hook(string _, string value) {
        nicknameText.text = value;
        Debug.Log(nicknameText.text + " Player훅으로 오는 value값");
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        shotFlag = 0;
        shotDelay = 5;
        curShotDelay = 1;
        shotSpeed = 60000;
        hp = 4;

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
            if (dir.x < 0f) {
                transform.localScale = new Vector3(-0.75f, 0.75f, 1f);
            }
            else if (dir.x > 0f) {
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
        
        // 닉네임 방향전환
        if (transform.localScale.x < 0)
        {
            nicknameText.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (transform.localScale.x > 0)
        {
            nicknameText.transform.localScale = new Vector3(1f, 1f, 1f);
        }        
    }

    //사격 함수
    void Fire() {

        //보는 방향이 정확하지 못함(너무 세세하게 shotFlag가 수정되서), 버튼을 천천하고 정확히 1개씩만 눌러서 방향 확인해야함(일단 나가는거에 의의맞춤)
        
        ///누르면 쏘게하기
        //Input.GetButton("Fire1")

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

        //컨트롤 나가면 총알 나감
        if (Input.GetMouseButton(0) && shotFlag > 0 && curShotDelay > shotDelay) {
           
            var manager = NetworkRoomManager.singleton as MafiaRoomManager;
            //var bullet = Instantiate(manager.spawnPrefabs[1], transform.position, transform.rotation);

            if (manager.mode == Mirror.NetworkManagerMode.Host) {
                RpcShotUpdate(GetShotFlag(), 1);
            }            
            else{
                CmdShotUpdate(GetShotFlag());
            }
            
            curShotDelay = 0;
        }
        else {
            return;
        }
    }
    
    [Command(requiresAuthority = false)]
    public void CmdShotUpdate(int shotFlag) {
        RpcShotUpdate(shotFlag, 0);
    }
    
    [ClientRpc]
    public void RpcShotUpdate(int flag, int checking) {
        var roomSlots = (NetworkManager.singleton as MafiaRoomManager).roomSlots;
        foreach (var roomPlayer in roomSlots) {
            var mafiaRoomPlayer = roomPlayer as MafiaRoomPlayer;
            if (roomPlayer.netId == ownerNetId) {
                if(roomPlayer.index == 0 && checking == 0){ // 클라이언트가 발사 시 호스트는 처리 안함
                    SpawnBullet = false;
                    continue;
                }
                playerTransform = mafiaRoomPlayer.playerCharacter.transform;
                SpawnBullet = true;
                BulletAttack.attacker = nicknameText.text;
                break;
            }
        }
        
        if (SpawnBullet){
            bullet = Instantiate(BulletPrefab, playerTransform.position, playerTransform.rotation);
            rig = bullet.GetComponent<Rigidbody2D>();
            NetworkServer.Spawn(bullet);  // 총알 스폰
            attacker = nicknameText.text;
        } else {
            return;
        }
        
        if (flag == 1) {
            rig.AddForce(Vector2.right * shotSpeed * Time.deltaTime, ForceMode2D.Force);
            //dirBullet = Vector3.right;
        }
        else if (flag == 2) {
            rig.AddForce(Vector2.left * shotSpeed * Time.deltaTime, ForceMode2D.Force);
            //dirBullet = Vector3.left;
        }
        else if (flag == 3) {
            rig.AddForce(Vector2.up * shotSpeed * Time.deltaTime, ForceMode2D.Force);
            //dirBullet = Vector3.up;
        }
        else if (flag == 4) {
            rig.AddForce(Vector2.down * shotSpeed * Time.deltaTime, ForceMode2D.Force);
            //dirBullet = Vector3.down;
        }
    }

    //사격 재장전 함수
    void AimDelay() {
        curShotDelay = curShotDelay + Time.deltaTime ;
    }

    public int GetShotFlag() {
        return shotFlag;
    }

    // rigidBody 가 무언가와 충돌할 때 호출되는 함수
    void OnTriggerEnter2D(Collider2D other) { // Collider2D other 로 부딪힌 객체를 받아옴
        if(other.tag == "Bullet"){  // 곂친 상대의 태그가 Bullet 인 경우 처리
            
            BulletAttack bullet = this.gameObject.AddComponent<BulletAttack>();
            bool hit;
            
            BulletAttack.victim = nicknameText.text;
            hit = bullet.Hit();

            if(hit){
                hp -= 1;
            }
        }
        Debug.Log(nicknameText.text + " : " + hp);  // 플레이어의 hp 잔여량 표시
    }

}
