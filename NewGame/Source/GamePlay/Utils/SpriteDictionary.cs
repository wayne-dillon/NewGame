using System.Collections.Generic;

public class SpriteDictionary
{
    public static Dictionary<int, string> PlayerSpriteDict()
    {
        Dictionary<int, string> ret = new()
        {
            { 0, "Player//catIdle1" },
            { 1, "Player//catIdle1" },
            { 2, "Player//catIdle1" },
            { 3, "Player//catIdle2" },

            { 4, "Player//catFall1" },
            { 5, "Player//catFall2" },
            
            { 11, "Player//catWalk1" },
            { 12, "Player//catWalk2" },
            { 13, "Player//catWalk3" },
            { 14, "Player//catWalk4" },
            { 15, "Player//catWalk5" },
            { 16, "Player//catWalk6" },
            { 17, "Player//catWalk7" },
            { 18, "Player//catWalk8" },
            { 19, "Player//catWalk9" },
            { 20, "Player//catWalk10" },

            { 21, "Player//catJump1" },
            { 22, "Player//catJump2" },
            { 23, "Player//catJump3" },
            { 24, "Player//catJump2" },
            { 25, "Player//catJump2" },
            { 26, "Player//catJump3" },
            { 27, "Player//catJump2" },
            { 28, "Player//catJump2" },
            { 29, "Player//catJump3" },
            { 30, "Player//catJump2" },
        };

        return ret;
    }
}