using Microsoft.Xna.Framework.Media;

public class Music
{
    private static Song menuTheme;
    private static Song gameTheme;
    private static MyTimer fadeTime;
    private static Song currentSong;

    public Music()
    {
        menuTheme = Globals.content.Load<Song>("Sound//menuLoop");
        gameTheme = Globals.content.Load<Song>("Sound//runAmok");
        PlayOnRepeat(menuTheme);
        MediaPlayer.Volume = Globals.musicVolume = 0.25f;
    }

    public static void SetTrack()
    {
        switch (Globals.gameState)
        {
            case GameState.GAME_PLAY:
                PlayOnRepeat(gameTheme);
                break;
            default:
                PlayOnRepeat(menuTheme);
                break;
        }
    }

    private static void PlayOnRepeat(Song SONG)
    {
        MediaPlayer.MediaStateChanged -= (SENDER, OBJECT) => MediaPlayer.Play(currentSong);

        currentSong = SONG;
        MediaPlayer.Play(currentSong);
        MediaPlayer.MediaStateChanged += (SENDER, OBJECT) => MediaPlayer.Play(currentSong);
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