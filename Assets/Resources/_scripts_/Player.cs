using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    ArrayList cards;
    bool hasPassed; //boolean that indicates if player has passed

    public Player()
    {
        cards = new ArrayList();
        hasPassed = false;
    }

    void addCard(Card c)
    {
        cards.Add(c);
    }
}
