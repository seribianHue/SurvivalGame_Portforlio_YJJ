using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData : MonoBehaviour
{
    public readonly string _address =
        "https://docs.google.com/spreadsheets/d/11JTDg-xDTATe6xdfTpQhqkMPYTwG4TRdjARY9M-YUJw";
    public readonly string _range = "A2:P";
    public readonly long _sheetID = 1759990118;

    [SerializeField]
    ItemGameData _itemGameData;

    public string _data;

    ItemListTot _itemTot;
    List<Item> _itemListTot;
    List<Recipe> _recipeListTot;

    public void SetData()
    {
        _data = ReadGoogleSpreadSheet.ReadData(_address, _range, _sheetID);

        _itemTot = GetComponentInParent<ItemListTot>();

        _itemListTot = GetComponentInParent<ItemListTot>()._itemListTot;
        _recipeListTot = GetComponentInParent<RecipeListTot>()._recipeListTot;

        string[] Item = _data.Split('\n');

        for (int i = 0; i < Item.Length; i++)
        {
            string[] Data = Item[i].Split(',');

            Category catagory = (Category)Enum.Parse(typeof(Category), Data[4]);
            Type type = (Type)Enum.Parse(typeof(Type), Data[5]);

            _itemListTot.
                Add(new Item(
                    _itemGameData.itemSprites[i], Int32.Parse(Data[0]), Data[1], catagory, type, Data[15], _itemGameData.itemPrefabs[i]));

            _recipeListTot.
                Add(new Recipe(
                _itemListTot[_itemListTot.Count - 1], Int32.Parse(Data[14]),
                _itemTot.FindItemByID(Int32.Parse(Data[8])), Int32.Parse(Data[9]),
                _itemTot.FindItemByID(Int32.Parse(Data[10])), Int32.Parse(Data[11]),
                _itemTot.FindItemByID(Int32.Parse(Data[12])), Int32.Parse(Data[13])));

        }

    }
}
