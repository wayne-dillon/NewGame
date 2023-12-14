using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

public class SFXPlayer
{
    private static SoundEffect buttonClick = Globals.content.Load<SoundEffect>("Sound//buttonClick");
    private static SoundEffect jump = Globals.content.Load<SoundEffect>("Sound//jump");
    private static SoundEffect objective = Globals.content.Load<SoundEffect>("Sound//objectiveHit");
    private static SoundEffect dash = Globals.content.Load<SoundEffect>("Sound//dash");
    private static SoundEffect shock = Globals.content.Load<SoundEffect>("Sound//shock");
    private static SoundEffect swoosh = Globals.content.Load<SoundEffect>("Sound//swoosh");

    public static void PlaySound(SoundEffects sound)
    {
        SoundEffectInstance instance = sound switch
        {
            SoundEffects.BUTTON_CLICK => buttonClick.CreateInstance(),
            SoundEffects.OBJECTIVE_HIT => objective.CreateInstance(),
            SoundEffects.JUMP => jump.CreateInstance(),
            SoundEffects.DASH => dash.CreateInstance(),
            SoundEffects.SHOCK => shock.CreateInstance(),
            SoundEffects.SWOOSH => swoosh.CreateInstance(),
            _ => buttonClick.CreateInstance()
        };
        instance.Volume = Persistence.preferences.sfxVolume;
        instance.Play();
    }
}