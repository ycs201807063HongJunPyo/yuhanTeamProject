using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
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
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    // 입력 처리 함수
    void ProcessInputs()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

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
    
    // 이동 처리 함수
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
