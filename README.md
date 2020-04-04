# Unity-Text-Localisation
 String localisation system for Unity
 
 You can clone or download the project directly, or download the package from releases section and import it to your project.
 
 ## How To Use
 
  Use Localisation namespace wherever you want to use Localisation.
```csharp
using Localisation;
```

Declare a public LocalisedString value inside any attached script.

```csharp
public LocalisedString sample;
```

From the inspector, you can edit that Localised String on your attached script.

![ins](https://user-images.githubusercontent.com/32217921/78459327-ee2dd180-76c0-11ea-9690-0d448f015243.png)

Type a key and click edit button to localise a new value.

![unedited](https://user-images.githubusercontent.com/32217921/78459343-043b9200-76c1-11ea-8751-dcce74ce0598.png)

Enter the localised values for the key and click update button at the bottom of the prompted window.

![samplevalues](https://user-images.githubusercontent.com/32217921/78459444-b5dac300-76c1-11ea-8146-64aecd2c3680.png)

Note that the key now is in vivid color. This means that the key is valid.

![edited](https://user-images.githubusercontent.com/32217921/78459483-eae71580-76c1-11ea-988f-8fc7fab04208.png)

### Inserting New Line

Insert line breaks by hitting return key as usual.

![return](https://user-images.githubusercontent.com/32217921/78459531-50d39d00-76c2-11ea-837d-eb10b4590ea6.png)

Alternatively, it is possible to insert new lines by "\n". They will be converted to new lines automatically.

![backslashn](https://user-images.githubusercontent.com/32217921/78459556-82e4ff00-76c2-11ea-935d-6ae06a94690a.png)

 ## Add New Languages
 
 Open Assets/Localisation/Scripts/Languages.cs file. By default there are four languages defined.
 ```csharp
public enum Language
{
    English,
    Turkish,
    German,
    Spanish
}
``` 

 Add desired language(s).
 ```csharp
public enum Language
{
    English,
    Turkish,
    German,
    Spanish,
    Russian,
    Japanese
}
``` 

New languages will appear automatically in the edit window.

![newlangs](https://user-images.githubusercontent.com/32217921/78459639-128aad80-76c3-11ea-91c9-b0d2fd86f9e0.png)

 ## Delete Existing Languages
 
 Removing a language from the middle of the list will cause some conflicts. After removing language from enum, it is recommended to delete column of that language from Assets/Localisation/Resources/Localisation.csv via some software such as Excel. Also all the keys can be manually updated from the inspector as explained.
 
 **Being additive when building localisation system is strongly recommended.**

 ## Runtime Localisation

In runtime, you can get localised values from any script.
```csharp
string localisedString = LocalisationManager.GetLocalisedValue(sample.key);
``` 

Since English is the default language, former code should return the English value. You can change the language beforehand.
```csharp
LocalisationManager.ChangeLanguage(Language.German);
string germanSample = LocalisationManager.GetLocalisedValue(sample.key);
``` 

Subscribe your methods to OnLanguageChange event, and they will be called when language changes.
```csharp
public LocalisedString sample;
private string localisedString;

void OnEnable()
{
    LocalisationManager.OnLanguageChange += Localise;
}

void OnDisable()
{
    LocalisationManager.OnLanguageChange -= Localise;
}

void Localise()
{
    localisedString = LocalisationManager.GetLocalisedValue(sample.key);
}
``` 

 ## Easy Management
 
Search among existing keys via search button.

![searchbutton](https://user-images.githubusercontent.com/32217921/78459661-554c8580-76c3-11ea-80f0-11e7d369403a.png)

Chooese, edit or delete existing keys easily.

![editsearch](https://user-images.githubusercontent.com/32217921/78459734-b2483b80-76c3-11ea-989a-78af7254125d.png)

See the sample scene in Assets/Localisation/Sample folder.
