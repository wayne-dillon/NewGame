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
            
            { 5, "Cards//145x210//CLUBS_ACE" },
            { 6, "Cards//145x210//CLUBS_TWO" },
            { 7, "Cards//145x210//CLUBS_THREE" },
            { 8, "Cards//145x210//CLUBS_FOUR" },
            { 9, "Cards//145x210//CLUBS_FIVE" },
            
            { 10, "Cards//145x210//HEARTS_ACE" },
            { 11, "Cards//145x210//HEARTS_TWO" },
            { 12, "Cards//145x210//HEARTS_THREE" },
            { 13, "Cards//145x210//HEARTS_FOUR" },
            { 14, "Cards//145x210//HEARTS_FIVE" }
        };

        return ret;
    }
}