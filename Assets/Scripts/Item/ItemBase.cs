using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase
{
    public ItemListTot.Item _item;
    public int _count;

    //public Item FindItemWithId(int id) { return (Item)id; }

    public ItemBase(ItemListTot.Item item)
    {
        _item = item;
        if(_count == 0)
        {
            _count = 1;
        }
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
