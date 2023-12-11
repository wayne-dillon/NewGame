using System;
using Microsoft.Xna.Framework;

public class GamePlay
{
    private Player player;
    private Camera camera;
    private Level level;
    private TextComponent timeDisplay;
    private TextComponent addedTimeDisplay;
    private int collected;
    private readonly int startTime = 10;
    private readonly int timeBonus = 5;

    public TextComponent modeText;

    private TimeSpan runTime;
    private string TimerText
    { 
        get {
            if (runTime.TotalMilliseconds < 0) return "00.000";
            int seconds = (int)Math.Floor(runTime.TotalSeconds % 60);
            int millis = (int)Math.Floor(runTime.TotalMilliseconds % 1000);
            return $"{seconds:D2}.{millis:D3}"; 
        }
    }

    public GamePlay()
    {
        Init();
    }

    private void Init()
    {
        level = LevelBuilder.Build();

        player = new Player(level.playerStartPos);
        camera = new Camera(player.sprite, level);
        runTime = new(0,0,startTime);

        timeDisplay = new TextComponentBuilder().WithText(TimerText)
                                                .WithScreenAlignment(Alignment.TOP_RIGHT)
                                                .WithTextAlignment(Alignment.CENTER_RIGHT)
                                                .WithOffset(new Vector2(-75,75))
                                                .WithFont(Fonts.numberFont)
                                                .Build();

        addedTimeDisplay = new TextComponentBuilder().WithText($"+{timeBonus}.000")
                                                .WithScreenAlignment(Alignment.TOP_RIGHT)
                                                .WithTextAlignment(Alignment.CENTER_RIGHT)
                                                .WithOffset(new Vector2(-75,100))
                                                .WithFont(Fonts.numberFont)
                                                .Build();

        modeText = new TextComponentBuilder().WithScreenAlignment(Alignment.TOP)
                                            .WithOffset(new Vector2(0, 30))
                                            .Build();

        collected = 0;

        GameGlobals.roundState = RoundState.START;
        GameGlobals.beatLevel = false;
    }

    public virtual void Update()
    {
        player.Update();
        camera.Update();
        level.backdrop.Update();
        foreach (Sprite start in level.startBlocks)
        {
            start.Update();
            if (GameGlobals.roundState == RoundState.START)
            {
                if (start.hitbox.PassesThrough(player.prevHitbox, player.sprite.hitbox) != Direction.NONE)
                {
                    GameGlobals.roundState = RoundState.TIMER_RUNNING;
                }
            }
        }
        foreach (AnimatedSprite objective in level.objectives)
        {
            objective.Update();
            if (GameGlobals.roundState == RoundState.TIMER_RUNNING)
            {
                CollectObjective(objective);
            }
        }
        if (GameGlobals.roundState == RoundState.TIMER_RUNNING)
        {
            CheckEnd();
        }
        foreach (Sprite platform in level.platforms)
        {
            platform.Update();
        }
        foreach (Sprite hazard in level.hazards)
        {
            hazard.Update();
        }
        foreach (TextComponent text in level.text)
        {
            text.Update();
        }
        if (Globals.isNewGame) 
        {
            Globals.isNewGame = false;
            ResetWorld(null, null);
        }
        modeText.Update(GameGlobals.currentMode.ToString());
        timeDisplay.Update(TimerText);
        addedTimeDisplay.Update();
    }

    private void CheckEnd()
    {
        if (level.objectives.Count == collected)
        {
            GameGlobals.roundState = RoundState.END;
            GameGlobals.beatLevel = true;
            if (Persistence.preferences.levelsComplete == (int)GameGlobals.currentLevel)
            {
                Persistence.preferences.levelsComplete++;
                Persistence.SavePreferences();
            }

            Scores.LevelScores.Add(new RunDetails((int)runTime.TotalMilliseconds, "You", GameGlobals.currentLevel, DateTime.Now));
        } else {
            runTime -= Globals.gameTime.ElapsedGameTime;
            if (runTime.TotalMilliseconds <= 0)
            {
                GameGlobals.roundState = RoundState.END;
            }
        }
    }

    private void CollectObjective(AnimatedSprite OBJ)
    {
        if (OBJ.hitbox.PassesThrough(player.prevHitbox, player.sprite.hitbox) != Direction.NONE && OBJ.type == InteractableType.OBJECTIVE)
        {
            collected++;
            OBJ.SetAnimationValues(0,0,20);
            OBJ.type = InteractableType.NONE;
            runTime += new TimeSpan(0,0,timeBonus);

            addedTimeDisplay.color.A = 255;
            addedTimeDisplay.animation = new FadeOut(750);
        }
    }

    public virtual void ResetWorld(object SENDER, object INFO)
    {
        if (INFO is bool won)
        {
            if (won && GameGlobals.currentLevel != LevelSelection.LEVEL_3) GameGlobals.currentLevel++;
        }
        Init();
    }

    public virtual void Draw()
    {
        level.backdrop.Draw();
        foreach (Sprite start in level.startBlocks)
        {
            start.Draw();
        }
        foreach (Sprite end in level.objectives)
        {
            end.Draw();
        }
        foreach (Sprite hazard in level.hazards)
        {
            hazard.Draw();
        }
        foreach (Sprite platform in level.platforms)
        {
            platform.Draw();
        }
        foreach (TextComponent text in level.text)
        {
            text.Draw();
        }
        player.Draw();
        modeText.Draw();
        timeDisplay.Draw();
        if (addedTimeDisplay.animation != null)
            addedTimeDisplay.Draw();
    }
}
