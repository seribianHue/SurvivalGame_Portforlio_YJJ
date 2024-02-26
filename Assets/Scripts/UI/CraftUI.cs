using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
        _itemName = EventSystem.current.currentSelectedGameObject.name;

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
                        else Debug.Log("Not Enough Item!");
                    }
                    else Debug.Log("Item Cannot Found!");
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
                        if (needCount >= recipe._mat1Count)
                        {
                            mat2OK = true;
                        }
                    }
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
                        if (needCount >= recipe._mat1Count)
                        {
                            mat3OK = true;
                        }
                    }
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
    }
}