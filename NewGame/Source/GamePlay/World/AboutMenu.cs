using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class AboutMenu
{
    private readonly List<LinkedButton> buttons = new();
    private readonly List<HighScoreDisplay> scoreDisplays = new(); 
    private readonly Sprite background;
    private Tab currentTab = Tab.STORY;
    private readonly TextComponent storyText;
    private readonly TextComponent controlsDescText;
    private readonly TextComponent controlsText;
    private readonly TextComponent creditsText;
    private readonly TextComponent level1Text;
    private readonly TextComponent level2Text;
    private readonly TextComponent level3Text;

    private readonly string story = "Charlie is off on a well earned holiday, but some rude kittens on the flight\n"
                                  + "haven't switched their phones to airplane mode. In order to avoid CATastrophy\n"
                                  + "he'll have to tap into his many (more specifically, three) animalistic modes\n"
                                  + "in order to dash, hop, and climb his way around the cabin, righting these\n"
                                  + "egregious wrongs!"; 

    private readonly string controlsDesc = "Move Left/Right\n\n"
                                         + "Jump/Double Jump/Wall Jump\n\n"
                                         + "Dash\n\n"
                                         + "Change Mode (Previous/Next)";

    private readonly string controls = "A/D, or, Left/Right Arrow\n\n"
                                     + "W, or, Up Arrow\n\n"
                                     + "Space Bar\n\n"
                                     + "Q/E, or, Left/Right Mouse";

    private readonly string credits = "Programming, Gameplay and Level Design By\nWayne Dillon\n\n"
                                    + "Illustations and Animations By\nFairy Elina\n\n"
                                    + "Music and Sound Effects By\nAbraham Putnam"; 

    private enum Tab
    {
        STORY,
        CONTROLS,
        SCORES,
        CREDITS
    }

    public AboutMenu()
    {
        background = new SpriteBuilder().WithPath("rect")
                                        .WithColor(Colors.AirForceBlue)
                                        .WithDims(new Vector2(1400, 700))
                                        .Build();

        storyText = new TextComponentBuilder().WithText(story).Build();
        creditsText = new TextComponentBuilder().WithText(credits).Build();
        controlsDescText = new TextComponentBuilder().WithText(controlsDesc)
                                                    .WithOffset(new Vector2(-400, 0)).Build();
        controlsText = new TextComponentBuilder().WithText(controls)
                                                .WithTextAlignment(Alignment.CENTER_RIGHT)
                                                .WithOffset(new Vector2(400, 0))
                                                .Build();
                                                
        level1Text = new TextComponentBuilder().WithText("Level 1")
                                            .WithTextAlignment(Alignment.CENTER_LEFT)
                                            .WithOffset(new Vector2(-400, -150))
                                            .Build();
                                            
        level2Text = new TextComponentBuilder().WithText("Level 2")
                                            .WithTextAlignment(Alignment.CENTER_LEFT)
                                            .WithOffset(new Vector2(-400, 0))
                                            .Build();
                                            
        level3Text = new TextComponentBuilder().WithText("Level 3")
                                            .WithTextAlignment(Alignment.CENTER_LEFT)
                                            .WithOffset(new Vector2(-400, 150))
                                            .Build();

        SpriteBuilder buttonBuilder = new SpriteBuilder().WithPath("rect")
                                                        .WithDims(new Vector2(250,40))
                                                        .WithScreenAlignment(Alignment.TOP_LEFT)
                                                        .WithColor(Colors.Buttons)
                                                        .WithHoverColor(Colors.Hover)
                                                        .WithUnavailableColor(Colors.AirForceBlue)
                                                        .WithHoverScale(new Vector2(1.01f, 1.01f))
                                                        .WithButtonAction(SwitchTabs);

        buttons.Add(buttonBuilder.WithText("Controls").WithOffset(new Vector2(480,82)).WithButtonInfo(Tab.CONTROLS).BuildLinkedButton());
        buttons.Add(buttonBuilder.WithText("High Scores").WithOffset(new Vector2(735,82)).WithButtonInfo(Tab.SCORES).BuildLinkedButton());
        buttons.Add(buttonBuilder.WithText("Credits").WithOffset(new Vector2(990,82)).WithButtonInfo(Tab.CREDITS).BuildLinkedButton());
        buttons.Add(buttonBuilder.WithText("How to Play").WithOffset(new Vector2(225,82)).WithButtonInfo(Tab.STORY).WithAvailable(false).BuildLinkedButton());

        foreach (LinkedButton button in buttons)
        {
            button.SetLinkedList(buttons);
        }

        PopulateHighScores();
    }

    public void Update()
    {
        if (TransitionManager.transState == TransitionState.BEGIN_IN)
        {
            PopulateHighScores();
        }

        background.Update();
        foreach (LinkedButton button in buttons)
        {
            button.Update();
        }

        switch (currentTab)
        {
            case Tab.STORY:
                storyText.Update();
                break;
            case Tab.CONTROLS:
                controlsDescText.Update();
                controlsText.Update();
                break;
            case Tab.SCORES:
                level1Text.Update();
                level2Text.Update();
                level3Text.Update();
                foreach (HighScoreDisplay display in scoreDisplays)
                {
                    display.Update();
                }
                break;
            case Tab.CREDITS:
                creditsText.Update();
                break;
        }
    }

    public void PopulateHighScores()
    {
        scoreDisplays.Clear();
        scoreDisplays.Add(Scores.GetHighScores(LevelSelection.LEVEL_1, Alignment.CENTER, new Vector2(100, -150)));
        scoreDisplays.Add(Scores.GetHighScores(LevelSelection.LEVEL_2, Alignment.CENTER, new Vector2(100, 0)));
        scoreDisplays.Add(Scores.GetHighScores(LevelSelection.LEVEL_3, Alignment.CENTER, new Vector2(100, 150)));
    }

    public void SwitchTabs(object SENDER, object INFO)
    {
        if (INFO is Tab tab)
        {
            currentTab = tab;
        }
    }

    public void Draw()
    {
        background.Draw();
        foreach (LinkedButton button in buttons)
        {
            button.Draw();
        }

        switch (currentTab)
        {
            case Tab.STORY:
                storyText.Draw();
                break;
            case Tab.CONTROLS:
                controlsDescText.Draw();
                controlsText.Draw();
                break;
            case Tab.SCORES:
                level1Text.Draw();
                level2Text.Draw();
                level3Text.Draw();
                foreach (HighScoreDisplay display in scoreDisplays)
                {
                    display.Draw();
                }
                break;
            case Tab.CREDITS:
                creditsText.Draw();
                break;
        }
    }
}