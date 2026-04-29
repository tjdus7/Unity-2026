using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float jumpForce = 300f;
    float walkForce = 10f;
    float maxWalkSpeed = 1f;

    public Sprite[] walkSprites;
    public Sprite jumpSprite;
    public float animationPeriod = 0.1f;

    float time = 0;
    int idx = 0;

    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim; // ✅ Animator 선언 추가

    void Start()
    {
        Application.targetFrameRate = 60;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>(); // ✅ 여기로 이동
    }

    void Update()
    {
        // 점프
        if (Input.GetMouseButtonDown(0) && rb.linearVelocity.y == 0)
        {
            rb.AddForce(transform.up * jumpForce);
        }

        // 오른쪽 이동
        if (rb.linearVelocity.x < maxWalkSpeed)
        {
            rb.AddForce(transform.right * walkForce);
        }

        time += Time.deltaTime;

        // 애니메이션 처리
        if (rb.linearVelocity.y != 0)
        {
            anim.SetBool("IsJumping", true);
        }
        else if (time > animationPeriod)
        {
            anim.SetBool("IsJumping", false);
        }

        // 낙사 시 재시작
        if (transform.position.y < -8)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("ClearScene");
        Debug.Log("성공");
    }
}
