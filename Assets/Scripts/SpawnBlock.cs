using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    public GameObject[] blocks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewBlock();
    }

    public void NewBlock()
    {
        Instantiate(blocks[Random.Range(0, blocks.Length)], transform.position, Quaternion.identity);
    }
}
