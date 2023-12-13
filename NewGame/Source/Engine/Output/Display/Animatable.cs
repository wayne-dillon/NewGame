using Microsoft.Xna.Framework;

public abstract class Animatable
{
    public Color color;
    private Vector2 pos;
    public Vector2 dims, baseDims;
    public Alignment alignment;
    public IAnimate animation;
    public InteractableType type;
    public Hitbox hitbox;
    public bool isTransitionable;
    public bool isUI;

    public Hitbox adjustedDims;
    public Hitbox shrink;

    public Vector2 Pos 
    { 
        get { return pos; } 
        set {
                        pos = value;
            if (type != InteractableType.NONE) UpdateHitbox();
        }
    }

    public Animatable(Color COLOR, Vector2 POS, Vector2 DIMS, Alignment ALIGNMENT, IAnimate ANIMATION, InteractableType TYPE, bool ISTRANSITIONABLE, bool ISUI)
    {
        color = ISTRANSITIONABLE ? new Color(COLOR, 0) : COLOR;
        Pos = POS;
        dims = DIMS;
        baseDims = dims;
        alignment = ALIGNMENT;
        animation = ANIMATION;
        type = TYPE;
        isTransitionable = ISTRANSITIONABLE;
        isUI = ISUI;
        hitbox = new(Pos.X - dims.X / 2, Pos.X + dims.X / 2, Pos.Y - dims.Y / 2, Pos.Y + dims.Y / 2);
        if (type == InteractableType.CHARACTER) ShrinkHitbox(30, 2, 5, 5);
        if (type == InteractableType.OBJECTIVE) ShrinkHitbox(5, 5, 5, 5);
        if (type == InteractableType.PLATFORM) Platforms.hitboxes.Add(hitbox);
        if (type == InteractableType.HAZARD)
        {
            ShrinkHitbox(40, 0, 3, 3);
            Hazards.hitboxes.Add(hitbox);
        }
    }

    public virtual void Update()
    {
        HandleTransitions();

        if (animation != null)
        {
            animation.Update();
            animation.Animate(this);
            if (animation.IsComplete())
            {
                animation = null;
            }
        }
    }

    private void HandleTransitions()
    {
        if (isTransitionable)
        {
            TransitionManager.animate?.Invoke(this);
        }
    }

    private void UpdateHitbox()
    {
        hitbox.left = Pos.X - dims.X / 2 + shrink.left;
        hitbox.right = Pos.X + dims.X / 2 - shrink.right;
        hitbox.top = Pos.Y - dims.Y / 2 + shrink.top;
        hitbox.bottom = Pos.Y + dims.Y / 2 - shrink.bottom;
    }

    private void ShrinkHitbox(int TOP, int BOTTOM, int LEFT, int RIGHT)
    {
        shrink.top = TOP;
        shrink.bottom = BOTTOM;
        shrink.left = LEFT;
        shrink.right = RIGHT;

        hitbox.top += TOP;
        hitbox.bottom -= BOTTOM;
        hitbox.left += LEFT;
        hitbox.right -= RIGHT;

        adjustedDims.top = (dims.Y / 2) - TOP;
        adjustedDims.bottom = (dims.Y / 2) - BOTTOM;
        adjustedDims.left = (dims.X / 2) - LEFT;
        adjustedDims.right = (dims.X / 2) - RIGHT;
    }
}