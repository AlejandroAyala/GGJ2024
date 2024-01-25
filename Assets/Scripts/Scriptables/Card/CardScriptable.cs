using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class CardScriptable : ScriptableObject
{
    public CardTypeScriptable cardType;
    public string cardName;
    public Sprite cardImage;
    public CardEffect[] cardEffects;
}
