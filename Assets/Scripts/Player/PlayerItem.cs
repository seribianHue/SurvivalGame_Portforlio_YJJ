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

    public void AddList(Item item, int count)
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

    public void RemoveItem(Item item, int count)
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
                            UIManager.Instance._itemListUI.SetEquipSpot(0, _curItem._item._itemSprite);

                            ClearToolPos();
                            Instantiate(_toolPrefabs.toolPrefabs[_curItem._item._id % 100], _toolPos);

                        }
                        else if(_curItem._item._type == Type.ARMOR)
                        {
                            if (_equipList[1] != null)
                            {
                                AddList(_equipList[1]._item, 1);
                            }
                            _equipList[1] = _curItem;
                            RemoveItem(_curItem._item, 1);
                            UIManager.Instance._itemListUI.SetEquipSpot(1, _curItem._item._itemSprite);
                            WearArmor(_curItem._item._id % 200);
                        }
                        else if (_curItem._item._type == Type.HELMET)
                        {
                            if (_equipList[2] != null)
                            {
                                AddList(_equipList[2]._item, 1);
                            }
                            _equipList[2] = _curItem;
                            RemoveItem(_curItem._item, 1);
                            UIManager.Instance._itemListUI.SetEquipSpot(2, _curItem._item._itemSprite);
                            WearArmor(_curItem._item._id % 200);

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
                    UIManager.Instance._itemListUI.ReturnEquipSpot(_curIndex % 10);
                    ClearToolPos();
                }

            }
        }
    }
    [Header("Armor")]
    [SerializeField] GameObject _underwear;
    [SerializeField] GameObject[] _armorList;
    void WearArmor(int index)
    {
        if(index > 2)
        {
            _underwear.SetActive(false);
            _armorList[index].SetActive(true);
        }
        else
        {
            _armorList[index].SetActive(true);
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

    public void DropItem()
    {
        if (_equipList[0] != null)
        {
            Instantiate(_equipList[0]._item._itemPrefab, transform.forward.normalized * 1, Quaternion.identity);
            _equipList[0] = null;
            ClearToolPos();
            UIManager.Instance._itemListUI.ReturnEquipSpot(0);

        }
        else if (_equipList[1] != null)
        {
            Instantiate(_equipList[1]._item._itemPrefab, transform.forward.normalized * 2, Quaternion.identity);
            _equipList[1] = null;
            UIManager.Instance._itemListUI.ReturnEquipSpot(1);
        }
        else if (_equipList[2] != null)
        {
            Instantiate(_equipList[2]._item._itemPrefab, transform.forward.normalized * 2, Quaternion.identity);
            _equipList[2] = null;
            UIManager.Instance._itemListUI.ReturnEquipSpot(2);
        }
        else
        {
            int randomIndex = Random.Range(0, _myItemArray.Length);
            while (_myItemArray[randomIndex] == null)
            {
                randomIndex = Random.Range(0, _myItemArray.Length);
            }
            Instantiate(_myItemArray[randomIndex]._item._itemPrefab, transform.forward.normalized * 2, Quaternion.identity);
            RemoveItem(_myItemArray[randomIndex]._item, 1);
        }
    }
}
