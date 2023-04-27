using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text nameText;
    public Text positionText;
    public Text scoreText;

    private FinishScript finishScript;
    private RaceManager raceManager;

    void Start()
    {
        // Récupère la référence au script FinishScript du joueur
        finishScript = GetComponent<FinishScript>();

        // Récupère la référence au script RaceManager
        raceManager = FindObjectOfType<RaceManager>();

        // Met à jour le texte du nom du joueur
        nameText.text = finishScript.name;
    }

    void Update()
    {
        // Met à jour le texte de la position du joueur
        int racerPosition = raceManager.getRacerPosition(finishScript);
        positionText.text = "" + racerPosition;

        // Met à jour le texte du score du joueur
        scoreText.text = "" + finishScript.score;
    }
}