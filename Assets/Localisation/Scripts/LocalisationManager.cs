using UnityEngine;
using System.Collections.Generic;
using System;

namespace Localisation
{
    [System.Serializable]
    public struct LocalisedString
    {
        public string key;
    }

    public class LocalisationManager : MonoBehaviour
    {
        public static Action OnLanguageChange;
        private static Language language = Language.English;

        private static Dictionary<string, string>[] dictionaries;
        private static CSVLoader csvLoader;

        private static bool isInitialised;

        public static void Init()
        {
            csvLoader = new CSVLoader();

            csvLoader.LoadCSV();
            csvLoader.UpdateHeaders();
            csvLoader.LoadCSV();

            UpdateDictionaries();

            isInitialised = true;
        }

        public static void ChangeLanguage(Language language)
        {
            LocalisationManager.language = language;
            OnLanguageChange?.Invoke();
        }

        private static void UpdateDictionaries()
        {
            string[] languages = Enum.GetNames(typeof(Language));
            dictionaries = new Dictionary<string, string>[languages.Length];

            for (int i = 0; i < languages.Length; i++)
            {
                dictionaries[i] = csvLoader.GetDictionaryValues(languages[i]);
            }
        }

        public static string GetLocalisedValue(string key)
        {
            if (!isInitialised)
                Init();

            string value = key;
            dictionaries[(int)language].TryGetValue(key, out value);

            if (value != null)
                value = value.Replace("\\n", "\n");

            return value;
        }

#if UNITY_EDITOR
        public static string GetLocalisedValue(string key, Language language)
        {
            if (!isInitialised)
                Init();

            string value = key;
            dictionaries[(int)language].TryGetValue(key, out value);

            if (value != null)
                value = value.Replace("\\n", "\n");

            return value;
        }

        public static Dictionary<string, string> GetAnyDictionaryForKeys()
        {
            if (!isInitialised)
                Init();

            return dictionaries[0];
        }

        public static void Replace(string key, string[] values)
        {
            foreach (var value in values)
            {
                if (value.Contains("\""))
                    value.Replace('"', '\"');
            }

            if (csvLoader == null)
                csvLoader = new CSVLoader();

            csvLoader.LoadCSV();
            csvLoader.Edit(key, values);
            csvLoader.LoadCSV();

            UpdateDictionaries();
        }

        public static void Remove(string key)
        {
            if (csvLoader == null)
                csvLoader = new CSVLoader();

            csvLoader.LoadCSV();
            csvLoader.Remove(key);
            csvLoader.LoadCSV();

            UpdateDictionaries();
        }
#endif
    }
}
