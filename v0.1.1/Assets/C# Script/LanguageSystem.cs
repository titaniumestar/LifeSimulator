using System;
using UnityEngine;

[System.Serializable]
public class LabelsSubData
{
    public string Key;
    public string Word;
}

[System.Serializable]
public class LanguageData
{
    public LabelsSubData[] Labels;
}

public class LanguageSystem : MonoBehaviour
{
    public static event Action<string> LanguageChanged;

    public static TextAsset[] languageFile;

    public string currentLanguage = "English";

    public void LanguageUpdate(string language)
    {
        if (language == currentLanguage) return;
        currentLanguage = language;

        LanguageChanged?.Invoke(language);
    }

    void Awake()
    {
        languageFile = Resources.LoadAll<TextAsset>("Languages");
    }
}
