using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//import something for collision detection

public class Tetris_Block : MonoBehaviour
{
    public Vector3 rotationPoint;
    public static int height = 20;
    public static int width = 12;
    private Rigidbody2D rb;
    private bool collided = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (!collided)
        { 
            OnInput();
        }
    }

   void OnInput()
    {
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
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!isValidGridPos())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
    }

    void  OnCollisionEnter2D (Collision2D collision)
    {
        if (collided) return;
        collided = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        //rb.freezeRotation = true;
        Debug.Log($"Highest Point: {GridManager.highest}");
        GridManager.HighestPointUpdate(transform);
        FindObjectOfType<SpawnBlock>().NewBlock();
    }
    // Check if the block is within the bounds of the grid
    bool isValidGridPos()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 + (Camera.main.transform.position.y - 9) || roundedY >= height)
            {
                return false;
            }
        }

        return true;
    }

}
