using Microsoft.Xna.Framework;

public class Player
{
    public AnimatedSprite sprite;

    private readonly Movement movement;
    private static readonly int mantleDist = 5;

    private CharacterState currentState;

    public Hitbox prevHitbox;
    private bool animationChanged;

    public Player(Vector2 POS)
    {
        movement = new();
        sprite = new SpriteBuilder().WithPathDict(SpriteDictionary.PlayerSpriteDict())
                            .WithFrameTime(150)
                            .WithRangeMinMax(0, 3)
                            .WithInteractableType(InteractableType.CHARACTER)
                            .WithDims(new Vector2(63, 112))
                            .WithAbsolutePosition(POS)
                            .BuildAnimated();
        sprite.hboxOffsets = new(4,4,25,0);
    }

    public void Update()
    {
        if (GameGlobals.roundState != RoundState.END)
        {
            SetCharacterMode();
            movement.Update(sprite.hitbox);
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
                    if (box.bottom - sprite.hitbox.top <= mantleDist) {
                        sprite.Pos = new Vector2(sprite.Pos.X, box.bottom + sprite.dims.Y / 2);
                    } else if (sprite.hitbox.bottom - box.top <= mantleDist) {
                        sprite.Pos = new Vector2(sprite.Pos.X, box.top - sprite.dims.Y / 2);
                    } else {
                        sprite.Pos = new Vector2(box.right + sprite.dims.X / 2, sprite.Pos.Y);
                    }
                    break;
                case Direction.RIGHT:
                    if (box.bottom - sprite.hitbox.top <= mantleDist) {
                        sprite.Pos = new Vector2(sprite.Pos.X, box.bottom + sprite.dims.Y / 2);
                    } else if (sprite.hitbox.bottom - box.top <= mantleDist) {
                        sprite.Pos = new Vector2(sprite.Pos.X, box.top - sprite.dims.Y / 2);
                    } else {
                        sprite.Pos = new Vector2(box.left - sprite.dims.X / 2, sprite.Pos.Y);
                    }
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
        switch (currentState)
        {
            case CharacterState.JUMPING:
                sprite.SetAnimationValues(21, 30, 100);
                break;
            case CharacterState.FALLING:
                sprite.SetAnimationValues(4, 5, 75);
                break;
            case CharacterState.RUNNING:
                sprite.SetAnimationValues(11, 20, 75);
                break;
            case CharacterState.IDLE:
            default:
                sprite.SetAnimationValues(0, 3, 150);
                break;
        }
        animationChanged = false;
    }

    private void SetCharacterMode()
    {
        if (InputController.NextMode())
        {
            GameGlobals.currentMode = (int)GameGlobals.currentMode == 2 ? 0 : GameGlobals.currentMode + 1;
            animationChanged = true;
        }
        if (InputController.PrevMode())
        {
            GameGlobals.currentMode = GameGlobals.currentMode == 0 ? (CharacterMode)2 : GameGlobals.currentMode - 1;
            animationChanged = true;
        }
    }

    private void UpdatePosition(Vector2 VELOCITY)
    {
        if (VELOCITY.X > 0)
        {
            GameGlobals.facingLeft = false;
        } else if (VELOCITY.X < 0)
        {
            GameGlobals.facingLeft = true;
        }
        sprite.hFlipped = GameGlobals.facingLeft;
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