using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu
    (fileName = "ItemSprite_Data", 
    menuName = "Scriptable Object/Item Sprite Data", 
    order = int.MaxValue)]
public class ItemSprite : ScriptableObject
{
    public Sprite[] itemSprites;
}
