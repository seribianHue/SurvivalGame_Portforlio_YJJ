using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class CraftUI : MonoBehaviour
{
    [SerializeField]
    RecipeListTot _recipeList;

    [SerializeField]
    PlayerItem _playerItem;

    public string _itemName;

    private void Awake()
    {
        _recipeList = GameObject.Find("Data").GetComponent<RecipeListTot>();
        _playerItem = GameObject.Find("Player").GetComponent<PlayerItem>();
    }

    public void BTNCraft()
    {
        GameObject curBTN = EventSystem.current.currentSelectedGameObject;
        _itemName = curBTN.name;
        Debug.Log(_itemName);
        bool mat1OK = false;
        bool mat2OK = false;
        bool mat3OK = false;
        foreach (var recipe in _recipeList._recipeListTot)
        {
            if(recipe._resultItem._name == _itemName)
            {
                if(recipe._mat1 != null)
                {
                    int needCount = 0;
                    if (_playerItem.FindItemNCount(recipe._mat1._id, out needCount))
                    {
                        if (needCount >= recipe._mat1Count)
                        {
                            mat1OK = true;
                        }
                        else Debug.Log("Not Enough Item 1!");
                    }
                    else Debug.Log("Item Cannot Found 1!");
                }
                else
                {
                    mat1OK = true;
                }

                if(recipe._mat2 != null)
                {
                    int needCount = 0;
                    if (_playerItem.FindItemNCount(recipe._mat2._id, out needCount))
                    {
                        if (needCount >= recipe._mat2Count)
                        {
                            mat2OK = true;
                        }
                        else Debug.Log("Not Enough Item 2!");

                    }
                    else Debug.Log("Item Cannot Found 2!");

                }
                else
                {
                    mat2OK = true;
                }

                if (recipe._mat3 != null)
                {
                    int needCount = 0;
                    if (_playerItem.FindItemNCount(recipe._mat3._id, out needCount))
                    {
                        if (needCount >= recipe._mat3Count)
                        {
                            mat3OK = true;
                        }
                        else Debug.Log("Not Enough Item 3!");
                    }
                    else Debug.Log("Item Cannot Found 3!");

                }
                else
                {
                    mat3OK = true;
                }

                if (mat1OK && mat2OK && mat3OK)
                {
                    _playerItem.RemoveItem(recipe._mat1, recipe._mat1Count);
                    _playerItem.RemoveItem(recipe._mat2, recipe._mat2Count);
                    _playerItem.RemoveItem(recipe._mat3, recipe._mat3Count);
                    _playerItem.AddList(recipe._resultItem, recipe._resultCount);
                }
            }
        }
        curBTN.GetComponent<CraftBTNControll>().CraftConfirmBTN();
    }

    public void MakeFood()
    {

    }

    [Header("Craft Scroll Btn")]
    [SerializeField]
    GameObject _toolCraftScroll;
    public void BTNToolCraft()
    {
        SetScrollOff();
        _toolCraftScroll.SetActive(true);
        Button[] BTNs = _toolCraftScroll.GetComponent<ScrollRect>().content.GetComponentsInChildren<Button>();
        foreach(Button btns in BTNs)
        {
            btns.GetComponent<CraftBTNControll>().CraftConfirmBTN();
        }

    }

    [SerializeField]
    GameObject _ArmorCraftScroll;
    public void BTNArmorCraft()
    {
        if (PlayerManager.Instance._isCraftTabel)
        {
            SetScrollOff();
            _ArmorCraftScroll.SetActive(true);
            
            Button[] BTNs = _ArmorCraftScroll.GetComponent<ScrollRect>().content.GetComponentsInChildren<Button>();
            foreach (Button btns in BTNs)
            {
                btns.GetComponent<CraftBTNControll>().CraftConfirmBTN();
            }

        }
    }

    [SerializeField]
    GameObject _BuildingsCraftScroll;
    public void BTNBuildingsCraft()
    {
        SetScrollOff();
        _BuildingsCraftScroll.SetActive(true);
     
        Button[] BTNs = _BuildingsCraftScroll.GetComponent<ScrollRect>().content.GetComponentsInChildren<Button>();
        foreach (Button btns in BTNs)
        {
            btns.GetComponent<CraftBTNControll>().CraftConfirmBTN();
        }

    }

    [SerializeField]
    GameObject _FoodCraftScroll;
    public void BTNFoodCraft()
    {
        if (PlayerManager.Instance._isBonFire)
        {
            SetScrollOff();
            _FoodCraftScroll.SetActive(true);
        }
    }

    public void SetScrollOff()
    {
        _toolCraftScroll.SetActive(false);
        _ArmorCraftScroll.SetActive(false);
        _BuildingsCraftScroll.SetActive(false);
        _FoodCraftScroll.SetActive(false);

    }
}
