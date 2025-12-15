using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Score")]
    public int currentScore = 0;

    [Header("UI")]
    public TMP_Text scoreText;
    public GameObject CanvasWin;

    [Header("Audio")]
    public AudioSource BubblePop;
    public AudioSource YouWinBGM;

    private bool hasWon = false;

    void Awake()
    {
        Debug.Log("I am here!");

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (CanvasWin != null)
            CanvasWin.SetActive(false);

        UpdateScoreText();
    }

    public void AddPoints(int points)
    {
        if (hasWon) return; // 🛑 stop extra scoring

        currentScore += points;
        UpdateScoreText();

        if (BubblePop != null)
            BubblePop.Play();

        if (currentScore >= 10)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        hasWon = true;

        if (CanvasWin != null)
            CanvasWin.SetActive(true);

        if (YouWinBGM != null)
            YouWinBGM.Play();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}/10";
        }
    }

    public void BaoTapped()
    {
        if (BubblePop != null)
            BubblePop.Play();
    }
}
