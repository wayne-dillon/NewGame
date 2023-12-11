using System;
using System.Collections.Generic;
using System.Xml.Linq;

public class Persistence
{
    public static Preferences preferences;

    private static string DocName() => "Source//Content//XML//Preferences.xml";

    public static void LoadPreferences()
    {
        preferences = new();
        XDocument xml = XDocument.Load(DocName());

        preferences.musicVolume = (float)Convert.ToDecimal(xml.Element("preferences").Element("musicVolume").Value);
        preferences.sfxVolume = (float)Convert.ToDecimal(xml.Element("preferences").Element("sfxVolume").Value);
        preferences.fullScreen = Convert.ToBoolean(xml.Element("preferences").Element("fullScreen").Value);
        preferences.resolution = Convert.ToInt32(xml.Element("preferences").Element("resolution").Value);
        preferences.levelsComplete = Convert.ToInt32(xml.Element("preferences").Element("levelsComplete").Value);

        if (preferences.fullScreen) Globals.graphics.ToggleFullScreen();
    }

    public static void SavePreferences()
    {
        List<XElement> elements = new()
        {
            new("musicVolume", preferences.musicVolume),
            new("sfxVolume", preferences.sfxVolume),
            new("fullScreen", preferences.fullScreen),
            new("resolution", preferences.resolution),
            new("levelsComplete", preferences.levelsComplete)
        };

        XDocument xml = new(new XDeclaration("1.0", "utf-8", null), new XElement("preferences", elements));
        xml.Save(DocName());
    }
}