using Microsoft.Xna.Framework.Media;

public class Music
{
    private static Song mainTheme;
    private static MyTimer fadeTime;

    public Music()
    {
        mainTheme = Globals.content.Load<Song>("Sound//runAmok");
        MediaPlayer.Play(mainTheme);
        MediaPlayer.Volume = Globals.musicVolume = 0.25f;
    }

    public static void SetPreferredVolume(object SENDER, object INFO)
    {
        if (INFO is float value)
        {
            MediaPlayer.Volume = Globals.musicVolume = value;
        }
    }

    public static void SetCurrentVolume(object SENDER, object INFO)
    {
        if (INFO is float value)
        {
            MediaPlayer.Volume = value;
        }
    }

    public static float GetVolume()
    {
        return MediaPlayer.Volume;
    }

    public static void SetFadeTime(int TIME)
    {
        fadeTime = new MyTimer(TIME);
    }

    public static void FadeDown()
    {
        fadeTime.UpdateTimer();
        float percentage = fadeTime.RemainingTime <= 0 ? 0 : (float)fadeTime.RemainingTime / (float)fadeTime.MSec;
        SetCurrentVolume(null, Globals.musicVolume * percentage);
    }

    public static void FadeUp()
    {
        fadeTime.UpdateTimer();
        float percentage = fadeTime.RemainingTime <= 0 ? 1 : (float)fadeTime.Timer / (float)fadeTime.MSec;
        SetCurrentVolume(null, Globals.musicVolume * percentage);
    }
}