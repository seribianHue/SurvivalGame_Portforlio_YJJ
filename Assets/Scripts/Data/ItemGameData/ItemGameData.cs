using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu
    (fileName = "ItemGameData",
    menuName = "Scriptable Object/Item Game Data",
    order = int.MaxValue)]
public class ItemGameData : ScriptableObject
{
    public Sprite[] itemSprites;
    public GameObject[] itemPrefabs;
}
