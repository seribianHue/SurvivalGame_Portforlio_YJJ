using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu
    (fileName = "ItemPrefabs_Data",
    menuName = "Scriptable Object/Item Prefab Data",
    order = int.MaxValue)]
public class ItemPrefabs : ScriptableObject
{
    public GameObject[] itemPrefabs;
}
