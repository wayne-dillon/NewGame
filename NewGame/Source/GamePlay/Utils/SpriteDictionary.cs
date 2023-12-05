using System.Collections.Generic;

public class SpriteDictionary
{
    public static Dictionary<int, string> PlayerSpriteDict()
    {
        Dictionary<int, string> ret = new()
        {
            { 0, "Player//catIdle1" },
            { 1, "Player//catIdle2" },
            { 2, "Player//catIdle3" },
            { 3, "Player//catIdle4" },
            { 4, "Player//catIdle5" },
            { 5, "Player//catIdle6" },
            { 6, "Cards//145x210//SPADES_SEVEN" },
            { 7, "Cards//145x210//SPADES_EIGHT" },
            { 8, "Cards//145x210//SPADES_NINE" },
            
            { 10, "Cards//145x210//DIAMONDS_ACE" },
            { 11, "Cards//145x210//DIAMONDS_TWO" },
            { 12, "Cards//145x210//DIAMONDS_THREE" },
            { 13, "Cards//145x210//DIAMONDS_FOUR" },
            { 14, "Cards//145x210//DIAMONDS_FIVE" },
            { 15, "Cards//145x210//DIAMONDS_SIX" },
            { 16, "Cards//145x210//DIAMONDS_SEVEN" },
            { 17, "Cards//145x210//DIAMONDS_EIGHT" },
            { 18, "Cards//145x210//DIAMONDS_NINE" },
            
            { 20, "Cards//145x210//HEARTS_ACE" },
            { 21, "Cards//145x210//HEARTS_TWO" },
            { 22, "Cards//145x210//HEARTS_THREE" },
            { 23, "Cards//145x210//HEARTS_FOUR" },
            { 24, "Cards//145x210//HEARTS_FIVE" },
            { 25, "Cards//145x210//HEARTS_SIX" },
            { 26, "Cards//145x210//HEARTS_SEVEN" },
            { 27, "Cards//145x210//HEARTS_EIGHT" },
            { 28, "Cards//145x210//HEARTS_NINE" }
        };

        return ret;
    }
}