using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//import something for collision detection

public class Tetris_Block : MonoBehaviour
{
    public static List<GameObject> stack = new List<GameObject>();
    public static bool isGameOver = false;
    public Vector3 rotationPoint;
    public static int height = 20;
    public static int width = 12;
    private Rigidbody2D rb;
    private bool collided = false;
    public static int score = 0;
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
            if (transform.position.y < (Camera.main.transform.position.y - 10))
            {
                isGameOver = true;
                Time.timeScale = 0;
                Debug.Log("Game Over block fell");
            }
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
        // Stop the block from moving
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        //rb.freezeRotation = true

        // Add the block to the stack
        stack.Add(gameObject);
        // score 
        score += 1;
        Debug.Log("Score: " + score);
        GridManager.HighestPointUpdate(transform);
        FindObjectOfType<SpawnBlock>().NewBlock();
        StackFalls();
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

    public static void StackFalls()
    {
        float bottomLimit = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y; // Bottom of the camera view
        List<GameObject> toRemove = new List<GameObject>(); // Track blocks that are too low

        foreach (GameObject block in stack)
        {
            if (block == null) continue; // Skip destroyed blocks

            float blockY = block.transform.position.y;

            // If block falls below screen with a buffer (-1f), game over
            if (blockY < bottomLimit - 0.75f)
            {
                isGameOver = true;
                Time.timeScale = 0;
                Debug.Log("Game Over: Block fell below the screen");
                return;
            }

            // Remove blocks that are far below and irrelevants
            if (blockY < bottomLimit - 5f) // Clean up blocks way below the screen
            {
                toRemove.Add(block);
            }
        }

        // Clean up blocks that are way below
        foreach (GameObject block in toRemove)
        {
            stack.Remove(block);
            Destroy(block); 
        }
    }

}
