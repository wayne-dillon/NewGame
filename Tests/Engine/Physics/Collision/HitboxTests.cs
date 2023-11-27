using NUnit.Framework;

public class HitboxTests
{
    [Test]
    public void TestNoCollision()
    {
        Hitbox primary = new(0, 10, 0, 10);
        Hitbox target = new(100, 110, 100, 110);

        Assert.That(primary.GetContactDirection(target), Is.EqualTo(Direction.NONE));
    }
    
    [Test]
    public void TestInside()
    {
        Hitbox primary = new(10, 20, 10, 20);
        Hitbox target = new(0, 100, 0, 100);

        Assert.That(primary.GetContactDirection(target), Is.EqualTo(Direction.NONE));
    }
    
    [Test]
    public void TestTouchLeft()
    {
        Hitbox primary = new(0, 10, 10, 30);
        Hitbox target = new(10, 100, 0, 100);

        Assert.That(primary.GetContactDirection(target), Is.EqualTo(Direction.RIGHT));
    }
    
    [Test]
    public void TestTouchRight()
    {
        Hitbox primary = new(100, 110, 10, 30);
        Hitbox target = new(10, 100, 0, 100);

        Assert.That(primary.GetContactDirection(target), Is.EqualTo(Direction.LEFT));
    }
    
    [Test]
    public void TestTouchTop()
    {
        Hitbox primary = new(10, 30, 0, 10);
        Hitbox target = new(0, 100, 10, 100);

        Assert.That(primary.GetContactDirection(target), Is.EqualTo(Direction.DOWN));
    }
    
    [Test]
    public void TestTouchBottom()
    {
        Hitbox primary = new(10, 30, 100, 110);
        Hitbox target = new(0, 100, 10, 100);

        Assert.That(primary.GetContactDirection(target), Is.EqualTo(Direction.UP));
    }
    
    [Test]
    public void TestPassesFromLeft()
    {
        Hitbox primaryOld = new(0, 10, 10, 30);
        Hitbox primaryNew = new(10, 20, 10, 30);
        Hitbox target = new(10, 100, 0, 100);

        Assert.That(target.PassesThrough(primaryOld, primaryNew), Is.EqualTo(Direction.RIGHT));
    }
    
    [Test]
    public void TestPassesFromLeftFast()
    {
        Hitbox primaryOld = new(0, 5, 10, 30);
        Hitbox primaryNew = new(15, 20, 10, 30);
        Hitbox target = new(10, 100, 0, 100);

        Assert.That(target.PassesThrough(primaryOld, primaryNew), Is.EqualTo(Direction.RIGHT));
    }
    
    [Test]
    public void TestPassesFromRight()
    {
        Hitbox primaryOld = new(100, 110, 10, 30);
        Hitbox primaryNew = new(90, 100, 10, 30);
        Hitbox target = new(10, 100, 0, 100);

        Assert.That(target.PassesThrough(primaryOld, primaryNew), Is.EqualTo(Direction.LEFT));
    }
    
    [Test]
    public void TestPassesFromTop()
    {
        Hitbox primaryOld = new(10, 30, 0, 10);
        Hitbox primaryNew = new(10, 30, 10, 20);
        Hitbox target = new(0, 100, 10, 100);

        Assert.That(target.PassesThrough(primaryOld, primaryNew), Is.EqualTo(Direction.UP));
    }
    
    [Test]
    public void TestPassesFromBottom()
    {
        Hitbox primaryOld = new(10, 30, 100, 110);
        Hitbox primaryNew = new(10, 30, 90, 100);
        Hitbox target = new(0, 100, 0, 100);

        Assert.That(target.PassesThrough(primaryOld, primaryNew), Is.EqualTo(Direction.DOWN));
    }
}