using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Starting Deck")]
public class StartingDeck : ScriptableObject
{
    [SerializeField]
    public SerializableDictionary<CardScriptable, int> startingCards;
}
