using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 5;
    public int cameraOffset = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float targetY = GridManager.highest + cameraOffset;

        if (targetY > transform.position.y)
        {
            Debug.Log($"Camera Position: {transform.position.y} Target: {targetY} Highest:{GridManager.highest}");
            transform.position = new Vector3(transform.position.x,
                                             Mathf.Lerp(transform.position.y,targetY, speed * Time.deltaTime),
                                             transform.position.z); 
        }
    }
}
