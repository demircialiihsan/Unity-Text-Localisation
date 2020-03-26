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

![ins](https://user-images.githubusercontent.com/32217921/77640598-bc539700-6f6b-11ea-8921-67bc720e27a6.png)

Type a key and click edit button to localise new value.

![unedited](https://user-images.githubusercontent.com/32217921/77641026-692e1400-6f6c-11ea-91b7-549f6591b9a3.png)

Enter localised values for the key and click update button on the bottom of the prompted window.

![samplevalues](https://user-images.githubusercontent.com/32217921/77641591-5700a580-6f6d-11ea-9766-4b675f618703.png)

Note that the key is now in black color. This means that the key is valid.

![edited](https://user-images.githubusercontent.com/32217921/77643621-c62bc900-6f70-11ea-9c21-fc0edbe52a5d.png)

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

![newlangs](https://user-images.githubusercontent.com/32217921/77647710-f6c33100-6f77-11ea-916f-39a83c6ad96e.png)

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

Search among existing keys via search button.

![searchbutton](https://user-images.githubusercontent.com/32217921/77644243-c5476700-6f71-11ea-8442-d330444baa90.png)

Edit or delete existing keys easily.

![editsearch](https://user-images.githubusercontent.com/32217921/77644545-3555ed00-6f72-11ea-822b-0483033e23dc.png)

See the sample scene in Assets/Localisation/Sample folder.
