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
    public Hitbox hboxOffsets = new(0,0,0,0);
    public bool isTransitionable;

    public Vector2 Pos 
    { 
        get { return pos; } 
        set {
            pos = value;
            if (type != InteractableType.NONE) UpdateHitbox();
        }
    }

    public Animatable(Color COLOR, Vector2 POS, Vector2 DIMS, Alignment ALIGNMENT, IAnimate ANIMATION, InteractableType TYPE, bool ISTRANSITIONABLE)
    {
        color = ISTRANSITIONABLE ? new Color(COLOR, 0) : COLOR;
        Pos = POS;
        dims = DIMS;
        baseDims = dims;
        alignment = ALIGNMENT;
        animation = ANIMATION;
        type = TYPE;
        isTransitionable = ISTRANSITIONABLE;
        hitbox = new(Pos.X - dims.X / 2, Pos.X + dims.X / 2, Pos.Y - dims.Y / 2, Pos.Y + dims.Y / 2);
        if (type == InteractableType.PLATFORM) Platforms.hitboxes.Add(hitbox);
        if (type == InteractableType.HAZARD) Hazards.hitboxes.Add(hitbox);
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
        hitbox.left = Pos.X + hboxOffsets.left - dims.X / 2;
        hitbox.right = Pos.X - hboxOffsets.right + dims.X / 2;
        hitbox.top = Pos.Y + hboxOffsets.top - dims.Y / 2;
        hitbox.bottom = Pos.Y - hboxOffsets.bottom + dims.Y / 2;
    }

}