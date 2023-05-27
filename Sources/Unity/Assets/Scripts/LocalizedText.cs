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

    private void OnEnable()
    {
        if (gameObject.activeSelf)
        {    
            translationManager.Instance.LanguageChanged += UpdateText;
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
        if (translationManager.Instance == null)
        {
            Debug.LogWarning("translationManager.Instance is null in LocalizedText");
            return;
        }
        if (textComponent == null)
        {
            Debug.LogWarning("Text component is null in LocalizedText");
            return;
        }

        textComponent.text = translationManager.Instance.GetTranslation(translationKey);
    }
}