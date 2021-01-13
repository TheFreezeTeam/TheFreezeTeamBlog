Title: EnsureConfigurationSection
Tags: 
  - CSharp 
  - Blazor 
  - dotnetcore 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

After breaking up your web.config into a logical `ConfigurationSection` now you want to actually update the web.config file.

So you open the web.config and ask yourself the following:

* What does the node name, need to be?  
* What attributes were on it?
* Where do I declare the section?

This little utility is here to help. Based on the simple principle of "It is easier to edit than it is to create!"  

The `EnsureConfigurationSection` method, will automatically create the section with the default values when you run the application.  After which, if you desire to modify the configuration you can just edit vs create.

Example Usage:

```
databaseSettingsConfigurationSection = ConfigurationUtilities.EnsureConfigurationSection<DatabaseSettingsConfigurationSection>(DatabaseSettingsConfigurationSection.SectionName);
```

The Source:
```
namespace Tft.FlashCards.Configuration
{
  using System;
  using System.Configuration;

  public class ConfigurationUtilities
  {

    public TConfigurationSection EnsureConfigurationSection<TConfigurationSection>(string aSectionName) where TConfigurationSection : ConfigurationSection
    {
      return (TConfigurationSection) EnsureConfigurationSection(aSectionName, typeof(TConfigurationSection));
    }

    private ConfigurationSection EnsureConfigurationSection(string aSectionName, Type aConfigurationSectionType)
    {
      var configurationSection = ConfigurationManager.GetSection(aSectionName) as ConfigurationSection;
      if (configurationSection == null)
      {
        Console.WriteLine($"Congifuration Manager did not find config section ({aSectionName}) and will create it.");
        Configuration config;
        if (System.Web.HttpContext.Current != null)
          config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path: "~");
        else
          config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        if (config.Sections[aSectionName] == null)
        {
          configurationSection = (ConfigurationSection) Activator.CreateInstance(aConfigurationSectionType);

          config.Sections.Add(aSectionName, configurationSection);
          configurationSection.SectionInformation.ForceSave = true;
          config.Save(ConfigurationSaveMode.Full);
          ConfigurationManager.RefreshSection(aSectionName);
          configurationSection = ConfigurationManager.GetSection(aSectionName) as ConfigurationSection;
        }
      }

      return configurationSection;
    }
  }
}
```

