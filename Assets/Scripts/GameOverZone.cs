using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Debug.Log($"Game Over: {other.gameObject.name} fell out of bounds while moving.");
        Time.timeScale = 0; 


    }
    
    
}

