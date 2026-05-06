using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 1f;


    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * rotationSpeed * Time.deltaTime;
        float zSpeed = zInput * moveSpeed * Time.deltaTime;

        transform.Translate(0, 0, zSpeed);
        transform.Rotate(0, xSpeed, 0);
    }
}
