using Microsoft.Xna.Framework;

public class Player
{
    private Sprite sprite;

    private float horizontalAcceleration = 0.05f;
    private float horizontalDeceleration = 0.3f;
    private float speed;
    private float gravity = 0.1f;
    private float fallSpeed;
    private float maxFallSpeed = 4;
    private Vector2 velocity = new();
    private readonly float maxSpeed = 2;
    private bool grounded;
    private bool blockedLeft;
    private bool blockedRight;
    private bool blockedTop;

    public Player()
    {
        sprite = new SpriteBuilder().WithPath("Cards//145x210//SPADES_ACE")
                            .WithInteractableType(InteractableType.CHARACTER)
                            .WithDims(new Vector2(145, 210))
                            .WithOffset(new Vector2(-200, 0))
                            .Build();
    }

    public void Update()
    {
        CheckForContact();
        Movement();
        MoveSprite();
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

    public void Movement()
    {
        if (InputController.Left() && !InputController.Right())
        {
            speed -= horizontalAcceleration;
            if (speed < -maxSpeed)
                speed = -maxSpeed;
        } else if (InputController.Right() && !InputController.Left())
        {
            speed += horizontalAcceleration;
            if (speed > maxSpeed)
                speed = maxSpeed;
        } else if (speed > 0)
        {
            speed -= horizontalDeceleration;
            if (speed < 0) speed = 0;
        } else {
            speed += horizontalDeceleration;
            if (speed > 0) speed = 0;
        }
    }

    private void MoveSprite()
    {
        if (!grounded && !blockedLeft && !blockedRight)
        {
            fallSpeed = fallSpeed < maxFallSpeed ? fallSpeed + gravity : maxFallSpeed;
            velocity = new Vector2(speed, fallSpeed) * Globals.gameTime.ElapsedGameTime.Milliseconds;
            return;
        }
        if (speed > 0)
        {
            if (blockedRight)
            {
                velocity = blockedTop ? Vector2.Zero : new Vector2(0, -speed * Globals.gameTime.ElapsedGameTime.Milliseconds);
            } else {
                velocity = new Vector2(speed * Globals.gameTime.ElapsedGameTime.Milliseconds, 0);
            }
        }
        if (speed <= 0)
        {
            if (blockedLeft)
            {
                velocity = blockedTop ? Vector2.Zero : new Vector2(0, speed * Globals.gameTime.ElapsedGameTime.Milliseconds);
            } else {
                velocity = new Vector2(speed * Globals.gameTime.ElapsedGameTime.Milliseconds, 0);
            }
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
        sprite.Draw();
    }
}