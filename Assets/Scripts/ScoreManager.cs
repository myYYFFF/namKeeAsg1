using UnityEngine;
using TMPro; 

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int currentScore = 0;
    public GameObject CanvasWin;

    [Header("UI Reference")]
    public TMP_Text scoreText; 

    void Awake()
    {
        Debug.Log("I am here!");

        // Singleton pattern to ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); 
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
        
        if(currentScore >= 10)
        {
            CanvasWin.SetActive(true);
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString()+"/10";
        }
    }

}