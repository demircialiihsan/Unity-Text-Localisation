using UnityEngine;
using Localisation;

public class LanguageChanger : MonoBehaviour
{
    public void ChangeToEnglish()
    {
        LocalisationManager.ChangeLanguage(Language.English);
    }

    public void ChangeToTurkish()
    {
        LocalisationManager.ChangeLanguage(Language.Turkish);
    }

    public void ChangeToGerman()
    {
        LocalisationManager.ChangeLanguage(Language.German);
    }

    public void ChangeToSpanish()
    {
        LocalisationManager.ChangeLanguage(Language.Spanish);
    }
}
