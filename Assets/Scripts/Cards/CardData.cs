using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/BasicCard", order = 1)]
public class CardData : ScriptableObject
{
    public string cardName;
    public CardType type;
    public int power;
    public int attackSlots;
    public Sprite sprite;
}

