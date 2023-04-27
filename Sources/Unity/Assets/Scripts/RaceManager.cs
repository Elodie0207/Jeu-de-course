using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour {

    // List des FinishScript pour chaque coureur
    public List<FinishScript> racers;

    // List pour stocker les positions des coureurs
    public List<int> positions;
    
    

    void Update () {
        
        // Appelle la fonction de mise à jour des positions des coureurs
        UpdateRacerPositions();
        
    }

    // Fonction de mise à jour des positions des coureurs
    private void UpdateRacerPositions() {
        Debug.Log("UpdateRacerPositions() called.");
        positions.Clear();
        foreach (FinishScript racer in racers) {
            int position = 1;
            foreach (FinishScript otherRacer in racers) {
                if (otherRacer.nbTours > racer.nbTours) {
                    position++;
                } else if (otherRacer.nbTours == racer.nbTours && otherRacer.GetComponent<TrackPosition>().object1Position.z > racer.GetComponent<TrackPosition>().object1Position.z) {
                    position++;
                }
            }
            positions.Add(position);
            Debug.Log(racer.name + " est en " + position + "ème position.");
        }

        // Met à jour la variable "positions" en temps réel
        for (int i = 0; i < positions.Count; i++) {
            positions[i] = positions.Count(p => p < positions[i]) + 1;
        }

        // Ajoute le score des joueurs en fonction de leur position
        for (int i = 0; i < racers.Count; i++) {
            
            int score = 0;
            
            switch (positions[i]) {
                case 1:
                    score = 10;
                    break;
                case 2:
                    score = 7;
                    break;
                case 3:
                    score = 5;
                    break;
                case 4:
                    score = 3;
                    break;
                case 5:
                    score = 2;
                    break;
            }
            
            racers[i].score = score;
        }

    }
    public int getRacerPosition(FinishScript racer)
    {
        // Trie la liste des joueurs en fonction de leur score
        List<FinishScript> sortedRacers = racers.OrderByDescending(r => r.score).ToList();

        // Trouve l'index du joueur dans la liste triée
        int position = sortedRacers.IndexOf(racer);

        // Ajoute 1 pour obtenir la position du joueur dans la course
        return position + 1;
    }
}
