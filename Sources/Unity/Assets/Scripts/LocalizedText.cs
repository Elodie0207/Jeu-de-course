using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))] // Requiert le composant Text
public class LocalizedText : MonoBehaviour
{
    public string translationKey; // Texte à chercher
    private Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<Text>();
    }

    private void Start()
    {
        translationManager.Instance.LanguageChanged += UpdateText;
    }

    private void OnEnable()
    {
        if (gameObject.activeSelf)
        {    
            UpdateText();
        }
    }

    private void OnDisable()
    {
        if (gameObject.activeSelf)
        {    
            translationManager.Instance.LanguageChanged -= UpdateText;
        }  
    }

    public void UpdateText()
    {
        if (translationManager.Instance != null && textComponent != null)
        {
            textComponent.text = translationManager.Instance.GetTranslation(translationKey);
        }
    }
}