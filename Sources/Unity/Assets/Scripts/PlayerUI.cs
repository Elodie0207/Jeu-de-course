using System;
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

    public void GetPlayerInfo()
    {
        // Vérifie si l'objet est actif avant de récupérer la référence au script FinishScript du joueur
        if (gameObject.activeInHierarchy)
        {
            finishScript = GetComponent<FinishScript>();
            raceManager = FindObjectOfType<RaceManager>();
            
        }
    }

    private void Start()
    {
        GetPlayerInfo();
    }

    void Update()
    {
        // Vérifie si le joueur possède un script FinishScript actif
        if (finishScript != null && finishScript.isActiveAndEnabled)
        {
            // Met à jour le texte de la position du joueur
            int racerPosition = raceManager.getRacerPosition(finishScript);
            positionText.text = "" + racerPosition;
            
            nameText.text = finishScript.name;

            // Met à jour le texte du score du joueur
            scoreText.text = "" + finishScript.score;
        }
       
    }
}