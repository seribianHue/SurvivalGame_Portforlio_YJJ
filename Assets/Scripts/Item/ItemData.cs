using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [SerializeField]
    ItemListTot _itemList;

    [SerializeField]
    public Item _selfInfo;

    private void Awake()
    {
        _itemList = GameObject.Find("Data").GetComponent<ItemListTot>();

        int startIndex = transform.name.IndexOf("(");
        string name = "";

        if (startIndex > -1)
            name = transform.name.Remove(startIndex, 7);
        else
            name = transform.name;

        foreach (var item in _itemList._itemListTot)
        {
            if(item._name == name)
            {
                _selfInfo = item;
                break;
            }
        }
    }
}
