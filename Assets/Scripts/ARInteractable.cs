using UnityEngine;

public class ARInteractable : MonoBehaviour
{
    [SerializeField] private int pointsAwarded = 1;

    // This function is called when the object is clicked/tapped (via raycasting)
    public void OnTap()
    {
        // 1. Give the point
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddPoints(pointsAwarded);
        }
        else
        {
            Debug.LogError("ScoreManager not found! Cannot award points.");
        }

        // 2. Make the object disappear (Destroy it)
        Destroy(gameObject);
    }
}