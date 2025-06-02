using UnityEngine;
using System.Collections.Generic;

public class CardPresenter : MonoBehaviour
{
    public List<CardData> cards;
    public CardView view;

    public Sprite grabAttackSprite;
    public Sprite grabDefenseSprite;
    public Sprite punchAttackSprite;
    public Sprite punchDefenseSprite;
    public Sprite kickAttackSprite;
    public Sprite kickDefenseSprite;

    private int index;

    public void SetCard(CardData _data, CardView _view)
    {
        _view.powerDisplay.text = _data.power.ToString();
        _view.cardNameDisplay.text = _data.cardName;
        _view.attackSlotssDisplay.text = _data.attackSlots.ToString();
        _view.sprite.sprite = _data.sprite;
        _view.type.sprite = GetTypeSprite(_data.type);
    }

    Sprite GetTypeSprite(CardType type)
    {
        switch (type)
        {
            case CardType.GrabAttack:
                return grabAttackSprite;
            case CardType.GrabDefense:
                return grabDefenseSprite;
            case CardType.PunchAttack:
                return punchAttackSprite;
            case CardType.PunchDefense:
                return punchDefenseSprite;
            case CardType.KickAttack:
                return kickAttackSprite;
            case CardType.KickDefense:
            default:
                return kickDefenseSprite;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //int i = Random.Range(0, cards.Count);
            if (index >= cards.Count - 1)
                index = 0;
            else
                index++;
                
            
            SetCard(cards[index], view);
        }
    }

    private void Start()
    {
        index = 0;
        SetCard(cards[index], view);

    }
}
