using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class GamePlay
{
    private Player player;
    private Camera camera;
    private List<Sprite> platforms;
    private List<Sprite> hazards;
    private Sprite startLine;
    private Sprite finishLine;
    private TextComponent timeDisplay;

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
        Platforms.Reset();
        Hazards.Reset();

        player = new Player();
        camera = new Camera(player.sprite);
        platforms = new();
        hazards = new();
        runTime = new();

        timeDisplay = new TextComponentBuilder().WithText(TimerText)
                                                .WithScreenAlignment(Alignment.TOP_RIGHT)
                                                .WithTextAlignment(Alignment.CENTER_RIGHT)
                                                .WithOffset(new Vector2(-75,75))
                                                .Build();

        SpriteBuilder platformBuilder = new SpriteBuilder().WithInteractableType(InteractableType.PLATFORM)
                                                        .WithPath("rect")
                                                        .WithColor(Colors.SealBrown);
        platforms.Add(platformBuilder.WithDims(new Vector2(1600, 20))
                                    .WithScreenAlignment(Alignment.TOP)
                                    .Build());
        platforms.Add(platformBuilder.WithScreenAlignment(Alignment.BOTTOM)
                                    .Build());
        platforms.Add(platformBuilder.WithOffset(new Vector2(2000, -200))
                                    .Build());
        platforms.Add(platformBuilder.WithDims(new Vector2(20, 900))
                                    .WithScreenAlignment(Alignment.CENTER_LEFT)
                                    .Build());
        platforms.Add(platformBuilder.WithScreenAlignment(Alignment.CENTER_RIGHT)
                                    .Build());
        platforms.Add(platformBuilder.WithDims(new Vector2(500, 100))
                                    .WithOffset(new Vector2(0, -50))
                                    .WithScreenAlignment(Alignment.BOTTOM)
                                    .Build());
                                    
        SpriteBuilder hazardBuilder = new SpriteBuilder().WithInteractableType(InteractableType.HAZARD)
                                                        .WithPath("rect")
                                                        .WithColor(Color.Red);
        hazards.Add(hazardBuilder.WithDims(new Vector2(10000, 20))
                                    .WithScreenAlignment(Alignment.TOP)
                                    .WithOffset(new Vector2(0, -500))
                                    .Build());
        hazards.Add(hazardBuilder.WithScreenAlignment(Alignment.BOTTOM)
                                    .WithOffset(new Vector2(0, 500))
                                    .Build());
        hazards.Add(hazardBuilder.WithDims(new Vector2(20, 3000))
                                    .WithScreenAlignment(Alignment.CENTER_LEFT)
                                    .WithOffset(new Vector2(-500, 0))
                                    .Build());
        hazards.Add(hazardBuilder.WithScreenAlignment(Alignment.CENTER_RIGHT)
                                    .WithOffset(new Vector2(4000, 0))
                                    .Build());

        startLine = new SpriteBuilder().WithInteractableType(InteractableType.START_TIMER)
                                    .WithPath("rect")
                                    .WithColor(Colors.ShamrockGreen)
                                    .WithDims(new Vector2(20, 3000))
                                    .Build();

        finishLine = new SpriteBuilder().WithInteractableType(InteractableType.END_ROUND)
                                    .WithPath("rect")
                                    .WithColor(Colors.AmaranthPurple)
                                    .WithDims(new Vector2(20, 3000))
                                    .WithOffset(new Vector2(3000, 0))
                                    .Build();

        GameGlobals.roundState = RoundState.START;
    }

    public virtual void Update()
    {
        player.Update();
        camera.Update();
        startLine.Update();
        finishLine.Update();
        foreach (Sprite platform in platforms)
        {
            platform.Update();
        }
        foreach (Sprite hazard in hazards)
        {
            hazard.Update();
        }
        if (GameGlobals.roundState == RoundState.START)
        {
            if (startLine.hitbox.PassesThrough(player.prevHitbox, player.sprite.hitbox) != Direction.NONE)
            {
                GameGlobals.roundState = RoundState.TIMER_RUNNING;
            }
        }
        if (GameGlobals.roundState == RoundState.TIMER_RUNNING)
        {
            if (finishLine.hitbox.PassesThrough(player.prevHitbox, player.sprite.hitbox) != Direction.NONE)
            {
                GameGlobals.roundState = RoundState.END;
            } else {
                runTime += Globals.gameTime.ElapsedGameTime;
            }
        }
        if (Globals.isNewGame) 
        {
            Globals.isNewGame = false;
            ResetWorld(null, null);
        }
        timeDisplay.Update(TimerText);
    }

    public virtual void ResetWorld(object SENDER, object INFO)
    {
        Init();
    }

    public virtual void Draw()
    {
        startLine.Draw();
        finishLine.Draw();
        foreach (Sprite hazard in hazards)
        {
            hazard.Draw();
        }
        foreach (Sprite platform in platforms)
        {
            platform.Draw();
        }
        player.Draw();
        timeDisplay.Draw();
    }
}
