using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class CardScriptable : ScriptableObject
{
    public string cardName;
    public string[] cardDescriptions;
    public int energyCost;
    public CardTypeScriptable cardType;
    public Sprite cardImage;
    public CardEffect[] cardEffects;
    public string animation_trigger;
}
