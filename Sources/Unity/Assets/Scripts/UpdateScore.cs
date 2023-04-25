using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour {

    public List<Text> rankTexts; 
    public List<Text> scoreTexts; 

    public void UpdateRankAndScoreUI(List<int> positions, List<int> scores) {

        // trier les positions et les scores dans le même ordre
        List<KeyValuePair<int, int>> sortedData = new List<KeyValuePair<int, int>>();
        for (int i = 0; i < positions.Count; i++) {
            sortedData.Add(new KeyValuePair<int, int>(positions[i], scores[i]));
        }
        sortedData.Sort((x, y) => x.Key.CompareTo(y.Key));

        // mettre à jour les éléments texte
        for (int i = 0; i < sortedData.Count; i++) {
            rankTexts[i].text = "Rank " + (i+1).ToString() + ": " + sortedData[i].Key.ToString();
            scoreTexts[i].text = "Score: " + sortedData[i].Value.ToString();
        }
    }
}





