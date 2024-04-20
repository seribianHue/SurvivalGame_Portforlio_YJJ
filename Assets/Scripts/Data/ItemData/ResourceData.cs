using UnityEngine;
using System;

public class ResourceData : MonoBehaviour
{
    public readonly string _address =
        "https://docs.google.com/spreadsheets/d/11JTDg-xDTATe6xdfTpQhqkMPYTwG4TRdjARY9M-YUJw";
    public readonly string _range = "A2:G";
    public readonly long _sheetID = 310232778;

    [SerializeField]
    ItemGameData _itemGameData;

    public string _data;

    ItemListTot _itemTot;

    public void SetData()
    {
        _data = ReadGoogleSpreadSheet.ReadData(_address, _range, _sheetID);

        _itemTot = GetComponentInParent<ItemListTot>();

        string[] resourceItem = _data.Split('\n');

        for (int i = 0; i < resourceItem.Length; i++)
        {
            string[] itemData = resourceItem[i].Split(',');

            Category catagory = (Category)Enum.Parse(typeof(Category), itemData[4]);
            Type type = (Type)Enum.Parse(typeof(Type), itemData[5]);

            _itemTot._itemListTot.
                Add(new Item(
                    _itemGameData.itemSprites[i], Int32.Parse(itemData[0]), itemData[1], catagory, type, itemData[6], _itemGameData.itemPrefabs[i]));
        }

    }



}
