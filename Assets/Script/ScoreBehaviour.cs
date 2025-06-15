using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

public class ScoreBehaviour : MonoBehaviour
{
    public static ScoreBehaviour Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI congratsText;

    private int totalCollected = 0;
    public int totalToCollect = 5;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreUI();
        if (congratsText != null)
            congratsText.enabled = false;
    }

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
    
    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Coins: " + totalCollected + " / " + totalToCollect;
    }
}
