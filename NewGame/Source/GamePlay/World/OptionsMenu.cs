using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class OptionsMenu
{
    private readonly Sprite background;
    private readonly TextComponent fullscreenText, resolutionText;
    private readonly Checkbox fullscreenCheckbox;
    private readonly List<LinkedCheckbox> resolutionCheckboxs = new();

    public OptionsMenu()
    {
        background = new SpriteBuilder().WithPath("rect").WithDims(new Vector2(600, 500)).WithColor(Colors.Background).Build();

        fullscreenText = new TextComponentBuilder().WithText("Fullscreen")
                                                .WithTextAlignment(Alignment.CENTER_LEFT)
                                                .WithOffset(new Vector2(-200, -75))
                                                .Build();
        resolutionText = new TextComponentBuilder().WithText("Resolution")
                                                .WithTextAlignment(Alignment.CENTER_LEFT)
                                                .WithOffset(new Vector2(-200, 25))
                                                .Build();

        fullscreenCheckbox = new SpriteBuilder().WithOffset(new Vector2(100, -75))
                                                .WithButtonAction((sender, args) => { Globals.graphics.ToggleFullScreen(); })
                                                .BuildCheckbox();

        resolutionCheckboxs.Add(new SpriteBuilder().WithText("1600 x 900")
                                                    .WithOffset(new Vector2(100, 50))
                                                    .WithButtonAction(UpdateResolution)
                                                    .WithButtonInfo(new Vector2(1600, 900))
                                                    .WithChecked(true)
                                                    .BuildLinkedCheckbox());
        resolutionCheckboxs.Add(new SpriteBuilder().WithText("1920 x 1080")
                                                    .WithOffset(new Vector2(100, 75))
                                                    .WithButtonAction(UpdateResolution)
                                                    .WithButtonInfo(new Vector2(1920, 1080))
                                                    .BuildLinkedCheckbox());

        foreach (LinkedCheckbox box in resolutionCheckboxs)
        {
            box.SetLinkedList(resolutionCheckboxs);
        }
    }

    public void Update()
    {
        background.Update();
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
            Globals.screenWidth = (int)ratio.X;
            Globals.screenHeight = (int)ratio.Y;

            Globals.graphics.PreferredBackBufferWidth = Globals.screenWidth;
            Globals.graphics.PreferredBackBufferHeight = Globals.screenHeight;

            Globals.graphics.ApplyChanges();
        }
    }

    public void Draw()
    {
        background.Draw();
        fullscreenText.Draw();
        fullscreenCheckbox.Draw();
        resolutionText.Draw();
        foreach (LinkedCheckbox box in resolutionCheckboxs)
        {
            box.Draw();
        }
    }
}