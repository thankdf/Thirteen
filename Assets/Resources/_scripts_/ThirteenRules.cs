using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirteenRules
{
    /**
     * Done so no instances of ThirteenRules can be instantiated
     */
    private ThirteenRules() { }
	
    /**
     * Checks to see if plays are valid
     * @param cards Cards selected to be played
     * @param canPass True if everyone else did not pass yet, false if player is forced to play
    */
    public static Boolean isPlayable(List<Card> cards, List<Card> cardsOnField, Boolean canPass)
    {
        if(cards.Count == cardsOnField.Count)
        {

        }
        return false;
    }
}
