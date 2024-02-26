using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    ItemBase[] _myItemArray = new ItemBase[10];

    List<int> _curEmptyIndex = new List<int>();

    private void Start()
    {
        for(int i = 0; i < _myItemArray.Length; i++)
        {
            _curEmptyIndex.Add(i);
        }
    }

    public void AddList(ItemListTot.Item item, int count)
    {
        for(int i = 0; i < _myItemArray.Length; ++i)
        {
            if(_myItemArray[i] != null)
            {
                if(_myItemArray[i]._item == item)
                {
                    if (_myItemArray[i]._count >= 99)
                    {

                    }
                    _myItemArray[i].AddUp(count);
                    UIManager.Instance._itemListUI.UpdateItemList(i, _myItemArray[i]._count);
                    return;
                }
            }
            else
            {
                _myItemArray[i] = new ItemBase(item);
                UIManager.Instance._itemListUI.AddItemListSprite(i, item._itemSprite, count);
                return;
            }
        }
    }

    public void RemoveItem(ItemListTot.Item item, int count)
    {
        if(item == null) return;

        for (int i = 0; i < _myItemArray.Length; ++i)
        {
            if (_myItemArray[i] != null)
            {
                if (_myItemArray[i]._item == item)
                {
                    if (_myItemArray[i]._count >= 99)
                    {

                    }
                    _myItemArray[i].RemoveDown(count);

                    if (_myItemArray[i]._count <= 0)
                    {
                        _myItemArray[i] = null;
                        UIManager.Instance._itemListUI.RemoveItemList(i);
                        return;
                    }
                    else
                    {
                        UIManager.Instance._itemListUI.UpdateItemList(i, _myItemArray[i]._count);
                        return;
                    }

                }
            }
        }
    }

    public bool FindItemNCount(int id, out int count)
    {
        bool isFound = false;
        count = 0;
        foreach(ItemBase item in _myItemArray)
        {
            if(item != null)
            {
                if(item._item._id == id)
                {
                    isFound = true;
                    count = item._count;
                    return isFound;
                }
            }

        }
        return isFound;
    }
}
