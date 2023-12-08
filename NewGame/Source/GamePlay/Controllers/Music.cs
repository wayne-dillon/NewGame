using Microsoft.Xna.Framework.Media;

public class Music
{
    private static Song mainTheme;

    public Music()
    {
        mainTheme = Globals.content.Load<Song>("Sound//runAmok");
        MediaPlayer.Play(mainTheme);
        MediaPlayer.Volume = 0.25f;
    }

    public static void SetVolume(object SENDER, object INFO)
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
}