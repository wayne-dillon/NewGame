using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

public class SoundEffects
{
    private static SoundEffect buttonClick;
    private static SoundEffect ButtonClick
    {
        get 
        {
            buttonClick ??= Globals.content.Load<SoundEffect>("Sound//buttonClick");
            return buttonClick;
        }
    }

    public static void PlayButtonClick()
    {
        SoundEffectInstance instance = ButtonClick.CreateInstance();
        instance.Volume = Persistence.preferences.sfxVolume;
        instance.Play();
    }
}