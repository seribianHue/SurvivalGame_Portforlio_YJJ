using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    ItemBase[] _myItemArray = new ItemBase[10];

    List<int> _curEmptyIndex = new List<int>();

    [SerializeField]
    ItemUIManager _itemUIManager;

    private void Start()
    {
        for(int i = 0; i < _myItemArray.Length; i++)
        {
            _curEmptyIndex.Add(i);
        }
    }

    public void AddList(Item item)
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
                    _myItemArray[i].AddUp();
                    _itemUIManager.UpdateItemList(i, _myItemArray[i]._count);
                    return;
                }
            }
            else
            {
                _myItemArray[i] = new ItemBase(item);
                _itemUIManager.AddItemList(i, item);
                return;
            }
        }
    }
}
