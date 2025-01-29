using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 3;
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
            if (targetY - transform.position.y < 4) speed = 6;
            else speed = 3;
            transform.position = new Vector3(transform.position.x,
                                             Mathf.Lerp(transform.position.y,targetY, speed * Time.deltaTime),
                                             transform.position.z); 
        }
    }
}
