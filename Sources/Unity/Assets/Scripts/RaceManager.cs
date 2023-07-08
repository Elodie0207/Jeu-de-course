using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour 
{
    public List<FinishScript> racers;
    public List<int> positions;
    
    public Text[] nameTexts;
    public Text[] positionTexts;
    public Text[] scoreTexts;
    
    void Start() 
    {
        foreach (FinishScript racer in racers) 
        {
            if (!racer.gameObject.activeSelf) 
            {
                racer.enabled = false;
            }
        }
        
        racers.RemoveAll(racer => !racer.gameObject.activeSelf);
    }

    void Update () 
    {
        UpdateRacerPositions();
    }

    private void UpdateRacerPositions() 
    {
        positions.Clear();
        foreach (FinishScript racer in racers) 
        {
            int position = 1;
            foreach (FinishScript otherRacer in racers) 
            {
                if (otherRacer.nbTours > racer.nbTours) 
                {
                    position++;
                } 
                else if (otherRacer.nbTours == racer.nbTours && otherRacer.GetComponent<TrackPosition>().object1Position.z > racer.GetComponent<TrackPosition>().object1Position.z) 
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

    public void UpdateScore()
    {   
        FinishScript[] sortedRacers = racers.OrderBy(r => positions[racers.IndexOf(r)]).ToArray();

        for (int i = 0; i < sortedRacers.Length; i++)
        {
            FinishScript racer = sortedRacers[i];
            int racerIndex = racers.IndexOf(racer);

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

            nameTexts[i].text = racer.name;
            positionTexts[i].text = positions[racerIndex].ToString();
            scoreTexts[i].text = ScoreManager.Instance.GetScore(racer.name).ToString();
        }
    }

    public IEnumerator Freeze(float count = 3f)
    {
        FinishScript firstRacer = racers[positions.IndexOf(1)];

        CharacterController Controller = firstRacer.gameObject.GetComponent<CharacterController>();
        MultiControl MultiController = null;
        IA IAController = null;

        if (Controller == null) 
        {
            MultiController = firstRacer.gameObject.GetComponent<MultiControl>();
            if (MultiController == null)
            {
                IAController = firstRacer.gameObject.GetComponent<IA>();
            }
        }

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
