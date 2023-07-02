using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class translationManager : MonoBehaviour
{
    public event Action LanguageChanged;

    public static translationManager Instance { get; private set; }

    private string defaultLanguage = "fr";
    private string currentLanguage;
    private Dictionary<string, string> translations;

    private void Awake()
    {
        // creer instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // detruire instance et la recreer
            Destroy(gameObject);
            return;
        }

        // Définir la langue au lancement du jeu
        currentLanguage = defaultLanguage;
    }

    private void Start()
    {
        LoadLanguage();
    }
    private void LoadLanguage()
    {
        if (PlayerPrefs.HasKey("CurrentLanguage"))
        {
            currentLanguage = PlayerPrefs.GetString("CurrentLanguage");
        }
        else
        {
            currentLanguage = defaultLanguage;
        }

        
        LoadTranslations();
        LocalizedText[] localizedTexts = FindObjectsOfType<LocalizedText>();
        foreach (LocalizedText text in localizedTexts)
        {
            text.UpdateText();
        }
    }

    private void LoadTranslations()
    {
        string languageFile = "translation_" + currentLanguage;
        TextAsset textAsset = Resources.Load<TextAsset>("Translation/" + languageFile);

        if (textAsset != null)
        {
            string jsonContent = textAsset.text;
            translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent);
        }
        else
        {
            Debug.LogError("Fichier non trouvé: " + languageFile);
        }
    }

    public string GetTranslation(string key)
    {
        if (translations == null)
        {
            return key;
        }

        if (translations.TryGetValue(key, out string translation))
        {
            return translation;
        }

        Debug.LogWarning("La traduction pour " + key + " n'a pas été trouvée");
        return key;
    }

    // Changer la langue courante
    public void ChangeLanguage()
    {
        LanguageChanged?.Invoke();

        switch (currentLanguage)
        {
            case "en":
                currentLanguage = "fr";
                break;
            case "fr":
                currentLanguage = "en";
                break;
        }

        PlayerPrefs.SetString("CurrentLanguage", currentLanguage);
        LoadTranslations();
        
        LocalizedText[] localizedTexts = FindObjectsOfType<LocalizedText>();
        foreach (LocalizedText text in localizedTexts)
        {
            text.UpdateText();
        }
    }
    
}
