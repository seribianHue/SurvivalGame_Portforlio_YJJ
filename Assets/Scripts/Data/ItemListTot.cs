using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Category { RESOURCE, EQUIP, BUILDING, FOOD }
public enum Type
{
    NONE = -1,
    BURNABLE = 0, NONBURNABLE = 1,
    TOOL = 10, HELMET = 11, ARMOR = 12
}
[Serializable]
public class Item
{
    public string _name;
    public Sprite _itemSprite;
    public int _id;
    public Category _category;
    public Type _type;
    public string _description;
    public GameObject _itemPrefab;

    public Item(Sprite itemSprite, int id, string name, Category category, Type type, string description, GameObject itemPrefab)
    {
        _itemSprite = itemSprite;
        _id = id;
        _name = name;
        _category = category;
        _type = type;
        _description = description;
        _itemPrefab = itemPrefab;
    }
}
public class ItemListTot : MonoBehaviour
{
    //ItemSprite _itemSpirteList;
    public List<Item> _itemListTot = new List<Item>();

    public Item FindItemByID(int id)
    {
        foreach (Item item in _itemListTot)
        {
            if (id == -1) return null;
            else if (item._id == id) return item;
        }
        return null;
    }
}