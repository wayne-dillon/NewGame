using System;
using Microsoft.Xna.Framework;

public class GamePlay
{
    private Player player;
    private Camera camera;
    private Level level;
    private TextComponent timeDisplay;
    private int collected;
    private int timeBonus = 30;

    public TextComponent modeText;

    private TimeSpan runTime;
    private string TimerText
    { 
        get {
            if (runTime.TotalMilliseconds < 0) return "Time: 00:00.000";
            int minutes = (int)Math.Floor(runTime.TotalMinutes);
            int seconds = (int)Math.Floor(runTime.TotalSeconds % 60);
            int millis = (int)Math.Floor(runTime.TotalMilliseconds % 1000);
            return $"Time: {minutes}:{seconds:D2}.{millis:D3}"; 
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
        runTime = new(0,0,timeBonus);

        timeDisplay = new TextComponentBuilder().WithText(TimerText)
                                                .WithScreenAlignment(Alignment.TOP_RIGHT)
                                                .WithTextAlignment(Alignment.CENTER_RIGHT)
                                                .WithOffset(new Vector2(-75,75))
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
        if (Globals.isNewGame) 
        {
            Globals.isNewGame = false;
            ResetWorld(null, null);
        }
        modeText.Update(GameGlobals.currentMode.ToString());
        timeDisplay.Update(TimerText);
    }

    private void CheckEnd()
    {
        if (level.objectives.Count == collected)
        {
            GameGlobals.roundState = RoundState.END;
            GameGlobals.beatLevel = true;
        } else {
            runTime -= Globals.gameTime.ElapsedGameTime;
            if (runTime.TotalMilliseconds <= 0)
            {
                GameGlobals.roundState = RoundState.END;
            }
        }
    }

    private void CollectObjective(AnimatedSprite end)
    {
        if (end.hitbox.PassesThrough(player.prevHitbox, player.sprite.hitbox) != Direction.NONE && end.type == InteractableType.OBJECTIVE)
        {
            collected++;
            end.SetAnimationValues(0,0,20);
            end.type = InteractableType.NONE;
            runTime += new TimeSpan(0,0,timeBonus);
        }
    }

    public virtual void ResetWorld(object SENDER, object INFO)
    {
        if (INFO is bool won)
        {
            if (won && GameGlobals.currentLevel != LevelSelection.LEVEL_4) GameGlobals.currentLevel++;
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
        player.Draw();
        modeText.Draw();
        timeDisplay.Draw();
    }
}
