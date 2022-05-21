using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private bool isHorizonMove;
    private float moveX;
    private float moveY;
    private Vector2 moveDirection;
    private Animator anim;

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
        // ProcessInputs 호출
        ProcessInputs();

        // 위, 아래 버튼 확인
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        // 수평이동 확인
        if(hDown){
            isHorizonMove = true;
        }
        else if(vDown){
            isHorizonMove = false;
        }
        else if(hUp || vUp){
            isHorizonMove = moveX != 0;
        }

        // 애니메이션 세팅
        if(anim.GetInteger("hAxisRaw") != moveX){
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)moveX);
        }
        else if(anim.GetInteger("vAxisRaw") != moveY){
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)moveY);
        }
        else{
            anim.SetBool("isChange", false);
        }
    }

    void FixedUpdate()
    {
        // Move 호출
        Move();
    }
    void ProcessInputs()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
