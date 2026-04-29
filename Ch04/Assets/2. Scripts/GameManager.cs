using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    GameObject car;
    GameObject flag;
    GameObject distance;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        car = GameObject.Find("car");
        flag = GameObject.Find("flag");
        distance = GameObject.Find("distance"); 
    }

    // Update is called once per frame
    void Update()
    {
        float length = flag.transform.position.x - car.transform.position.x;
        distance.GetComponent<TextMeshProUGUI>().text =
            "Distance : " + length.ToString("F2") + "m";
    }
}
