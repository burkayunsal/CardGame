using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum CardValue
{
    Ace = 0,
    Two = 1,
    Three = 2,
    Four = 3,
    Five = 4,
    Six = 5,
    Seven = 6,
    Eight = 7,
    Nine = 8,
    Ten = 9,
    Jack = 10,
    Queen = 11,
    King = 12
}

public enum CardType
{
    Clubs = 0,
    Diamonds = 1,
    Hearts = 2,
    Spades = 3
}

[RequireComponent(typeof(Image))]
public class Card : CardBase
{
    [SerializeField] public Image cardImg;

    public Sprite cardBackground;
    public Sprite cardForeground;
    
    public CardType type;
    public CardValue value;

    protected override void Awake()
    {
        base.Awake();
        ShowCardFace(false);
        SetButtonInteractable(false);
    }

    public void Init(CardType cardType, CardValue cardValue)
    {
        type = cardType;
        value = cardValue;
        cardForeground = SpriteManager.I.GetSprite(type, value);
    }
    
    protected override void ButtonClick()
    {
        if (TurnManager.I.activeTurn == Turn.Player)
        {
            PlayerController.I.UseCard(this);
        }
    }

    public void ShowCardFace(bool show)
    {
        cardImg.sprite = show ? cardForeground : cardBackground;
    }
}
