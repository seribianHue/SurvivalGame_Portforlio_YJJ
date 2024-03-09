using System;
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
        BuildingConfirm();
        FoodConfirm();

        DropItem();
    }

    public void AddList(Item item, int count)
    {
        for(int j = 0; j < _myItemArray.Length; j++)
        {
            if (_myItemArray[j] != null)
            {
                if (_myItemArray[j]._item == item)
                {
                    if (_myItemArray[j]._count >= 99)
                    {

                    }
                    _myItemArray[j].AddUp(count);
                    UIManager.Instance._itemListUI.UpdateItemList(j, _myItemArray[j]._count);
                    return;
                }
            }

        }
        for (int i = 0; i < _myItemArray.Length; ++i)
        {
/*            if(_myItemArray[i] != null)
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
            }*/

            if (_myItemArray[i] == null)
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

    #region Building Confirm
    [SerializeField]
    Transform _buildingTrnf;
    GameObject _buildingINhand;
    void BuildingConfirm()
    {
        if(_curItem != null)
        {
            if(_curItem._item._category == Category.BUILDING)
            {
                
                if ((_buildingINhand == null) || (_buildingINhand.GetComponent<ItemData>()._item._id != _curItem._item._id))
                {
                    if (_buildingINhand != null)
                    {
                        Destroy(_buildingINhand);
                    }
                    _buildingINhand = Instantiate(_curItem._item._itemPrefab, _buildingTrnf);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _buildingINhand.transform.parent = null;
                    _buildingINhand = null;
                    RemoveItem(_curItem._item, 1);
                }
            }
            else
            {
                if (_buildingINhand != null)
                {
                    Destroy(_buildingINhand);
                    _buildingINhand = null;
                }
            }
        }
        else 
        {
            if(_buildingINhand != null)
            {
                Destroy(_buildingINhand);
                _buildingINhand = null;
            }
        }

    }
    #endregion

    #region Food Confirm
    GameObject _isFoodINhand;
    void FoodConfirm()
    {
        if (_curItem != null)
        {
            if (_curItem._item._category == Category.FOOD)
            {

                if ((_isFoodINhand == null) || (_isFoodINhand.GetComponent<ItemData>()._item._id != _curItem._item._id))
                {
                    if(_isFoodINhand != null)
                    {
                        Destroy(_isFoodINhand);
                    }
                    _isFoodINhand = Instantiate(_curItem._item._itemPrefab, _toolPos);
                    if(_isFoodINhand.GetComponent<Rigidbody>() != null)
                    {
                        _isFoodINhand.GetComponent<Rigidbody>().isKinematic = true;
                        _isFoodINhand.GetComponent<Collider>().isTrigger = true;
                    }
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //eat
                    PlayerManager.Instance._playerInfo.AddHunger(Int32.Parse(_curItem._item._description));
                    RemoveItem(_curItem._item, 1);
                    Destroy(_isFoodINhand);
                    _isFoodINhand = null;

                }
            }
            else
            {
                if ((_isFoodINhand != null))
                {
                    Destroy(_isFoodINhand);
                    _isFoodINhand = null;
                }
            }
        }
        else
        {
            if (_isFoodINhand != null)
            {
                Destroy(_isFoodINhand);
                _isFoodINhand = null;
            }
        }
    }
    #endregion

    public ItemBase[] _equipList = new ItemBase[3];

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
                            Instantiate(_curItem._item._itemPrefab, _toolPos);

                        }
                        else if(_curItem._item._type == Type.ARMOR)
                        {
                            if (_equipList[1] != null)
                            {
                                AddList(_equipList[1]._item, 1);
                            }
                            _equipList[1] = _curItem;
                            RemoveItem(_curItem._item, 1);
                            UIManager.Instance._itemListUI.SetArmorSpot(_curItem._item._itemSprite);
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
                            UIManager.Instance._itemListUI.SetHelmetSpot(_curItem._item._itemSprite);
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
        if (index > 2)
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
        if(_curItem != null)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                Instantiate(_curItem._item._itemPrefab,
                    transform.position + transform.forward.normalized * 1 + new Vector3(0, 1, 0), Quaternion.identity);
                RemoveItem(_curItem._item, 1);
            }
        }
    }

    public void DropItemByMonster()
    {
        Debug.Log(transform.position);
        if (_equipList[0] != null)
        {
            Instantiate(_equipList[0]._item._itemPrefab,
                transform.position + transform.forward.normalized * 1, Quaternion.identity);
            _equipList[0] = null;
            ClearToolPos();
            UIManager.Instance._itemListUI.ReturnEquipSpot(0);

        }
        else if (_equipList[1] != null)
        {
            Instantiate(_equipList[1]._item._itemPrefab, 
                transform.position + transform.forward.normalized * 1, Quaternion.identity);
            _equipList[1] = null;
            UIManager.Instance._itemListUI.ReturnEquipSpot(1);
        }
        else if (_equipList[2] != null)
        {
            Instantiate(_equipList[2]._item._itemPrefab, 
                transform.position + transform.forward.normalized * 1, Quaternion.identity);
            _equipList[2] = null;
            UIManager.Instance._itemListUI.ReturnEquipSpot(2);
        }
        else
        {
            bool _isEmpty = true;
            foreach(ItemBase item in _myItemArray)
            {
                if(item != null) _isEmpty = false;
            }

            if (!_isEmpty)
            {
                int randomIndex = UnityEngine.Random.Range(0, _myItemArray.Length);
                while (_myItemArray[randomIndex] == null)
                {
                    randomIndex = UnityEngine.Random.Range(0, _myItemArray.Length);
                }
                Instantiate(_myItemArray[randomIndex]._item._itemPrefab, 
                    transform.position + transform.forward.normalized * 1 + new Vector3(0, 1, 0), Quaternion.identity);
                RemoveItem(_myItemArray[randomIndex]._item, 1);
            }
        }
    }
}
