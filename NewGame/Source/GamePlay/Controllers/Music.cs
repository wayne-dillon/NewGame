using Microsoft.Xna.Framework.Media;

public class Music
{
    private static Song menuTheme;
    private static Song gameTheme;
    private static Song completionJingle;
    private static Song endJingle;
    private static MyTimer fadeTime;
    private static Song currentSong;

    public Music()
    {
        menuTheme = Globals.content.Load<Song>("Sound//menuLoop");
        gameTheme = Globals.content.Load<Song>("Sound//mainTheme");
        completionJingle = Globals.content.Load<Song>("Sound//completionJingle");
        endJingle = Globals.content.Load<Song>("Sound//endJingle");
        PlayOnRepeat(menuTheme);
        MediaPlayer.Volume = Persistence.preferences.musicVolume;
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

    public static void PlayCompletionJingle()
    {
        MediaPlayer.MediaStateChanged -= (SENDER, OBJECT) => MediaPlayer.Play(currentSong);
        MediaPlayer.Play(completionJingle);
        MediaPlayer.MediaStateChanged += (SENDER, OBJECT) => MediaPlayer.Play(currentSong);
    }

    public static void PlayEndJingle()
    {
        MediaPlayer.MediaStateChanged -= (SENDER, OBJECT) => MediaPlayer.Play(currentSong);
        MediaPlayer.Play(endJingle);
        MediaPlayer.MediaStateChanged += (SENDER, OBJECT) => MediaPlayer.Play(currentSong);
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
            MediaPlayer.Volume = Persistence.preferences.musicVolume = value;
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
        SetCurrentVolume(null, Persistence.preferences.musicVolume * percentage);
    }

    public static void FadeUp()
    {
        fadeTime.UpdateTimer();
        float percentage = fadeTime.RemainingTime <= 0 ? 1 : (float)fadeTime.Timer / (float)fadeTime.MSec;
        SetCurrentVolume(null, Persistence.preferences.musicVolume * percentage);
    }
}