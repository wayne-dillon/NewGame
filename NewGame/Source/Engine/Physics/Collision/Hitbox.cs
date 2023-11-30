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

    public Direction GetContactDirection(Hitbox TARGET)
    {
        if (left > TARGET.right || right < TARGET.left || top > TARGET.bottom || bottom < TARGET.top)
        {
            return Direction.NONE;
        }

        bool fromLeft = left == TARGET.right;
        bool fromRight = right == TARGET.left;
        bool fromTop = top == TARGET.bottom;
        bool fromBottom = bottom == TARGET.top;

        if (fromLeft) {
            if (fromTop) return Direction.UP_LEFT;
            if (fromBottom) return Direction.DOWN_LEFT;
            return Direction.LEFT;
        }
        if (fromRight) {
            if (fromTop) return Direction.UP_RIGHT;
            if (fromBottom) return Direction.DOWN_RIGHT;
            return Direction.RIGHT;
        }
        if (fromTop) return Direction.UP;
        if (fromBottom) return Direction.DOWN;
        return Direction.NONE;
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