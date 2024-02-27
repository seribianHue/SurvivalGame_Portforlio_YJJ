using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase
{
    public ItemListTot.Item _item;
    public int _count;

    //public Item FindItemWithId(int id) { return (Item)id; }

    public ItemBase(ItemListTot.Item item, int count)
    {
        _item = item;
        _count = count;
    }

    public void AddUp(int count)
    {
        _count += count;
    }

    public void RemoveDown(int count)
    {
        _count -= count;
    }
}
