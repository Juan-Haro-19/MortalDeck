using UnityEngine;
using System.Collections.Generic;

public class CardPresenter : MonoBehaviour
{
    public Sprite grabAttackSprite;
    public Sprite grabDefenseSprite;
    public Sprite punchAttackSprite;
    public Sprite punchDefenseSprite;
    public Sprite kickAttackSprite;
    public Sprite kickDefenseSprite;

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
}
