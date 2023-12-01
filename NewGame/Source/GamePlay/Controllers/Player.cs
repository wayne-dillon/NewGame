using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class Player
{
    public AnimatedSprite sprite;

    private readonly Movement movement;

    private CharacterState currentState;

    public Hitbox prevHitbox;
    private bool animationChanged;

    public Player(Vector2 POS)
    {
        movement = new();
        sprite = new SpriteBuilder().WithPathDict(SpriteDictionary.PlayerSpriteDict())
                            .WithFrameTime(200)
                            .WithRangeMinMax(0, 4)
                            .WithInteractableType(InteractableType.CHARACTER)
                            .WithDims(new Vector2(63, 112))
                            .WithAbsolutePosition(POS)
                            .BuildAnimated();
    }

    public void Update()
    {
        if (GameGlobals.roundState != RoundState.END)
        {
            movement.CheckForContact(sprite.hitbox);
            CharacterState newState = movement.GetState();

            if (newState != currentState)
            {
                currentState = newState;
                animationChanged = true;
            }
            if (animationChanged)
            {
                SetAnimationRange();
            }
            
            UpdatePosition(movement.GetVelocity());
        }
        sprite.Update();
    }

    private void AdjustForPlatforms()
    {
        foreach (Hitbox box in Platforms.hitboxes)
        {
            switch (box.PassesThrough(prevHitbox, sprite.hitbox))
            {
                case Direction.NONE:
                    break;
                case Direction.LEFT:
                    sprite.Pos = new Vector2(box.right + sprite.dims.X / 2, sprite.Pos.Y);
                    break;
                case Direction.RIGHT:
                    sprite.Pos = new Vector2(box.left - sprite.dims.X / 2, sprite.Pos.Y);
                    break;
                case Direction.UP:
                    sprite.Pos = new Vector2(sprite.Pos.X, box.top - sprite.dims.Y / 2);
                    break;
                case Direction.DOWN:
                    sprite.Pos = new Vector2(sprite.Pos.X, box.bottom + sprite.dims.Y / 2);
                    break;
                case Direction.UP_LEFT:
                    sprite.Pos = new Vector2(box.right + sprite.dims.X / 2, box.top - sprite.dims.Y / 2);
                    break;
                case Direction.UP_RIGHT:
                    sprite.Pos = new Vector2(box.left - sprite.dims.X / 2, box.top - sprite.dims.Y / 2);
                    break;
                case Direction.DOWN_LEFT:
                    sprite.Pos = new Vector2(box.right + sprite.dims.X / 2, box.bottom + sprite.dims.Y / 2);
                    break;
                case Direction.DOWN_RIGHT:
                    sprite.Pos = new Vector2(box.left - sprite.dims.X / 2, box.bottom + sprite.dims.Y / 2);
                    break;
            }
        }
    }

    public void CheckForHazards()
    {
        foreach (Hitbox box in Hazards.hitboxes)
        {
            if (box.PassesThrough(prevHitbox, sprite.hitbox) != Direction.NONE)
            {
                GameGlobals.roundState = RoundState.END;
            }
        }
    }
    
    private void SetAnimationRange()
    {
        // int num = 10 * (int)currentMode;
        // num += (int)currentState;
        sprite.SetRange(0, 1);
        animationChanged = false;
    }

    private void UpdatePosition(Vector2 VELOCITY)
    {
        prevHitbox = sprite.hitbox.Clone();
        sprite.Pos += VELOCITY;
        AdjustForPlatforms();
        CheckForHazards();
    }

    public void Draw()
    {
        sprite.Draw();
    }
}