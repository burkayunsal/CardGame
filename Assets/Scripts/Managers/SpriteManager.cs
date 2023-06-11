using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{
    [SerializeField] private Sprite[] clubs = new Sprite[Configs.Card.cardNumberOfEachType];
    [SerializeField] private Sprite[] diamonds = new Sprite[Configs.Card.cardNumberOfEachType];
    [SerializeField] private Sprite[] hearts = new Sprite[Configs.Card.cardNumberOfEachType];
    [SerializeField] private Sprite[] spades = new Sprite[Configs.Card.cardNumberOfEachType];
    

    public Sprite GetSprite(CardType type, CardValue value)
    {
        switch (type)
        {
            case CardType.Clubs :
            {
                return clubs[(int)value];
            }
            case CardType.Diamonds :
            {
                return diamonds[(int)value];
            }
            case CardType.Hearts :
            {
                return hearts[(int)value];
            }
            case CardType.Spades :
            {
                return spades[(int)value];
            }
        }

        return null;
    }
}
