using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class Player
{
    public AnimatedSprite sprite;

    private Dictionary<CharacterMode, Mode> modes = new();
    private CharacterMode currentMode;
    private CharacterState currentState;

    public Hitbox prevHitbox;

    private bool animationChanged;

    public float speed;
    public float fallSpeed;
    public Vector2 velocity = new();
    public bool grounded;
    public bool blockedLeft;
    public bool blockedRight;
    public bool blockedTop;

    public TextComponent modeText;

    public Player(Vector2 POS)
    {
        sprite = new SpriteBuilder().WithPathDict(SpriteDictionary.PlayerSpriteDict())
                            .WithFrameTime(100)
                            .WithRangeMinMax(0, 4)
                            .WithInteractableType(InteractableType.CHARACTER)
                            .WithDims(new Vector2(120, 120))
                            .WithAbsolutePosition(POS)
                            .BuildAnimated();

        currentMode = CharacterMode.GECKO;
        modes.Add(CharacterMode.GECKO, new GeckoMode(this));
        modes.Add(CharacterMode.FROG, new FrogMode(this));
        modes.Add(CharacterMode.CAT, new CatMode(this));

        modeText = new TextComponentBuilder().WithScreenAlignment(Alignment.TOP)
                                            .WithOffset(new Vector2(0, 30))
                                            .Build();
    }

    public void Update()
    {
        if (GameGlobals.roundState != RoundState.END)
        {
            SetMode();
            CheckForContact();
            CharacterState newState = SetState();
            if (newState != currentState)
            {
                currentState = newState;
                animationChanged = true;
            }
            if (animationChanged)
            {
                SetAnimationRange();
            }
            modes[currentMode].MovementControl();
            UpdatePosition();
            modeText.Update(currentMode.ToString());
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

    public void CheckForContact()
    {
        ResetContacts();

        foreach (Hitbox box in Platforms.hitboxes)
        {
            switch (sprite.hitbox.GetContactDirection(box))
            {
                case Direction.NONE:
                case Direction.UP_LEFT:
                case Direction.UP_RIGHT:
                    break;
                case Direction.LEFT:
                    blockedLeft = true;
                    fallSpeed = 0;
                    break;
                case Direction.RIGHT:
                    blockedRight = true;
                    fallSpeed = 0;
                    break;
                case Direction.DOWN:
                case Direction.DOWN_LEFT:
                case Direction.DOWN_RIGHT:
                    grounded = true;
                    fallSpeed = 0;
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
    
    public void ResetContacts()
    {
        grounded = false;
        blockedLeft = false;
        blockedRight = false;
        blockedTop = false;
    }

    private void SetAnimationRange()
    {
        int num = 10 * (int)currentMode;
        num += (int)currentState;
        sprite.SetRange(num, num);
        animationChanged = false;
    }

    private void SetMode()
    {
        if (InputController.NextMode())
        {
            currentMode = (int)currentMode == 2 ? 0 : currentMode + 1;
            animationChanged = true;
        }
        if (InputController.PrevMode())
        {
            currentMode = currentMode == 0 ? (CharacterMode)2 : currentMode - 1;
            animationChanged = true;
        }
    }

    private CharacterState SetState()
    {
        if (grounded && speed == 0) return CharacterState.IDLE;
        if (currentMode != CharacterMode.CAT)
        {
            if (currentMode == CharacterMode.GECKO)
            {
                if (blockedLeft && speed < 0) return CharacterState.CLIMBING_LEFT;
                if (blockedRight && speed > 0) return CharacterState.CLIMBING_RIGHT;
            }
            if (blockedLeft) return CharacterState.CLINGING_LEFT;
            if (blockedRight) return CharacterState.CLINGING_RIGHT;
        }
        if (grounded)
        {
            if (speed < 0) return CharacterState.RUNNING_LEFT;
            if (speed > 0) return CharacterState.RUNNING_RIGHT;
        }
        if (velocity.Y < 0) return CharacterState.JUMPING;
        return CharacterState.FALLING;
    }

    private void UpdatePosition()
    {
        prevHitbox = sprite.hitbox.Clone();
        sprite.Pos += velocity;
        AdjustForPlatforms();
        CheckForHazards();
    }

    public void Draw()
    {
        modeText.Draw();
        sprite.Draw();
    }
}