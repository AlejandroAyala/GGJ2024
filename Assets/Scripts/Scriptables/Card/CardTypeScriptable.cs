using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Type")]
public class CardTypeScriptable : ScriptableObject
{
    public string typeName;
    public Color cardColor;
    public EnemyBlocks colorBlock;
}
