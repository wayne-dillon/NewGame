using Microsoft.Xna.Framework;

public abstract class Animatable
{
    public Color color;
    public Vector2 pos, dims, baseDims;
    public Alignment alignment;
    public IAnimate animation;
    public bool isTransitionable;

    public Animatable(Color COLOR, Vector2 POS, Vector2 DIMS, Alignment ALIGNMENT, IAnimate ANIMATION, bool ISTRANSITIONABLE)
    {
        color = ISTRANSITIONABLE ? new Color(COLOR, 0) : COLOR;
        pos = POS;
        dims = DIMS;
        baseDims = dims;
        alignment = ALIGNMENT;
        animation = ANIMATION;
        isTransitionable = ISTRANSITIONABLE;
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
}