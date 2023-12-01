using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class GamePlay
{
    private Player player;
    private Camera camera;
    private Level level;
    private TextComponent timeDisplay;
    private int collected;
    private int timeBonus = 30;

    private TimeSpan runTime;
    private string TimerText
    { 
        get {
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
        level = LevelBuilder.Build("Source//Content//Levels//Level1.csv");

        player = new Player(level.playerStartPos);
        camera = new Camera(player.sprite);
        runTime = new(0,0,timeBonus);

        timeDisplay = new TextComponentBuilder().WithText(TimerText)
                                                .WithScreenAlignment(Alignment.TOP_RIGHT)
                                                .WithTextAlignment(Alignment.CENTER_RIGHT)
                                                .WithOffset(new Vector2(-75,75))
                                                .Build();

        collected = 0;

        GameGlobals.roundState = RoundState.START;
    }

    public virtual void Update()
    {
        player.Update();
        camera.Update();
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
        timeDisplay.Update(TimerText);
    }

    private void CheckEnd()
    {
        if (level.objectives.Count == collected)
        {
            GameGlobals.roundState = RoundState.END;
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
            end.SetRange(0,0);
            end.type = InteractableType.NONE;
            runTime += new TimeSpan(0,0,timeBonus);
        }
    }

    public virtual void ResetWorld(object SENDER, object INFO)
    {
        Init();
    }

    public virtual void Draw()
    {
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
        timeDisplay.Draw();
    }
}
