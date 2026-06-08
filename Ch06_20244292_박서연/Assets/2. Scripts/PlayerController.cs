using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // [플레이어 이동 관련 변수]
    float jumpForce = 500f;    
    float walkForce = 25f;     
    float maxWalkSpeed = 4f;    

    // [애니메이션 및 스프라이트 관련 변수]
    public Sprite[] walkSprites;  // 걷는 애니메이션에 사용할 스프라이트 배열
    public Sprite jumpSprite;  // 점프할 때 사용할 스프라이트
    public float animationPeriod = 0.1f;  // 애니메이션 프레임 간격 (초)

    float time = 0;  
    int idx = 0;  

    int jumpCount = 0;  // 점프 횟수를 기억하기 위한 변수

    // [체력 및 UI 관련 변수]
    int health = 3; // 기본 하트 개수
    public GameObject[] hearts;  // 하트 UI를 나타내는 GameObject 배열

    // [컴포넌트 참조 변수]
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        Application.targetFrameRate = 60;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        UpdateHeartUI(); // 초기 하트 UI 업데이트
    }

    void Update()
    {
        // [점프 처리] 스페이스바를 누를 때, 점프 횟수가 2회 미만이면 점프 실행
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            // 연속 점프 시 이전 수직 속도를 초기화하여 일정한 높이 유지
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(transform.up * jumpForce);  // 위쪽 * 점프 힘 적용

            jumpCount++;
        }

        // [좌우 이동 처리] 
        float inputX = Input.GetAxisRaw("Horizontal");  // 왼쪽: -1, 입력없음: 0, 오른쪽: 1

        // [오른쪽 이동]
        if (inputX > 0) 
        {
            if (rb.linearVelocity.x < maxWalkSpeed)
            {
                rb.AddForce(transform.right * walkForce);
            }
            // 고양이가 오른쪽을 바라보도록 이미지 대칭 해제
            sr.flipX = false;
        }

        // [왼쪽 이동]
        else if (inputX < 0) 
        {
            if (rb.linearVelocity.x > -maxWalkSpeed)
            {
                rb.AddForce(-transform.right * walkForce);
            }
            // 고양이가 왼쪽을 바라보도록 이미지 대칭
            sr.flipX = true;
        }

        // [정지]
        else 
        {
            // 서서히 멈추게 하고 싶다면 이 부근에 감속 코드를 넣을 수 있습니다.
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.9f, rb.linearVelocity.y);
        }

        time += Time.deltaTime;

        // [애니메이션 처리]
        if (rb.linearVelocity.y != 0) // 수직 속도가 0이 아니면 점프 중으로 간주
        {
            if (jumpSprite != null)
            {
                sr.sprite = jumpSprite;  // 점프 스프라이트로 변경
            }
            if (anim != null)
            {
                anim.SetBool("IsJumping", true);  
            }
        }
        else if (time > animationPeriod)  // 일정 시간마다 걷는 애니메이션 프레임 변경
        {
            if (walkSprites != null && walkSprites.Length > 0)
            {
                sr.sprite = walkSprites[0];  
            }
            if (anim != null)
            {
                anim.SetBool("IsJumping", false);
            }
        }

        // 낙사 시 재시작
        if (transform.position.y < -8)
        {
            PlayerFall();
        }
    }

    void PlayerFall()  // 플레이어가 낙사했을 때 호출되는 함수
    {
        health--;
        UpdateHeartUI();

        if (health <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
            Debug.Log("게임 오버");
        }
        else
        {
            transform.position = new Vector3(-7f, 2f, 0f);
            rb.linearVelocity = Vector2.zero;
            jumpCount = 0; // 점프 횟수 초기화
        }
    }

    void UpdateHeartUI()  // 현재 체력에 따라 하트 UI를 업데이트하는 함수
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].SetActive(true);  // 하트 보이도록 설정
            }
            else
            {
                hearts[i].SetActive(false);  // 하트 숨기도록 설정
            }
        }
    }
    // 플레이어가 바닥과 충돌할 때마다 점프 횟수를 초기화하여 다시 점프할 수 있도록 함
    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpCount = 0;
    }
    // 플레이어가 클리어 지점(깃발)과 충돌할 때 클리어 씬으로 이동
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("ClearScene");
        Debug.Log("성공");
    }
}