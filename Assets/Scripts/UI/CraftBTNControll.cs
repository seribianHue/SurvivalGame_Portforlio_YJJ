using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftBTNControll : MonoBehaviour
{
    RecipeListTot _recipeList;
    PlayerItem _playerItem;

    string _itemName;

    private void Awake()
    {
        _recipeList = GameObject.Find("Data").GetComponent<RecipeListTot>();
        _playerItem = GameObject.Find("Player").GetComponent<PlayerItem>();
        _itemName = gameObject.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CraftConfirmBTN()
    {
        gameObject.GetComponent<Button>().interactable = false;

        bool mat1OK = false;
        bool mat2OK = false;
        bool mat3OK = false;
        foreach (var recipe in _recipeList._recipeListTot)
        {
            if (recipe._resultItem._name == _itemName)
            {
                if (recipe._mat1 != null)
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

                if (recipe._mat2 != null)
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
                    gameObject.GetComponent<Button>().interactable = true;
                }
            }
        }
    }

}
