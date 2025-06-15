/// ScoreBehaviour.cs
/// Manages the coin collection system and score display.
/// Shows a congratulatory message when all coins are collected.

using UnityEngine;
using TMPro;

public class ScoreBehaviour : MonoBehaviour
{
    /// Singleton instance for global access.
    public static ScoreBehaviour Instance;

    /// UI text that displays the player's current coin score.
    public TextMeshProUGUI scoreText;

    /// UI text displayed when the player collects all required coins.
    public TextMeshProUGUI congratsText;

    /// Number of coins collected by the player.
    private int totalCollected = 0;

    /// Total number of coins required to trigger the win message.
    public int totalToCollect = 5;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    /// Initializes the UI score and hides the win message.
    void Start()
    {
        UpdateScoreUI();
        if (congratsText != null)
            congratsText.enabled = false;
    }

    /// Adds to the player's score and checks for win condition.
    public void AddScore(int amount)
    {
        totalCollected += amount;
        UpdateScoreUI();

        if (totalCollected >= totalToCollect)
        {
            if (congratsText != null)
                congratsText.enabled = true;
        }
    }

    /// Updates the score UI text with current progress.
    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Coins: " + totalCollected + " / " + totalToCollect;
    }
}
