using Microsoft.Xna.Framework;

public class ChildSprite : Sprite
{
    private readonly Sprite parent;
    private Vector2 offset;

    public ChildSprite(string PATH, Vector2 DIMS, Vector2 OFFSET, Sprite PARENT, bool ISTRANSITIONABLE) 
        : base(PATH, PARENT.alignment, PARENT.pos + OFFSET, DIMS, PARENT.color, PARENT.animation, ISTRANSITIONABLE) 
    {
        parent = PARENT;
        offset = OFFSET;
    }

    public override void Update()
    {
        pos = parent.pos + offset;

        base.Update();
    }

    public override void Draw()
    {
        base.Draw();
    }
}
