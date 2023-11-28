using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class Player
{
    private Sprite sprite;

    private Dictionary<CharacterMode, Mode> modes = new();
    private CharacterMode currentMode;

    public float speed;
    public float fallSpeed;
    public Vector2 velocity = new();
    public bool grounded;
    public bool blockedLeft;
    public bool blockedRight;
    public bool blockedTop;

    public Player()
    {
        sprite = new SpriteBuilder().WithPath("Cards//145x210//SPADES_ACE")
                            .WithInteractableType(InteractableType.CHARACTER)
                            .WithDims(new Vector2(120, 120))
                            .WithOffset(new Vector2(-200, 0))
                            .Build();
        currentMode = CharacterMode.GECKO;
        modes.Add(CharacterMode.GECKO, new GeckoMode(this));
    }

    public void Update()
    {
        CheckForContact();
        modes[currentMode].MovementControl();
        UpdatePosition();
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
        sprite.Draw();
    }
}