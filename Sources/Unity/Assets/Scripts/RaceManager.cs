using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaceManager : MonoBehaviour {

    public List<FinishScript> racers;
    public List<int> positions;
    public List<int> scores = new List<int>{10, 7, 5, 3, 2};
    
    public UpdateScore updateRankAndScores;

    void Update () {
       
        positions.Clear();
        foreach (FinishScript racer in racers) {
            int positionScore = 0;
            if (positions.Count < scores.Count) { 
                positionScore = scores[positions.Count];
            }
            int position = 1;
            foreach (FinishScript otherRacer in racers) {
                if (otherRacer.nbTours > racer.nbTours) {
                    position++;
                } else if (otherRacer.nbTours == racer.nbTours && otherRacer.nbcheckpointsPasser > racer.nbcheckpointsPasser) {
                    position++;
                } else if (otherRacer.nbTours == racer.nbTours && otherRacer.nbcheckpointsPasser == racer.nbcheckpointsPasser) {
                    Vector3 otherRacerPosition = otherRacer.GetComponent<TrackPosition>().object1Position;
                    Vector3 racerPosition = racer.GetComponent<TrackPosition>().object1Position;
                    if (otherRacerPosition.z > racerPosition.z) {
                        position++;
                    }
                }
            }
            positions.Add(position);
            //ajoute le score en fonction de la position
            racer.score += positionScore;
        }
        
        List<FinishScript> sortedRacers = racers.OrderBy(racer => positions[racers.IndexOf(racer)]).ToList();

        // créer des listes de positions et de scores triées dans le même ordre
        List<int> sortedPositions = new List<int>();
        List<int> sortedScores = new List<int>();
        for (int i = 0; i < sortedRacers.Count; i++) {
            sortedPositions.Add(positions[racers.IndexOf(sortedRacers[i])]);
            sortedScores.Add(sortedRacers[i].score);
        }

        // mettre à jour les éléments texte dans la scène
        updateRankAndScores.UpdateRankAndScoreUI(sortedPositions, sortedScores);
    }
}






