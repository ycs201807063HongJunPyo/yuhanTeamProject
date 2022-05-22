using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //이동관련 변수
    public float moveSpeed;
    public Rigidbody2D rb;
    private float moveX;
    private float moveY;
    private Vector2 moveDirection;
    private Animator anim;

    //사격관련 변수
    public GameObject bulletImage;  //총알 이미지
    public float shotDelay;  //조준 끝(사격)
    public float curShotDelay;  //조준 중(조준)
    public float bulletLifeTime;  //총알 생존 시간
    private int shotFlag;  //방향 플래그 변수
    private int shotSpeed = 10;  //총알 속도 일괄 조정

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
        Fire();
        AimDelay();
    }

    // 입력 처리 함수
    void ProcessInputs()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        
        moveDirection = new Vector2(moveX, moveY).normalized;

        // 애니메이션 세팅
        if (anim.GetInteger("hAxisRaw") != moveX){
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)moveX);
            //보는 방향 x축
            if (moveDirection.x > 0 && moveDirection.y == 0) {
                shotFlag = 1;
            }
            else if (moveDirection.x < 0 && moveDirection.y == 0) {
                shotFlag = 2;
            }
        }
        else if(anim.GetInteger("vAxisRaw") != moveY){
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)moveY);
            //보는 방향 y축
            if (moveDirection.x == 0 && moveDirection.y > 0) {
                shotFlag = 3;
            }
            else if (moveDirection.x == 0 && moveDirection.y < 0) {
                shotFlag = 4;
            }
        }
        else{
            anim.SetBool("isChange", false);
        }
    }
    
    // 이동 처리 함수
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    //사격 함수
    void Fire() {
        //보는 방향이 정확하지 못함(너무 세세하게 shotFlag가 수정되서), 버튼을 천천하고 정확히 1개씩만 눌러서 방향 확인해야함(일단 나가는거에 의의맞춤)
        //누르면 쏘게하기 Input.GetButton("Fire1")
        if (curShotDelay > shotDelay) {
            GameObject bullet = Instantiate(bulletImage, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            
            if(shotFlag == 1) {
                rigid.AddForce(Vector2.right * shotSpeed, ForceMode2D.Impulse);
            }
            else if (shotFlag == 2) {
                rigid.AddForce(Vector2.left * shotSpeed, ForceMode2D.Impulse);
            }
            else if (shotFlag == 3) {
                rigid.AddForce(Vector2.up * shotSpeed, ForceMode2D.Impulse);
            }
            else {
                rigid.AddForce(Vector2.down * shotSpeed, ForceMode2D.Impulse);
            }
            //총알 생존시간 지정
            ///우리가 총으로 처치하긴해도 박스헤드마냥 총알 막 쏘는겜은 아니라서 넉넉하게 시간줬음(나중에 수정할거면 하기)
            Destroy(bullet, bulletLifeTime);
            curShotDelay = 0;
        }
        else {
            return;
        }
    }

    //사격 재장전 함수
    void AimDelay() {
        curShotDelay += Time.deltaTime;
    }


}


