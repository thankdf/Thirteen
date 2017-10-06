using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirteenCardComparator : IComparer<Card> {

    /**
    * Compares two cards and returns the greater card
    */
    public int Compare(Card a, Card b)
    {
        if (a == null || b == null)
            if (a == null)
                if (b == null) return 0;
                else return -1;
            else
                return 1;
        else
            if (a.GetValue() > b.GetValue()) return 1;
            else if (a.GetValue() < b.GetValue()) return -1;
            else
                if (a.GetSuit() > b.GetSuit()) return 1;
            else if (a.GetSuit() < b.GetSuit()) return -1;
            else return 0; //for games where suit does not matter
    }
}
