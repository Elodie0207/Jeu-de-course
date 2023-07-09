using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    //dictionnaire pour garder le score des jr.
    //la clé est le nom des != jr
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

        // Empeche destruction du gameobject pdt le load de la scene
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateScore(string playerName, int score)
    {
        // Ajouter le jr s'il n'est pas dans le disctionnaire
        if (!playerScores.ContainsKey(playerName))
        {
            playerScores[playerName] = score;
        }
        // S'il existe, on additionne son nv score
        else
        {
            playerScores[playerName] += score;
        }
    }

    // Obtenir le scrore du jr
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

    //retourne le dictionnaire du score des jr
    public Dictionary<string, int> GetTopScores(int count)
    {
        return playerScores.OrderByDescending(pair => pair.Value).Take(count).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    // methode pour obtenir le score de tt les jr en retournant le dictionnaire
    public Dictionary<string, int> GetAllScores()
    {
        return playerScores;
    }
}