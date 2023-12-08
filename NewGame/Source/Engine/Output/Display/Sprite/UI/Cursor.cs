using Microsoft.Xna.Framework;

public class Cursor : Sprite
{
    public Cursor() : base("UI//Cursor", Alignment.TOP_LEFT, new Vector2(0,0), new Vector2(16,16), Color.White, null, InteractableType.NONE, false, true) 
    {
    }

    public override void Update()
    {
        Pos = Globals.mouse.newMousePos + new Vector2(8,8);

        base.Update();
    }

    public override void Draw()
    {
        base.Draw(Pos / Globals.ScalingFactor());
    }
}