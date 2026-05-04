using UnityEngine;

public class BamsongiGenerator : MonoBehaviour
{
    public GameObject bamsongiPrefab; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bamsoni = Instantiate(bamsongiPrefab);
            Vector3 dir = new Vector3(0, 200, 1000);
            bamsoni.GetComponent<BamsongiController>().Shoot(dir);
        }
    }
}
