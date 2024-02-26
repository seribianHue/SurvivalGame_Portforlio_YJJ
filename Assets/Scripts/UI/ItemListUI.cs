using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ItemListUI : MonoBehaviour
{
    [Header("Item UI")]
    [SerializeField]
    GameObject[] _itemUIArray;

    public void AddItemListSprite(int index, Sprite itemSprite, int count)
    {
        if (_itemUIArray[index] == null)
        {
        }
        _itemUIArray[index].GetComponent<Image>().sprite = itemSprite;
        _itemUIArray[index].GetComponentInChildren<TextMeshProUGUI>().text = count.ToString();
    }

    public void UpdateItemList(int index, int count)
    {
        _itemUIArray[index].GetComponentInChildren<TextMeshProUGUI>().text = count.ToString();
    }

    public void RemoveItemList(int index)
    {
        _itemUIArray[index].GetComponent<Image>().sprite = null;
        _itemUIArray[index].GetComponentInChildren<TextMeshProUGUI>().text = "";
    }
}
