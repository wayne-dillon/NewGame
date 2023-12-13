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
        XElement xml = XDocument.Load(DocName()).Element("preferences");

        preferences.musicVolume = (float)Convert.ToDecimal(xml.Element("musicVolume").Value);
        preferences.sfxVolume = (float)Convert.ToDecimal(xml.Element("sfxVolume").Value);
        preferences.fullScreen = Convert.ToBoolean(xml.Element("fullScreen").Value);
        preferences.resolution = Convert.ToInt32(xml.Element("resolution").Value);
        preferences.levelsComplete = Convert.ToInt32(xml.Element("levelsComplete").Value);

        PlayerMovementValues.horizontalAcceleration = Convert.ToInt32(xml.Element("hAcc").Value);
        PlayerMovementValues.horizontalDeceleration = Convert.ToInt32(xml.Element("hDec").Value);
        PlayerMovementValues.maxSpeed = Convert.ToInt32(xml.Element("maxSpeed").Value);
        PlayerMovementValues.dashSpeed = Convert.ToInt32(xml.Element("dashSpeed").Value);
        PlayerMovementValues.dashTime = Convert.ToInt32(xml.Element("dashTime").Value);
        PlayerMovementValues.dashDeceleration = Convert.ToInt32(xml.Element("dashDeceleration").Value);
        PlayerMovementValues.jumpSpeed = Convert.ToInt32(xml.Element("jumpSpeed").Value);
        PlayerMovementValues.jumpHoldTime = Convert.ToInt32(xml.Element("jumpHoldTime").Value);
        PlayerMovementValues.gravity = Convert.ToInt32(xml.Element("gravity").Value);
        PlayerMovementValues.maxFallSpeed = Convert.ToInt32(xml.Element("maxFallSpeed").Value);

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
            new("levelsComplete", preferences.levelsComplete),
            
            new("hAcc", PlayerMovementValues.horizontalAcceleration),
            new("hDec", PlayerMovementValues.horizontalDeceleration),
            new("maxSpeed", PlayerMovementValues.maxSpeed),
            new("dashSpeed", PlayerMovementValues.dashSpeed),
            new("dashTime", PlayerMovementValues.dashTime),
            new("dashDeceleration", PlayerMovementValues.dashDeceleration),
            new("jumpSpeed", PlayerMovementValues.jumpSpeed),
            new("jumpHoldTime", PlayerMovementValues.jumpHoldTime),
            new("gravity", PlayerMovementValues.gravity),
            new("maxFallSpeed", PlayerMovementValues.maxFallSpeed)
        };

        XDocument xml = new(new XDeclaration("1.0", "utf-8", null), new XElement("preferences", elements));
        xml.Save(DocName());
    }
}