using Microsoft.Xna.Framework;

public class ChildSprite : Sprite
{
    private readonly Sprite parent;
    private Vector2 offset;

    public ChildSprite(string PATH, Vector2 DIMS, Vector2 OFFSET, Sprite PARENT, InteractableType TYPE, bool ISTRANSITIONABLE, bool ISUI) 
        : base(PATH, PARENT.alignment, PARENT.Pos + OFFSET, DIMS, PARENT.color, PARENT.animation, TYPE, ISTRANSITIONABLE, ISUI) 
    {
        parent = PARENT;
        offset = OFFSET;
    }

    public override void Update()
    {
        Pos = parent.Pos + offset;

        base.Update();
    }

    public override void Draw()
    {
        base.Draw();
    }
}
