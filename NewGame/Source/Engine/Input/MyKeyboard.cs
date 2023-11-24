using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class MyKeyboard
{
    public KeyboardState newKeyboard, oldKeyboard;

    public List<MyKey> pressedKeys = new(), previousPressedKeys = new();

    public MyKeyboard()
    {
    }

    public virtual void Update()
    {
        newKeyboard = Keyboard.GetState();

        GetPressedKeys();
    }

    public void UpdateOld()
    {
        oldKeyboard = newKeyboard;

        previousPressedKeys = new List<MyKey>();
        foreach (MyKey key in pressedKeys)
        {
            previousPressedKeys.Add(key);
        }
    }


    public bool GetPress(string KEY)
    {

        foreach (MyKey key in pressedKeys)
        {
            if (key.key == KEY)
            {
                return true;
            }
        }

        return false;
    }


    public virtual void GetPressedKeys()
    {
        pressedKeys.Clear();
        foreach (Keys key in newKeyboard.GetPressedKeys())
        {
            pressedKeys.Add(new MyKey(key.ToString(), 1));
        }
    }

    public bool GetSinglePress(string KEY)
    {
        foreach (MyKey key in pressedKeys)
        {
            bool isIn = false;

            foreach (MyKey prevKey in previousPressedKeys)
            {
                if (key.key == prevKey.key)
                {
                    isIn = true;
                    break;
                }
            }

            if (!isIn && (key.key == KEY || key.print == KEY))
            {
                return true;
            }
        }

        return false;
    }
}
