using UnityEngine;
using UnityEngine.UI;
using Localisation;

public class LocaliserExample : MonoBehaviour
{
    public LocalisedString sample;

    void Start()
    {
        LocaliseText();
    }

    void OnEnable()
    {
        LocalisationManager.OnLanguageChange += LocaliseText;
    }

    void OnDisable()
    {
        LocalisationManager.OnLanguageChange -= LocaliseText;
    }

    void LocaliseText()
    {
        GetComponent<Text>().text = LocalisationManager.GetLocalisedValue(sample.key);
    }
}
