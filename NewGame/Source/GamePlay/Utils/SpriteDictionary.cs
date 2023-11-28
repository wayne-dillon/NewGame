using System.Collections.Generic;

public class SpriteDictionary
{
    public static Dictionary<int, string> PlayerSpriteDict()
    {
        Dictionary<int, string> ret = new()
        {
            { 0, "Cards//145x210//SPADES_ACE" },
            { 1, "Cards//145x210//SPADES_TWO" },
            { 2, "Cards//145x210//SPADES_THREE" },
            { 3, "Cards//145x210//SPADES_FOUR" },
            { 4, "Cards//145x210//SPADES_FIVE" },
            { 5, "Cards//145x210//SPADES_SIX" },
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