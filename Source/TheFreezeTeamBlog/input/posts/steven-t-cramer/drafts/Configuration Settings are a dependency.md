Title: Configuration Settings are a dependency
Tags: 
  - C# 
  - Blazor 
  - dotnet 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

[Configuration Settings are a dependency that should be injected.](http://www.devtrends.co.uk/blog/configuration-settings-are-a-dependency-that-should-be-injected) 

We simplify the web.config by:

* using `ConfigurationSection`
* initialize the web config to avoid errors by creating default settings, 
* build to an interface and allow for injection of the dependency.
* Break up the sections of your configuration files into meaningful groups.  

This is how we get the Database Settings for an application.

Add a `Configuration` Folder to your project.

![](/content/images/2016/06/ConfigurationFolder.png)

## Add IDatabaseSettings.cs to the `Configuration` Folder

This interface defines all the items we may want to configure.
(Note: In integration testing you can point to a test database)
```
namespace Tft.FlashCards.Configuration
{
  public interface IDatabaseSettings
  {
    string ServerName { get; }
    string DatabaseName { get; }
    bool IntegratedSecurity { get; }
    string UserId { get; }
    string Password { get; }
    string MetadataModelName { get; }
    string SqlConnectionString { get; }
    string EntityConnectionString { get; }
  }
}
```

## Add DatabaseSettingsConfigurationSection.cs to the `Configuration` Folder
```
namespace Tft.Entities.Configuration
{
	using System;
	using System.Configuration;
	public class DatabaseSettingsConfigurationSection : ConfigurationSection, IDatabaseSettings
	{
		public const string SectionName = "Tft.DatabaseSettings";

		private const string ServerNamePropertyName = "ServerName";
		private const string DatabaseNamePropertyName = "DatabaseName";

		private static bool constructing = false; 
		public DatabaseSettingsConfigurationSection()
		{
			if (!constructing)
			{
				constructing = true; // Used to avoid construction loop as GetSection will create this class
				var databaseSettingsConfigurationSection = ConfigurationManager.GetSection(SectionName) as DatabaseSettingsConfigurationSection;
				if (databaseSettingsConfigurationSection == null)
				{
					CreateConfigurationSection();
				}
				else
				{
					this.ServerName = databaseSettingsConfigurationSection.ServerName;
					this.DatabaseName = databaseSettingsConfigurationSection.DatabaseName;
				}

				constructing = false;
			}
		}

		[ConfigurationProperty(ServerNamePropertyName, IsRequired = true)]
		public string ServerName
		{
			get
			{
				return this[ServerNamePropertyName].ToString();
			}
			set
			{
				this[ServerNamePropertyName] = value;
			}
		}

		[ConfigurationProperty(DatabaseNamePropertyName, IsRequired = true)]
		public string DatabaseName
		{
			get
			{
				return this[DatabaseNamePropertyName].ToString();
			}
			set
			{
				this[DatabaseNamePropertyName] = value;
			}
		}

		private void SetDefaults()
		{
			ServerName = "localhost";
			DatabaseName = "Tft";
		}

		/// <summary>
		/// This is done so that a default implementation is saved to the web.config or app.config
		/// which makes it easier to edit.
		/// </summary>
		private void CreateConfigurationSection()
		{
			Configuration config;
			Console.WriteLine("ConfigurationManager did not find config section ({0}) in your configuration file and will create it.", SectionName);
			
			if (System.Web.HttpContext.Current != null)
				config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
			else
				config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			if (config.Sections[SectionName] == null)
			{
				SetDefaults();

				config.Sections.Add(SectionName, this);
				this.SectionInformation.ForceSave = true;
				config.Save(ConfigurationSaveMode.Full);
				ConfigurationManager.RefreshSection(SectionName);
			}
		}
	}
}

```

When doing integration testing of the application, we can then register an implementation with the Ioc.container.

```
namespace Tft.Entities.Configuration
{

	class IntegrationTestDatabaseSettings : IDatabaseSettings
	{
		public string DatabaseName
		{
			get
			{
				return "localhost";
			}
		}

		public string ServerName
		{
			get
			{
				return "TftTest";
			}
		}
	}
}

``` 





