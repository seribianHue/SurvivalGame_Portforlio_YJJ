using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase
{
    //public int _id;
    public Item _item;
    public int _count;

    public Item FindItemWithId(int id) { return (Item)id; }

    public ItemBase(Item item)
    {
        //_id = id;
        //_item = (Item)id;
        _item = item;
        if(_count == 0)
        {
            _count = 1;
        }
    }

    public void AddUp()
    {
        _count++;
    }
}
