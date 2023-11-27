using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class Player
{
    private Sprite sprite;

    private float horizontalAcceleration = 0.05f;
    private float horizontalDeceleration = 0.1f;
    private float speed;
    private float fallSpeed = 0.1f;
    private Vector2 velocity = new();
    private readonly float maxSpeed = 3;
    private readonly List<Direction> contactDirections = new();

    public Player()
    {
        sprite = new SpriteBuilder().WithPath("Cards//145x210//SPADES_ACE").WithInteractableType(InteractableType.CHARACTER).WithDims(new Vector2(145, 210)).Build();
    }

    public void Update()
    {
        CheckForContact();
        if (contactDirections.Count != 0)
        {
            GroundedMovement();
        } else {
            AirMovement();
        }
        MoveSprite();
        UpdatePosition();
        sprite.Update();
    }

    public void CheckForContact()
    {
        contactDirections.Clear();

        foreach (Hitbox box in Platforms.hitboxes)
        {
            Direction contact = sprite.hitbox.GetContactDirection(box);
            if (contact != Direction.NONE) contactDirections.Add(contact);
        }
    }

    public void GroundedMovement()
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

    public void AirMovement()
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
        if (contactDirections.Count == 0)
        {
            velocity += new Vector2(speed * Globals.gameTime.ElapsedGameTime.Milliseconds, fallSpeed * Globals.gameTime.ElapsedGameTime.Milliseconds);
            return;
        }
        if (speed > 0)
        {
            if (!contactDirections.Contains(Direction.RIGHT))
            {
                velocity = new Vector2(speed * Globals.gameTime.ElapsedGameTime.Milliseconds, 0);
            } else {
                velocity = new Vector2(0, -speed * Globals.gameTime.ElapsedGameTime.Milliseconds);
            }
        }
        if (speed < 0)
        {
            if (!contactDirections.Contains(Direction.LEFT))
            {
                velocity = new Vector2(speed * Globals.gameTime.ElapsedGameTime.Milliseconds, 0);
            } else {
                velocity = new Vector2(0, speed * Globals.gameTime.ElapsedGameTime.Milliseconds);
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
                case Direction.UP_LEFT:
                case Direction.UP_RIGHT:
                    sprite.Pos = new Vector2(sprite.Pos.X, box.top - sprite.dims.Y/2);
                    break;
                case Direction.DOWN:
                case Direction.DOWN_LEFT:
                case Direction.DOWN_RIGHT:
                    sprite.Pos = new Vector2(sprite.Pos.X, box.bottom + sprite.dims.Y/2);
                    break;
                default:
                    break;
            }
        }
    }

    public void Draw()
    {
        sprite.Draw();
    }
}