using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Autres : MonoBehaviour
{
    public GameObject inputFieldObj; // Référence à l'objet InputField dans l'éditeur Unity
    private InputField inputField;
    private string userInput; // Variable pour stocker le texte saisi par l'utilisateur
    public  bool cheatcode = false;
   
    void Start()
    {
        inputField = inputFieldObj.GetComponent<InputField>(); // Obtenez la référence à InputField
        inputField.gameObject.SetActive(false); // Désactivez-le au démarrage
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.K))
        {
            // Activez InputField et donnez-lui le focus
            inputField.gameObject.SetActive(true);
            inputField.ActivateInputField();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    // Cette fonction est appelée lorsque l'utilisateur appuie sur Entrée dans InputField
    public void OnEndEdit()
    {
        // Récupérez le texte saisi par l'utilisateur
        userInput = inputField.text;
        
        // Faites ce que vous voulez avec le texte saisi ici, comme le sauvegarder ou l'utiliser pour autre chose.
        Debug.Log("Input text: " + userInput);

        // Désactivez InputField
        
        GameObject inputValue = FindObjectOfType<FinishScript>().tour;
        inputValue.GetComponent<Text>().text = userInput;
        cheatcode = true; 
        inputField.gameObject.SetActive(false);
    }
}