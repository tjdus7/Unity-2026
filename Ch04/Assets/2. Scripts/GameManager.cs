using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform car;
    public Transform flag;
    public TextMeshProUGUI distance;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //car = GameObject.Find("car");
    }

    // Update is called once per frame
    void Update()
    {
        float length = flag.position.x
            - car.position.x;
        distance.text =
            "Distance : " + length.ToString("F2") + "m";
    }
}
