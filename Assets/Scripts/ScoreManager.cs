using UnityEngine;
using TMPro; // Needed for TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int currentScore = 0;

    [Header("UI Reference")]
    public TextMeshProUGUI scoreText; // Assign a TMP Text component here

    void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // Keep it across scenes if needed
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddPoints(int points)
    {
        currentScore += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }
}