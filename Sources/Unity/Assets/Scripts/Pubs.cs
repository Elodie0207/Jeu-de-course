using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Pubs : MonoBehaviour
{
    public bool fin = false;
    public float waitTime = 5f;
    public Image image;
    private bool canLoadNextScene = false;
    void Start()
    {
        image.gameObject.SetActive(false);
    }
    void FixedUpdate()
    {
        fin = FindObjectOfType<FinishScript>().fin;
        if (fin)
        {
            Debug.Log("ah");
            // Activer l'image
           
        }
        if (canLoadNextScene)
        {
            // Passer à la ligne suivante si la coroutine a terminé
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
    private IEnumerator WaitForNextScene()
    {
        // Attendre 5 secondes
        yield return new WaitForSeconds(waitTime);

        // Passer à la ligne suivante
        canLoadNextScene = true;
    }
}
