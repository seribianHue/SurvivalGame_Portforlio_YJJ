using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsData : MonoBehaviour
{
    public readonly string _address =
        "https://docs.google.com/spreadsheets/d/11JTDg-xDTATe6xdfTpQhqkMPYTwG4TRdjARY9M-YUJw";
    public readonly string _range = "A2:S";
    public readonly long _sheetID = 1588693527;

    [SerializeField]
    ItemSprite _itemSpriteArray;

    public string _data;

    ItemListTot _itemTot;
    List<ItemListTot.Item> _itemListTot;
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
            string[] Data = Item[i].Split(',');

            Category catagory = (Category)Enum.Parse(typeof(Category), Data[4]);
            Type type = (Type)Enum.Parse(typeof(Type), Data[5]);

            _itemListTot.
                Add(new ItemListTot.Item(
                    _itemSpriteArray.itemSprites[i], Int32.Parse(Data[0]), Data[1], catagory, type, Data[18]));

            _recipeListTot.
                Add(new RecipeListTot.Recipe(
                _itemListTot[_itemListTot.Count - 1], Int32.Parse(Data[17]),
                _itemTot.FindItemByID(Int32.Parse(Data[11])), Int32.Parse(Data[12]),
                _itemTot.FindItemByID(Int32.Parse(Data[13])), Int32.Parse(Data[14]),
                _itemTot.FindItemByID(Int32.Parse(Data[15])), Int32.Parse(Data[16])));

        }

    }



}
