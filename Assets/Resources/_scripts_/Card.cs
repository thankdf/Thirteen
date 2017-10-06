using System;
using System.Collections;
using UnityEngine;


public class Card
{
    private int numbervalue, suitvalue;
    private Boolean isHidden;
    private Sprite face;

    /**
     * Sets card to specific card value
     * @param num Represents the numerical value written on card
     * @param rank Represents the suit value written on card
     * @param numvalue Represents the value of the card in the game
     * @param rankvalue Represents the value of the suit in the game
     */
    public Card(string num, string rank, int numvalue, int rankvalue)
    {
        string imageLookup = num + rank;
        face = Resources.Load("_images_/" + imageLookup, typeof(Sprite)) as Sprite;
        numbervalue = numvalue;
        suitvalue = rankvalue;
        isHidden = true;
    }

    /**
     * Returns numerical value of card in the game
     */
    public int GetValue()
    {
        return numbervalue;
    }

    /**
     * Returns face of card
     */
    public Sprite GetFace()
    {
        if(isHidden)
        {
            return Resources.Load("_images_/CardBack", typeof(Sprite)) as Sprite;
        }
        else
        {
            return face;
        }
    }

    /**
     * Changes card face during animation
     */
    public Sprite Flip()
    {
        isHidden = !isHidden;
        return GetFace();
    }

    /**
     * Returns suit value of card in the game
     */
    public int GetSuit()
    {
        return suitvalue;
    }

    /**
     * Toggles card between hidden and not hidden
     */
    private void Toggle()
    {
        isHidden = !isHidden;
    }
}
