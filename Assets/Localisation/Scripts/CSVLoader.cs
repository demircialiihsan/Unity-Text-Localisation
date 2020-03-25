using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

namespace Localisation
{
    public class CSVLoader
    {
        private TextAsset csvFile;
        private char lineSeperator = '\n';
        private char surround = '"';
        private string[] fieldSeperator = { "\",\"" };

        public void LoadCSV()
        {
            csvFile = Resources.Load<TextAsset>("Localisation");
        }

        public Dictionary<string, string> GetDictionaryValues(string attributeID)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            string[] lines = csvFile.text.Split(lineSeperator);

            int attributeIndex = -1;

            string[] headers = lines[0].Split(fieldSeperator, StringSplitOptions.None);

            for (int i = 0; i < headers.Length; i++)
            {
                if (headers[i].Contains(attributeID))
                {
                    attributeIndex = i;
                    break;
                }
            }

            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = CSVParser.Split(lines[i]);

                for (int j = 0; j < fields.Length; j++)
                {
                    fields[j] = fields[j].TrimStart(' ', surround);
                    fields[j] = fields[j].Replace(surround.ToString(), "");
                }

                if (fields.Length > attributeIndex)
                {
                    string key = fields[0];

                    if (dictionary.ContainsKey(key))
                        continue;

                    string value = fields[attributeIndex];

                    dictionary.Add(key, value);
                }
            }
            return dictionary;
        }

#if UNITY_EDITOR
        public void Add(string key, string[] values)
        {
            string tmp = string.Join(fieldSeperator[0], values);
            string appended = string.Format("\n\"{0}\",\"{1}\"", key, tmp);
            File.AppendAllText("Assets/Localisation/Resources/Localisation.csv", appended);

            UnityEditor.AssetDatabase.Refresh();
        }

        public void Remove(string key)
        {
            string[] lines = csvFile.text.Split(lineSeperator);
            string[] keys = new string[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                keys[i] = lines[i].Split(fieldSeperator, StringSplitOptions.None)[0];
            }

            int index = -1;

            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].Contains(key))
                {
                    index = i;
                    break;
                }
            }

            if (index > -1)
            {
                string[] remaining = lines.Where(l => l != lines[index]).ToArray();

                string replaced = string.Join(lineSeperator.ToString(), remaining);
                File.WriteAllText("Assets/Localisation/Resources/Localisation.csv", replaced);
            }
        }

        public void Edit(string key, string[] values)
        {
            Remove(key);
            Add(key, values);
        }

        public void UpdateHeaders()
        {
            string[] lines = csvFile.text.Split(lineSeperator);

            string languageHeaders = string.Join(fieldSeperator[0].ToString(), Enum.GetNames(typeof(Language)));
            lines[0] = surround.ToString() + "key" + fieldSeperator[0] + languageHeaders + surround.ToString();

            string replaced = string.Join(lineSeperator.ToString(), lines);
            File.WriteAllText("Assets/Localisation/Resources/Localisation.csv", replaced);

            UnityEditor.AssetDatabase.Refresh();
        }
#endif
    }
}
