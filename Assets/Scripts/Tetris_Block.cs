using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tetris_Block : MonoBehaviour
{
    public Vector3 rotationPoint;
    float fallTime = 0.8f;
    float previousTime;
    public static int height = 20;
    public static int width = 12;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement
        if ((Input.GetKeyDown(KeyCode.LeftArrow)) || (Input.GetKeyDown(KeyCode.A)))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!isValidGridPos())           
            {
                transform.position += new Vector3(1, 0, 0);
            }
            
         
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetKeyDown(KeyCode.D)))
        {
           transform.position += new Vector3(1, 0, 0);
            if (!isValidGridPos())
            {
                transform.position += new Vector3(-1, 0, 0);
            }
            
        }
        else if ((Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.W)))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), 90);
            if (!isValidGridPos())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), -90);
            }
        }

        // Vertical movement
        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            previousTime = Time.time;
            if (!isValidGridPos())
            {
                transform.position += new Vector3(0, 1, 0);
                this.enabled = false;
                FindObjectOfType<SpawnBlock>().NewBlock();
                
            }
        }
    }

    // Check if the block is within the bounds of the grid
    bool isValidGridPos()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }
        }

        return true;
    }
}
