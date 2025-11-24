using UnityEngine;
using TMPro; // Only needed if using TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    // Reference to UI text
    public TextMeshProUGUI scoreText; // For TextMeshPro
    // public UnityEngine.UI.Text scoreText; // Use this if using regular Text

    void Start()
    {
        UpdateScoreUI();
    }

    // Call this function whenever player earns points
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}
