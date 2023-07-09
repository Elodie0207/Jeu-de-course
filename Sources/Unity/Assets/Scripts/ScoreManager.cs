using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    // Dictionary to hold player scores. Key is the player's name.
    private Dictionary<string, int> playerScores = new Dictionary<string, int>();

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Don't destroy this object when a new scene is loaded
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateScore(string playerName, int score)
    {
        // If the player's name doesn't exist in the dictionary,
        // add it with the score.
        if (!playerScores.ContainsKey(playerName))
        {
            playerScores[playerName] = score;
        }
        // If the player's name is already in the dictionary, add the
        // new score to the existing one.
        else
        {
            playerScores[playerName] += score;
        }
    }

    // Optional: method to get a player's score. Returns 0 if the player's name doesn't exist in the dictionary.
    public int GetScore(string playerName)
    {
        if (playerScores.ContainsKey(playerName))
        {
            return playerScores[playerName];
        }
        else
        {
            return 0;
        }
    }

    public Dictionary<string, int> GetTopScores(int count)
    {
        return playerScores.OrderByDescending(pair => pair.Value).Take(count).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    // Optional: method to get all scores.
    public Dictionary<string, int> GetAllScores()
    {
        return playerScores;
    }
}