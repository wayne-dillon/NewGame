using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

public class AboutMenu
{
    private readonly List<LinkedButton> buttons = new();
    private readonly List<Button> levelEditorButtons = new();
    private readonly List<HighScoreDisplay> scoreDisplays = new();
    private Tab currentTab = Tab.OPTIONS;
    private readonly TextComponent storyText;
    private readonly TextComponent controlsDescText;
    private readonly TextComponent controlsText;
    private readonly TextComponent creditsText;
    private readonly TextComponent level1Text;
    private readonly TextComponent level2Text;
    private readonly TextComponent level3Text;
    private readonly TextComponent editorInstructionsText;

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

    private readonly string editorInstructions = "Click a tile to cycle through\n"
                                               + "the available options.\n\n"
                                               + "Use W,A,S,D or the directional\n"
                                               + "arrows to move around the level\n\n"
                                               + "To be completable levels need\n"
                                               + "a single spawn point, at least\n"
                                               + "one objective (phone) and at\n"
                                               + "least one starting tile.\n\n"
                                               + "There should also be an empty\n"
                                               + "space below the spawn point\n";

    private OptionsMenu options;
    private DevConsole devConsole;
    private EventHandler<object> resetEditor;

    private enum Tab
    {
        OPTIONS,
        STORY,
        CONTROLS,
        SCORES,
        CREDITS,
        LEVEL_EDITOR,
        DEV_CONSOLE
    }

    public AboutMenu(EventHandler<object> RESETEDITOR)
    {
        resetEditor = RESETEDITOR;
        storyText = new TextComponentBuilder().WithText(story).Build();
        creditsText = new TextComponentBuilder().WithText(credits).Build();
        controlsDescText = new TextComponentBuilder().WithText(controlsDesc)
                                                    .WithOffset(new Vector2(-400, 0)).Build();
        controlsText = new TextComponentBuilder().WithText(controls)
                                                .WithTextAlignment(Alignment.CENTER_RIGHT)
                                                .WithOffset(new Vector2(400, 0))
                                                .Build();

        editorInstructionsText = new TextComponentBuilder().WithText(editorInstructions)
                                            .WithTextAlignment(Alignment.CENTER_LEFT)
                                            .WithOffset(new Vector2(-500, 75))
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

        SpriteBuilder buttonBuilder = new SpriteBuilder().WithPath("UI//Button397x114")
                                                        .WithDims(new Vector2(220,40))
                                                        .WithScreenAlignment(Alignment.TOP_LEFT)
                                                        .WithHoverScale(new Vector2(1.01f, 1.01f))
                                                        .WithButtonAction(SwitchTabs);

        buttons.Add(buttonBuilder.WithText("How to Play").WithOffset(new Vector2(450,82)).WithButtonInfo(Tab.STORY).BuildLinkedButton());
        buttons.Add(buttonBuilder.WithText("Controls").WithOffset(new Vector2(680,82)).WithButtonInfo(Tab.CONTROLS).BuildLinkedButton());
        buttons.Add(buttonBuilder.WithText("High Scores").WithOffset(new Vector2(910,82)).WithButtonInfo(Tab.SCORES).BuildLinkedButton());
        buttons.Add(buttonBuilder.WithText("Credits").WithOffset(new Vector2(1140,82)).WithButtonInfo(Tab.CREDITS).BuildLinkedButton());
        buttons.Add(buttonBuilder.WithText("Level Editor").WithOffset(new Vector2(1370,82)).WithButtonInfo(Tab.LEVEL_EDITOR).BuildLinkedButton());
        buttons.Add(buttonBuilder.WithText("Dev Console").WithOffset(new Vector2(1600,82)).WithButtonInfo(Tab.DEV_CONSOLE).BuildLinkedButton());
        buttons.Add(buttonBuilder.WithText("Options").WithOffset(new Vector2(220,82)).WithButtonInfo(Tab.OPTIONS).WithAvailable(false).BuildLinkedButton());

        buttonBuilder = buttonBuilder.WithScreenAlignment(Alignment.CENTER).WithButtonAction(SwitchToEditor).WithAvailable(true);

        levelEditorButtons.Add(buttonBuilder.WithText("Level 1").WithOffset(new Vector2(250, -200)).WithButtonInfo(LevelSelection.LEVEL_1).BuildButton());
        levelEditorButtons.Add(buttonBuilder.WithText("Level 2").WithOffset(new Vector2(250, 0)).WithButtonInfo(LevelSelection.LEVEL_2).BuildButton());
        levelEditorButtons.Add(buttonBuilder.WithText("Level 3").WithOffset(new Vector2(250, 200)).WithButtonInfo(LevelSelection.LEVEL_3).BuildButton());

        levelEditorButtons.Add(buttonBuilder.WithText("Reset")
                                            .WithPath("UI//Button397x114")
                                            .WithDims(new Vector2(120,30))
                                            .WithOffset(new Vector2(450, -200))
                                            .WithButtonAction(ResetLevel)
                                            .WithButtonInfo(LevelSelection.LEVEL_1)
                                            .BuildButton());
        levelEditorButtons.Add(buttonBuilder.WithOffset(new Vector2(450, 0)).WithButtonInfo(LevelSelection.LEVEL_2).BuildButton());
        levelEditorButtons.Add(buttonBuilder.WithOffset(new Vector2(450, 200)).WithButtonInfo(LevelSelection.LEVEL_3).BuildButton());

        foreach (LinkedButton button in buttons)
        {
            button.SetLinkedList(buttons);
        }

        options = new();
        devConsole = new();

        PopulateHighScores();
    }

    public void Update()
    {
        if (TransitionManager.transState == TransitionState.BEGIN_IN)
        {
            PopulateHighScores();
        }

        foreach (LinkedButton button in buttons)
        {
            button.Update();
        }

        switch (currentTab)
        {
            case Tab.OPTIONS:
                options.Update();
                break;
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
            case Tab.LEVEL_EDITOR:
                editorInstructionsText.Update();
                foreach (Button button in levelEditorButtons)
                {
                    button.Update();
                }
                break;
            case Tab.DEV_CONSOLE:
                devConsole.Update();
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

    public void ResetLevel(object SENDER, object INFO)
    {
        if (INFO is LevelSelection level)
        {
            switch (level)
            {
                case LevelSelection.LEVEL_1:
                    CSVWriter.WriteFile(EnumHelper.GetLevelPath(level), CSVReader.ReadFile("Source//Content//Levels//Level1Base.csv"));
                    break;
                case LevelSelection.LEVEL_2:
                    CSVWriter.WriteFile(EnumHelper.GetLevelPath(level), CSVReader.ReadFile("Source//Content//Levels//Level2Base.csv"));
                    break;
                case LevelSelection.LEVEL_3:
                    CSVWriter.WriteFile(EnumHelper.GetLevelPath(level), CSVReader.ReadFile("Source//Content//Levels//Level3Base.csv"));
                    break;
            }
        }
    }

    public void OpenAsEditor(object SENDER, object INFO)
    {
        currentTab = Tab.LEVEL_EDITOR;
        TransitionManager.ChangeGameState(null, GameState.ABOUT);
    }

    public void SwitchTabs(object SENDER, object INFO)
    {
        if (INFO is Tab tab)
        {
            currentTab = tab;
        }
    }

    public void SwitchToEditor(object SENDER, object INFO)
    {
        if (INFO is LevelSelection level)
        {
            GameGlobals.currentLevel = level;
            resetEditor(null, null);
            TransitionManager.ChangeGameState(null, GameState.LEVEL_EDITOR);
        }
    }

    public void Draw()
    {
        foreach (LinkedButton button in buttons)
        {
            button.Draw();
        }

        switch (currentTab)
        {
            case Tab.OPTIONS:
                options.Draw();
                break;
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
            case Tab.LEVEL_EDITOR:
                editorInstructionsText.Draw();
                foreach (Button button in levelEditorButtons)
                {
                    button.Draw();
                }
                break;
            case Tab.DEV_CONSOLE:
                devConsole.Draw();
                break;
        }
    }
}