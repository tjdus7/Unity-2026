using UnityEngine;

public class TargetController : MonoBehaviour
{
    GameObject player;
    TargetGenerate tg;

    private void Start()
    {
        player = GameObject.Find("Player");
        tg = GameObject.FindObjectOfType<TargetGenerate>(); 
    }
    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(player.transform);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Bomsongi"))
        {
            tg.GenerateTarget(player.transform.position);
            Destroy(gameObject);
        }
    }
}
