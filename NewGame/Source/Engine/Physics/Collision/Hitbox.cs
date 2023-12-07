public struct Hitbox
{
    public float left;
    public float right;
    public float top;
    public float bottom;

    public Hitbox(float LEFT, float RIGHT, float TOP, float BOTTOM)
    {
        left = LEFT;
        right = RIGHT;
        top = TOP;
        bottom = BOTTOM;
    }

    public bool IsBelow(Hitbox TARGET)
    {
        if (top != TARGET.bottom) return false;

        bool onLeft = left < TARGET.left && right > TARGET.left;
        bool onRight = left < TARGET.right && right > TARGET.right;
        bool within = left > TARGET.right && right < TARGET.left;
        bool without = right > TARGET.left && left < TARGET.right;

        return onLeft || onRight || within || without;
    }

    public bool IsAbove(Hitbox TARGET)
    {
        if (bottom != TARGET.top) return false;

        bool onLeft = left < TARGET.left && right > TARGET.left;
        bool onRight = left < TARGET.right && right > TARGET.right;
        bool within = left > TARGET.right && right < TARGET.left;
        bool without = right > TARGET.left && left < TARGET.right;

        return onLeft || onRight || within || without;
    }

    public bool IsLeft(Hitbox TARGET)
    {
        if (right != TARGET.left) return false;

        bool above = top < TARGET.top && bottom > TARGET.top;
        bool below = top < TARGET.bottom && bottom > TARGET.bottom;
        bool within = top > TARGET.bottom && bottom < TARGET.top;
        bool without = bottom > TARGET.top && top < TARGET.bottom;

        return above || below || within || without;
    }

    public bool IsRight(Hitbox TARGET)
    {
        if (left != TARGET.right) return false;

        bool above = top < TARGET.top && bottom > TARGET.top;
        bool below = top < TARGET.bottom && bottom > TARGET.bottom;
        bool within = top > TARGET.bottom && bottom < TARGET.top;
        bool without = bottom > TARGET.top && top < TARGET.bottom;

        return above || below || within || without;
    }

    public Direction PassesThrough(Hitbox oldBox, Hitbox newBox)
    {
        if ((left > oldBox.right && left > newBox.right) || (right < oldBox.left && right < newBox.left)
                || (top > oldBox.bottom && top > newBox.bottom) || (bottom < oldBox.top && bottom < newBox.top))
        {
            return Direction.NONE;
        }
        bool passFromLeft = oldBox.left >= right && newBox.left < right;
        bool passFromRight = oldBox.right <= left && newBox.right > left;
        bool passFromTop = oldBox.bottom <= top && newBox.bottom > top;
        bool passFromBottom = oldBox.top >= bottom && newBox.top < bottom;
        
        bool onLeft = oldBox.left == right && newBox.left == right;
        bool onRight = oldBox.right == left && newBox.right == left;
        bool onTop = oldBox.bottom == top && newBox.bottom == top;
        bool onBottom = oldBox.top == bottom && newBox.top == bottom;
        if (passFromLeft && !(onTop || onBottom))
        {
            if (passFromTop) return Direction.UP_LEFT;
            if (passFromBottom) return Direction.DOWN_LEFT;
            return Direction.LEFT;
        } else if (passFromRight && !(onTop || onBottom))
        {
            if (passFromTop) return Direction.UP_RIGHT;
            if (passFromBottom) return Direction.DOWN_RIGHT;
            return Direction.RIGHT;
        }
        if (passFromTop && !(onLeft || onRight)) return Direction.UP;
        if (passFromBottom && !(onLeft || onRight)) return Direction.DOWN;
        return Direction.NONE;
    }

    public Hitbox Clone() => new(left, right, top, bottom);
}