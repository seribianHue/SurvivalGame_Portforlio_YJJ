using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodData : MonoBehaviour
{
    public readonly string _address =
        "https://docs.google.com/spreadsheets/d/11JTDg-xDTATe6xdfTpQhqkMPYTwG4TRdjARY9M-YUJw";
    public readonly string _range = "A2:O";
    public readonly long _sheetID = 575073612;

    [SerializeField]
    ItemGameData _itemGameData;

    public string _data;

    ItemListTot _itemTot; 
    List<Item> _itemListTot;
    List<RecipeListTot.Recipe> _recipeListTot;
    public void SetData()
    {
        _data = ReadGoogleSpreadSheet.ReadData(_address, _range, _sheetID);

        _itemTot = GetComponentInParent<ItemListTot>();
        _itemListTot = GetComponentInParent<ItemListTot>()._itemListTot;
        _recipeListTot = GetComponentInParent<RecipeListTot>()._recipeListTot;

        string[] Item = _data.Split('\n');

        for (int i = 0; i < Item.Length; i++)
        {
            string[] itemData = Item[i].Split(',');

            Category catagory = (Category)Enum.Parse(typeof(Category), itemData[4]);
            Type type = (Type)Enum.Parse(typeof(Type), itemData[5]);

            _itemListTot.
                Add(new Item(
                    _itemGameData.itemSprites[i], Int32.Parse(itemData[0]), itemData[1], catagory, type, itemData[14], _itemGameData.itemPrefabs[i]));

            _recipeListTot.Add(new RecipeListTot.Recipe(
                _itemListTot[_itemListTot.Count - 1], Int32.Parse(itemData[13]),
                _itemTot.FindItemByID(Int32.Parse(itemData[7])), Int32.Parse(itemData[8]),
                _itemTot.FindItemByID(Int32.Parse(itemData[9])), Int32.Parse(itemData[10]),
                _itemTot.FindItemByID(Int32.Parse(itemData[11])), Int32.Parse(itemData[12])));
        }

    }
}
