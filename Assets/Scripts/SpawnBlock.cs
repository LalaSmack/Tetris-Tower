using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    public GameObject[] blocks;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewBlock();
    }

    void  Update()
    {
        transform.position = new Vector3(transform.position.x,Camera.main.transform.position.y + 7, transform.position.z);
    }

    public void NewBlock()
    {
        Instantiate(blocks[Random.Range(0, blocks.Length)], transform.position, Quaternion.identity);
    }
}
