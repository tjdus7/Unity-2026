using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject applePrefab;
    public GameObject bombPrefab;

    public float span = 1f;
    public int ratio = 3; //30% Bomb
    float delta = 0f;
    

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        GameObject item;
        if(delta > span)
        {
            int dice = Random.Range(0, 10);
            if(dice < ratio)
            {
                item = Instantiate(bombPrefab);
            }
            else
            {
                item = Instantiate(applePrefab);
            }

                //GameObject item = Instantiate(applePrefab);
            float x = Random.Range(-1, 2);
            float z = Random.Range(-1, 2);
            item.transform.SetParent(transform);
            item.transform.position = new Vector3(x, 7, z);

            delta = 0;
        }
    }
}
