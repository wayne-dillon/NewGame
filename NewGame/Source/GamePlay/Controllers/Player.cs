using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class Player
{
    private AnimatedSprite sprite;

    private Dictionary<CharacterMode, Mode> modes = new();
    private CharacterMode currentMode;

    public float speed;
    public float fallSpeed;
    public Vector2 velocity = new();
    public bool grounded;
    public bool blockedLeft;
    public bool blockedRight;
    public bool blockedTop;

    public TextComponent modeText;

    public Player()
    {
        sprite = new SpriteBuilder().WithPathDict(SpriteDictionary.PlayerSpriteDict())
                            .WithFrameTime(100)
                            .WithRangeMinMax(0, 4)
                            .WithInteractableType(InteractableType.CHARACTER)
                            .WithDims(new Vector2(120, 120))
                            .WithOffset(new Vector2(-200, 0))
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
        SetMode();
        CheckForContact();
        modes[currentMode].MovementControl();
        UpdatePosition();
        modeText.Update(currentMode.ToString());
        sprite.Update();
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
    
    public void ResetContacts()
    {
        grounded = false;
        blockedLeft = false;
        blockedRight = false;
        blockedTop = false;
    }

    private void SetMode()
    {
        if (InputController.NextMode())
        {
            currentMode = (int)currentMode == 2 ? 0 : currentMode + 1;
            SetAnimationRange();
        }
        if (InputController.PrevMode())
        {
            currentMode = currentMode == 0 ? (CharacterMode)2 : currentMode - 1;
            SetAnimationRange();
        }
    }

    private void SetAnimationRange()
    {
        switch (currentMode)
        {
            case CharacterMode.GECKO:
                sprite.SetRange(0, 4);
                break;
            case CharacterMode.FROG:
                sprite.SetRange(5, 9);
                break;
            case CharacterMode.CAT:
                sprite.SetRange(10, 14);
                break;
        }
    }

    private void UpdatePosition()
    {
        Hitbox oldBox = sprite.hitbox.Clone();
        sprite.Pos += velocity;
        foreach (Hitbox box in Platforms.hitboxes)
        {
            switch (box.PassesThrough(oldBox, sprite.hitbox))
            {
                case Direction.NONE:
                    break;
                case Direction.LEFT:
                    sprite.Pos = new Vector2(box.right + sprite.dims.X/2, sprite.Pos.Y);
                    break;
                case Direction.RIGHT:
                    sprite.Pos = new Vector2(box.left - sprite.dims.X/2, sprite.Pos.Y);
                    break;
                case Direction.UP:
                    sprite.Pos = new Vector2(sprite.Pos.X, box.top - sprite.dims.Y/2);
                    break;
                case Direction.DOWN:
                    sprite.Pos = new Vector2(sprite.Pos.X, box.bottom + sprite.dims.Y/2);
                    break;
                case Direction.UP_LEFT:
                    sprite.Pos = new Vector2(box.right + sprite.dims.X/2, box.top - sprite.dims.Y/2);
                    break;
                case Direction.UP_RIGHT:
                    sprite.Pos = new Vector2(box.left - sprite.dims.X/2, box.top - sprite.dims.Y/2);
                    break;
                case Direction.DOWN_LEFT:
                    sprite.Pos = new Vector2(box.right + sprite.dims.X/2, box.bottom + sprite.dims.Y/2);
                    break;
                case Direction.DOWN_RIGHT:
                    sprite.Pos = new Vector2(box.left - sprite.dims.X/2, box.bottom + sprite.dims.Y/2);
                    break;
            }
        }
    }

    public void Draw()
    {
        modeText.Draw();
        sprite.Draw();
    }
}