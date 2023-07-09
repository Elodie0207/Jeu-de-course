
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class RaceManager : MonoBehaviour 
{
    // Cette liste contient tous les joueurs de la course.
    public List<FinishScript> racers;
    
    public List<int> positions;
    
   
    public Text[] nameTexts;
    public Text[] positionTexts;
    public Text[] scoreTexts;
    

    public Text topScore1Text;
    public Text topScore2Text;
    public Text topScore3Text;
    
    void Start() 
    {
        // Au début de la partie, le script s'assure que seuls les joueurs actifs sont présents dans la liste des joueurs.
        foreach (FinishScript racer in racers) 
        {
            if (!racer.gameObject.activeSelf) 
            {
                racer.enabled = false;
            }
        }
        
        // Les joueurs non actifs sont supprimés de la liste.
        racers.RemoveAll(racer => !racer.gameObject.activeSelf);
    }

    void Update () 
    {
    
        UpdateRacerPositions();
    }

    // Cette méthode est utilisée pour mettre à jour les positions des joueurs.
    private void UpdateRacerPositions() 
    {
        positions.Clear();
        foreach (FinishScript racer in racers) 
        {
            int position = 1;
            foreach (FinishScript otherRacer in racers) 
            {
                if (otherRacer.nbTours > racer.nbTours || 
                    (otherRacer.nbTours == racer.nbTours && 
                    otherRacer.GetComponent<TrackPosition>().object1Position.z > racer.GetComponent<TrackPosition>().object1Position.z)) 
                {
                    position++;
                } 
            }
            positions.Add(position);
        }

      
        for (int i = 0; i < positions.Count; i++) 
        {
            positions[i] = positions.Count(p => p < positions[i]) + 1;
        }
    }

    // Cette méthode met à jour le score des joueurs en fonction de leur position.
    public void UpdateScore()
    {   
        FinishScript[] sortedRacers = racers.OrderBy(r => positions[racers.IndexOf(r)]).ToArray();

        for (int i = 0; i < sortedRacers.Length; i++)
        {
            FinishScript racer = sortedRacers[i];
            int racerIndex = racers.IndexOf(racer);

            // Attribution des points en fonction de la position du joueur.
            switch (positions[racerIndex])
            {
                case 1:
                    ScoreManager.Instance.UpdateScore(racer.name, 10);
                    break;
                case 2:
                    ScoreManager.Instance.UpdateScore(racer.name, 7);
                    break;
                case 3:
                    ScoreManager.Instance.UpdateScore(racer.name, 5);
                    break;
                case 4:
                    ScoreManager.Instance.UpdateScore(racer.name, 3);
                    break;
                case 5:
                    ScoreManager.Instance.UpdateScore(racer.name, 2);
                    break;
            }

            // Mise à jour des informations du joueur dans l'interface utilisateur.
            nameTexts[i].text = racer.name;
            positionTexts[i].text = positions[racerIndex].ToString();
            scoreTexts[i].text = ScoreManager.Instance.GetScore(racer.name).ToString();
        }
    }
    
    // Cette méthode met à jour le podium en affichant les trois meilleurs scores.
    public void UpdatePodium()
    {
        Dictionary<string, int> topScores = ScoreManager.Instance.GetTopScores(3);
        List<KeyValuePair<string, int>> topScoresList = topScores.ToList();

        if (topScoresList.Count > 0)
        {
            topScore1Text.text = topScoresList[0].Key;
        }
    
        if (topScoresList.Count > 1)
        {
            topScore2Text.text = topScoresList[1].Key;
        }
    
        if (topScoresList.Count > 2)
        {
            topScore3Text.text = topScoresList[2].Key;
        }
    }
    
    // Cette coroutine est utilisée pour "geler" le premier joueur pendant un certain temps.
    public IEnumerator Freeze(float count = 3f)
    {
        FinishScript firstRacer = racers[positions.IndexOf(1)];

        CharacterController Controller = firstRacer.gameObject.GetComponent<CharacterController>();
        MultiControl MultiController = null;
        IA IAController = null;

        // Vérification du type de contrôleur du joueur.
        if (Controller == null) 
        {
            MultiController = firstRacer.gameObject.GetComponent<MultiControl>();
            if (MultiController == null)
            {
                IAController = firstRacer.gameObject.GetComponent<IA>();
            }
        }

        // Désactivation temporaire du contrôleur.
        if (Controller != null)
        {
            Controller.enabled = false;
        }
        else if (MultiController != null)
        {
            MultiController.enabled = false;
        }
        else if (IAController != null)
        {
            IAController.enabled = false;
        }

        yield return new WaitForSeconds(count);

      
        if (Controller != null)
        {
            Controller.enabled = true;
        }
        else if (MultiController != null)
        {
            MultiController.enabled = true;
        }
        else if (IAController != null)
        {
            IAController.enabled = true;
        }
    }
}
