//autheur: Lokossou Axel

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MultiBonusTest : MonoBehaviour
{
    public MultiControl MultiController;
    public RaceManager Manager;
   
    //Les images des différents bonus 
    public Image nitroImage;
    public Image superNitroImage;
    public Image gravityImage;
    public Image inversionImage;
    public Image freezeImage;
    private float RespawnTime = 5f; 
    

    //En rentrant en contact avec un gameobject avec un tag bonus cube, un des bonus ou malus est chosit aléatoirement
    //on affiche aussi en bas à droite de l'écran pendant 5 secondes une image indiquant ce que le joueur à reçu 
    private void OnTriggerEnter(Collider other)
    {
        int bonusType = UnityEngine.Random.Range(0, 5);

        if (other.CompareTag("BonusCube"))
        {
            Image bonusImage = null; 

            if (bonusType == 0)
            {
                StartCoroutine(MultiController.Nitro());
                bonusImage = nitroImage;
            }

            if (bonusType == 1)
            {
                StartCoroutine(MultiController.SuperNitro());
                bonusImage = superNitroImage;
            }

            if (bonusType == 2)
            {
                StartCoroutine(MultiController.Gravity());
                bonusImage = gravityImage;
            }

            if (bonusType == 3)
            {
                StartCoroutine(MultiController.Invert());
                bonusImage = inversionImage;
            }

            if (bonusType == 4)
            {
                StartCoroutine(Manager.Freeze());
                bonusImage = freezeImage;
            }    

            other.gameObject.SetActive(false);
            StartCoroutine(ReactivateBonusCube(other.gameObject, RespawnTime));

            // Activez l'image du bonus et la désactivez après 5 secondes
            StartCoroutine(ShowBonusImage(bonusImage, 5f));
        }
    }

    //Couroutine faisant réapparaitre le cube après 5 secondes
    private IEnumerator ReactivateBonusCube(GameObject bonusCube, float count)
    {
        yield return new WaitForSeconds(count);
        bonusCube.SetActive(true);
    }
    
    //Affiche l'image du bonus pendant 5 secondes
    private IEnumerator ShowBonusImage(Image bonusImage, float duration)
    {
        bonusImage.enabled = true;
        yield return new WaitForSeconds(duration);
        bonusImage.enabled = false; // Cache l'image du bonus après la durée spécifiée
    }
    
}