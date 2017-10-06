using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Generates deck based on card values, suits, and lowest card value
 */ 
public class CardFactory: MonoBehaviour
{
    private static string[] values = {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
    private static string[] suits = {"Spades", "Clubs", "Diamonds", "Hearts"};
    private static int deckSize = values.Length * suits.Length;

    /**
     * Done so no instances of CardFactory can be instantiated
     */ 
    private CardFactory() { }

    /**
     * Creates a deck with specified lowest card value in order
     * @param lowestValue The lowest numerical value in game
     * @param lowestSuit The lowest numerical suit in game
     */
    public static Card[] CreateDeck(string lowestValue, string lowestSuit)
    {
        int valueindex = Array.FindIndex(values, x => x.Contains(lowestValue));
        int suitindex = Array.FindIndex(suits, x => x.Contains(lowestSuit));
        Card[] deck = new Card[deckSize];
        for (int i = valueindex; !(i % values.Length == valueindex && i != valueindex); i++)
        {
            for (int j = suitindex; !(j % suits.Length == suitindex && j != suitindex); j++)
            {
                //start all values at 1, so we add 1 to both number and suit values
                deck[(i - valueindex) * 4 + (j - suitindex)] = new Card(values[i % values.Length], suits[j % suits.Length], i-valueindex+1, j-suitindex+1);
            }
        }
        return Shuffle(deck);
    }

    /**
     * Shuffles the deck and returns it based on Fisher-Yates shuffle method
    */ 
    private static Card[] Shuffle(Card[] deck)
    {
        for(int i = deck.Length - 1; i >= 0; i--)
        {
            int r = UnityEngine.Random.Range(0, i+1);
            Card temp = deck[i];
            deck[i] = deck[r];
            deck[r] = temp;
        }
        return deck;
    }

    /**
     * Sorts the deck where the cards will be in order when dealt
     */
    public static Card[] sortToDealingOrder(Card[] cards, int players)
    {
        List<List<Card>> playerHands = new List<List<Card>>();
        Card[] newCards = new Card[cards.Length];
        for (int i = 0; i < players; i++)
        {
            playerHands.Add(new List<Card>());
        }
        for (int i = 0; i < cards.Length; i++)
        {
            (playerHands[i % players]).Add(cards[i]);
        }
        for (int i = 0; i < players; i++)
        {
            playerHands[i].Sort(new ThirteenCardComparator());
        }
        for (int i = 0; i < cards.Length; i++)
        {
            newCards[i] = playerHands[i % players][i / players];
        }
        return newCards;
    }
}
