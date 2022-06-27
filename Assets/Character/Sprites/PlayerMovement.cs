using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

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
    [SyncVar]
    private int shotFlag;  //방향 플래그 변수

    //총알 장전속도 느리게함
    private float shotDelay = 5;  //조준 끝(사격)
    private float curShotDelay = 1;  //조준 중(조준)
    public Rigidbody2D rig;
    public int shotSpeed;





    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    
    

    // Start is called before the first frame update
    void Start()
    {
        shotFlag = 0;
        //카메라 조정 코드
        if (hasAuthority) {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0f, 0f, -10f);
            cam.orthographicSize = 2.5f;
        }

        


    }
    
    /*
    //이름 관련
    [SyncVar(hook = nameof(SetNickname_Hook))]
    public string nickname;
    [SerializeField]
    private Text nicknameText;
    public void SetNickname_Hook(string _, string value) {
        //nicknameText.text = string.Format("{0}", FindObjectsOfType<MafiaRoomPlayer>().Length);
        nicknameText.text = value;
        Debug.Log(nicknameText.text);
    }

    [Command]
    public void CmdSetNickname(string nick) {
        nickname = nick;
        nicknameText.text = nick;
        //lobbyPlayerCharacter.GetComponent<PlayerMovement>().nickname = nick;
    }
    */
    
    
    void FixedUpdate()
    {
        Move();
        Fire();
        AimDelay();
        //UpdateNickname();
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
        
        /*
        //유튭 11 6:26
        if (transform.localScale.x < 0)
        {
            nicknameText.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (transform.localScale.x > 0)
        {
            nicknameText.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        // 6:26 여기까지
        */
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
        //컨트롤 나가면 총알 나감
        if (curShotDelay > shotDelay && shotFlag > 0) {
           
            var manager = NetworkRoomManager.singleton as MafiaRoomManager;
            
            if (manager.mode == Mirror.NetworkManagerMode.Host) {
                var bullet = Instantiate(manager.spawnPrefabs[1], transform.position, transform.rotation);
                rig = bullet.GetComponent<Rigidbody2D>();
                //원래 if문 위치
                NetworkServer.Spawn(bullet);  // 총알 스폰
                RpcShotUpdate(GetShotFlag());
            }
            //시연용 호스트만 총쏘기
            /*
            else{
                CmdShotUpdate(GetShotFlag());
            }
            */
            Debug.Log(shotSpeed);
            curShotDelay = 0;
        }
        else {
            return;
        }
    }

    /*
    [Command(requiresAuthority = false)]
    public void CmdShotUpdate(int shotFlag) {
        RpcShotUpdate(shotFlag);
    }
    */
    [ClientRpc]
    public void RpcShotUpdate(int flag) {
        
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
    
    /*
    public void UpdateNickname() {
        //닉네임 테스트 코드
        CmdSetNickname(PlayerSetting.playerName);  // 오류 nickname
        Debug.Log(PlayerSetting.playerName);
        if (transform.localScale.x < 0) {
            nicknameText.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (transform.localScale.x > 0) {
            nicknameText.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    */

    public int GetShotFlag() {
        return shotFlag;
    }
}


