using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    ItemBase[] _myItemArray = new ItemBase[10];

    List<int> _curEmptyIndex = new List<int>();

    int _curIndex = 0;
    ItemBase _curItem;

    private void Start()
    {
        for(int i = 0; i < _myItemArray.Length; i++)
        {
            _curEmptyIndex.Add(i);
        }
        _curItem = _myItemArray[_curIndex];
        UIManager.Instance._itemListUI.SetPointerTrf(_curIndex);

    }

    private void Update()
    {
        SetCurItem();
        EquipUnequipItem();
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
                _myItemArray[i] = new ItemBase(item, count);
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

    public void SetCurItem()
    {
        if(_curIndex < 10)
            _curItem = _myItemArray[_curIndex];

        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if(wheelInput > 0f)
        {
            if (++_curIndex > 12) _curIndex = 0;
            if(_curIndex < 10)
            {
                _curItem = _myItemArray[_curIndex];
                UIManager.Instance._itemListUI.SetPointerTrf(_curIndex);
            }
            else
            {
                _curItem = _equipList[_curIndex%10];
                UIManager.Instance._itemListUI.SetPointerTrf(_curIndex);
            }


        }
        else if(wheelInput < 0f)
        {
            if (--_curIndex < 0) _curIndex = 12;
            if (_curIndex < 10)
            {
                _curItem = _myItemArray[_curIndex];
                UIManager.Instance._itemListUI.SetPointerTrf(_curIndex);
            }
            else
            {
                _curItem = _equipList[_curIndex % 10];
                UIManager.Instance._itemListUI.SetPointerTrf(_curIndex);
            }

        }
    }

    public ItemBase[] _equipList = new ItemBase[3];

    [SerializeField]
    ToolPrefabs _toolPrefabs;

    [SerializeField]
    Transform _toolPos;
    public void EquipUnequipItem()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(_curIndex < 10)
            {
                if(_curItem != null)
                {
                    if(_curItem._item._category == Category.EQUIP)
                    {
                        if(_curItem._item._type == Type.TOOL)
                        {
                            if (_equipList[0] != null)
                            {
                                AddList(_equipList[0]._item, 1);
                            }
                            _equipList[0] = _curItem;
                            RemoveItem(_curItem._item, 1);
                            UIManager.Instance._itemListUI.SetToolSpot(_curItem._item._itemSprite);

                            ClearToolPos();
                            Instantiate(_toolPrefabs.toolPrefabs[_curItem._item._id % 100], _toolPos);

                        }
                        else if(_curItem._item._type == Type.ARMOR)
                        {
                            _equipList[1] = _curItem;
                            RemoveItem(_curItem._item, 1);
                            UIManager.Instance._itemListUI.SetToolSpot(_curItem._item._itemSprite);

                        }
                        else if (_curItem._item._type == Type.HELMET)
                        {
                            _equipList[2] = _curItem;
                            RemoveItem(_curItem._item, 1);
                            UIManager.Instance._itemListUI.SetToolSpot(_curItem._item._itemSprite);

                        }
                    }
                    else
                    {
                        Debug.Log("Cannot Equip this Item!");
                    }
                }

            }
            else
            {
                if (_equipList[_curIndex % 10] != null)
                {
                    AddList(_equipList[_curIndex % 10]._item, 1);
                    _equipList[_curIndex % 10] = null;
                }

            }
        }
    }

    void ClearToolPos()
    {
        try
        {
            Destroy(_toolPos.GetChild(0).gameObject);
        }
        catch
        {
            Debug.Log("NoTools");
        }
    }
}
