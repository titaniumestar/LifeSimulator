using UnityEngine;
using UnityEngine.UI;

public class LanguageLabel : MonoBehaviour
{
    public string labelKey;
    private Text text;
    public string language = "English";

    public void ChangeLabel(string newLanguage)
    {
        language = newLanguage;

        TextAsset[] languageFileTable = LanguageSystem.languageFile;
        LanguageData languageFile = JsonUtility.FromJson<LanguageData>(languageFileTable[0].text);

        for (int i = 0; i < languageFileTable.Length; i++) if (language == languageFileTable[i].name)
        {
            languageFile = JsonUtility.FromJson<LanguageData>(languageFileTable[i].text);
            break;
        }

        foreach (LabelsSubData value in languageFile.Labels)
        {
            if (value.Key == labelKey)
            {
                text.text = value.Word;
                break;
            }
        }
    }

    void Start()
    {
        text = gameObject.GetComponent<Text>();

        ChangeLabel(language);
    }

    void OnEnable()
    {
        LanguageSystem.LanguageChanged += ChangeLabel;
    }

    void OnDisable()
    {
        LanguageSystem.LanguageChanged -= ChangeLabel;
    }
}
