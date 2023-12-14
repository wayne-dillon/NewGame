using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class OptionsMenu
{
    private readonly TextComponent fullscreenText, resolutionText, musicVolumeText, sfxVolumeText;
    private readonly Checkbox fullscreenCheckbox;
    private readonly List<LinkedCheckbox> resolutionCheckboxs = new();
    private readonly Slider musicVolumeSlider;
    private readonly Slider sfxVolumeSlider;

    private static readonly Vector2 res1920x1080 = new(1920, 1080);
    private static readonly Vector2 res1600x900 = new(1600, 900);
    private static readonly Vector2 res1280x720 = new(1280, 720);

    public OptionsMenu()
    {
        musicVolumeText = new TextComponentBuilder().WithText("Music Volume")
                                                .WithTextAlignment(Alignment.CENTER_LEFT)
                                                .WithOffset(new Vector2(-200, 0))
                                                .Build();
        sfxVolumeText = new TextComponentBuilder().WithText("SFX Volume")
                                                .WithTextAlignment(Alignment.CENTER_LEFT)
                                                .WithOffset(new Vector2(-200, -80))
                                                .Build();
        fullscreenText = new TextComponentBuilder().WithText("Fullscreen")
                                                .WithTextAlignment(Alignment.CENTER_LEFT)
                                                .WithOffset(new Vector2(-200, 80))
                                                .Build();
        resolutionText = new TextComponentBuilder().WithText("Resolution")
                                                .WithTextAlignment(Alignment.CENTER_LEFT)
                                                .WithOffset(new Vector2(-200, 160))
                                                .Build();

        musicVolumeSlider = new Slider(Alignment.CENTER, new Vector2(100, 0), Music.GetVolume(), Music.SetPreferredVolume);
        sfxVolumeSlider = new Slider(Alignment.CENTER, new Vector2(100, -80), Persistence.preferences.sfxVolume,
                                    (sender, info) => { Persistence.preferences.sfxVolume = (float)info; });

        fullscreenCheckbox = new SpriteBuilder().WithOffset(new Vector2(100, 40))
                                                .WithButtonAction(UpdateFullscreen)
                                                .WithChecked(Persistence.preferences.fullScreen)
                                                .BuildCheckbox();

        resolutionCheckboxs.Add(new SpriteBuilder().WithText("1920 x 1080")
                                                    .WithOffset(new Vector2(100, 150))
                                                    .WithButtonAction(UpdateResolution)
                                                    .WithButtonInfo(res1920x1080)
                                                    .WithChecked(Persistence.preferences.resolution == 1080)
                                                    .BuildLinkedCheckbox());
        resolutionCheckboxs.Add(new SpriteBuilder().WithText("1600 x 900")
                                                    .WithOffset(new Vector2(100, 175))
                                                    .WithButtonAction(UpdateResolution)
                                                    .WithButtonInfo(res1600x900)
                                                    .WithChecked(Persistence.preferences.resolution == 900)
                                                    .BuildLinkedCheckbox());
        resolutionCheckboxs.Add(new SpriteBuilder().WithText("1280 x 720")
                                                    .WithOffset(new Vector2(100, 200))
                                                    .WithButtonAction(UpdateResolution)
                                                    .WithButtonInfo(res1280x720)
                                                    .WithChecked(Persistence.preferences.resolution == 720)
                                                    .BuildLinkedCheckbox());

        foreach (LinkedCheckbox box in resolutionCheckboxs)
        {
            box.SetLinkedList(resolutionCheckboxs);
        }

        switch (Persistence.preferences.resolution)
        {
            case 720:
                UpdateResolution(null, res1280x720);
                break;
            case 900:
                UpdateResolution(null, res1600x900);
                break;
            default:
                break;
        }
    }

    public void Update()
    {
        sfxVolumeText.Update();
        sfxVolumeSlider.Update();
        musicVolumeText.Update();
        musicVolumeSlider.Update();
        fullscreenText.Update();
        fullscreenCheckbox.Update();
        resolutionText.Update();
        foreach (LinkedCheckbox box in resolutionCheckboxs)
        {
            box.Update();
        }
    }

    public void UpdateResolution(object SENDER, object INFO)
    {
        if (INFO is Vector2 ratio)
        {
            if (ratio == res1920x1080) Globals.defaultFont = Fonts.defaultFont24;
            if (ratio == res1600x900) Globals.defaultFont = Fonts.defaultFont18;
            if (ratio == res1280x720) Globals.defaultFont = Fonts.defaultFont12;

            Globals.screenWidth = (int)ratio.X;
            Globals.screenHeight = (int)ratio.Y;

            Globals.graphics.PreferredBackBufferWidth = Globals.screenWidth;
            Globals.graphics.PreferredBackBufferHeight = Globals.screenHeight;

            Globals.graphics.ApplyChanges();

            Persistence.preferences.resolution = (int)ratio.Y;
        }
    }

    public void UpdateFullscreen(object SENDER, object INFO)
    {
        Globals.graphics.ToggleFullScreen();
        Persistence.preferences.fullScreen = !Persistence.preferences.fullScreen;
    }

    public void Draw()
    {
        sfxVolumeText.Draw();
        sfxVolumeSlider.Draw();
        musicVolumeText.Draw();
        musicVolumeSlider.Draw();
        fullscreenText.Draw();
        fullscreenCheckbox.Draw();
        resolutionText.Draw();
        foreach (LinkedCheckbox box in resolutionCheckboxs)
        {
            box.Draw();
        }
    }
}