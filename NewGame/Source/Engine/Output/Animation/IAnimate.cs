public interface IAnimate
{
    void Update();
    void Animate(Animatable TARGET);
    bool IsComplete();
}