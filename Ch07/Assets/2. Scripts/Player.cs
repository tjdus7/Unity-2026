using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2.0f;

    Rigidbody rb;
    Animator anim;

    Vector3 moveDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Fire");
            return;
        }
        
        moveDirection = new Vector3(xInput, 0, zInput);

        if (moveDirection.magnitude > 0.1f)
        {
            moveDirection.Normalize();
            anim.SetBool("IsWalking", true);
            transform.forward = moveDirection; //Rotation
            rb.MovePosition(rb.position + moveDirection*moveSpeed*Time.deltaTime);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }
}
