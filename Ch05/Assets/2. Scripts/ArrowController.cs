using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float dropSpeed = 0.1f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -dropSpeed, 0);

        if(transform.position.y < -7)
        {
            Destroy(gameObject);
        }
        
    }
}
