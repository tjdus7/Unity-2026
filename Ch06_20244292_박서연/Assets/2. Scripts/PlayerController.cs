using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // 수치를 직접 컨트롤할 수 있도록 변수 값을 최적화했습니다.
    float jumpForce = 500f;     // 점프 힘
    float walkForce = 25f;      // 걷는 힘 (좌우 가속도)
    float maxWalkSpeed = 4f;    // 최대 이동 속도 제한

    public Sprite[] walkSprites;
    public Sprite jumpSprite;
    public float animationPeriod = 0.1f;

    float time = 0;
    int idx = 0;

    int jumpCount = 0;  // 점프 횟수를 기억하기 위한 변수

    int health = 3; // 기본 하트 개수
    public GameObject[] hearts;

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
        // 1. [수정] 스페이스바를 누를 때, 점프 횟수가 2회 미만이면 점프 실행
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            // 연속 점프 시 이전 수직 속도를 초기화하여 일정한 높이 유지
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(transform.up * jumpForce);

            jumpCount++;
        }

        // 2. [수정] 키보드 좌우 입력 처리 (A/D 키 또는 왼쪽/오른쪽 방향키 둘 다 작동)
        float inputX = Input.GetAxisRaw("Horizontal"); // 왼쪽: -1, 입력없음: 0, 오른쪽: 1

        if (inputX > 0) // 오른쪽 이동
        {
            if (rb.linearVelocity.x < maxWalkSpeed)
            {
                rb.AddForce(transform.right * walkForce);
            }
            // 고양이가 오른쪽을 바라보도록 이미지 대칭 해제
            sr.flipX = false;
        }
        else if (inputX < 0) // 왼쪽 이동
        {
            if (rb.linearVelocity.x > -maxWalkSpeed)
            {
                rb.AddForce(-transform.right * walkForce);
            }
            // 고양이가 왼쪽을 바라보도록 이미지 대칭
            sr.flipX = true;
        }
        else // 키를 누르고 있지 않을 때 (순간 멈춤을 위해 마찰력 역할)
        {
            // 서서히 멈추게 하고 싶다면 이 부근에 감속 코드를 넣을 수 있습니다.
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.9f, rb.linearVelocity.y);
        }

        time += Time.deltaTime;

        // 애니메이션 처리
        // 수직 속도가 0이 아니거나, 좌우로 이동 중일 때 점프/이동 애니메이션이 조절됩니다.
        if (rb.linearVelocity.y != 0)
        {
            if (jumpSprite != null)
            {
                sr.sprite = jumpSprite;
            }
            if (anim != null)
            {
                anim.SetBool("IsJumping", true);
            }
        }
        else if (time > animationPeriod)
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

    void PlayerFall()
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

    void UpdateHeartUI()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("ClearScene");
        Debug.Log("성공");
    }
}