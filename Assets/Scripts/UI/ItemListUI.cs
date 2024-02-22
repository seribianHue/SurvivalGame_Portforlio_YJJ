using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ItemListUI : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField]
    Sprite[] _itemResorcesSprites;
    [SerializeField]
    Sprite[] _itemToolsSprites;
    [SerializeField]
    Sprite[] _itemArmorsSprites;
    [SerializeField]
    Sprite[] _itemBuildingsSprites;
    [SerializeField]
    Sprite[] _itemFoodsSprites;
    [SerializeField]
    List<Sprite[]> _itemSprites = new List<Sprite[]>();

    private void Awake()
    {
        _itemSprites.Add(_itemResorcesSprites);
        _itemSprites.Add(_itemToolsSprites);
        _itemSprites.Add(_itemArmorsSprites);
        _itemSprites.Add(_itemBuildingsSprites);
        _itemSprites.Add(_itemFoodsSprites);
    }

    [Header("Item UI")]
    [SerializeField]
    GameObject[] _itemUIArray;

    public void AddItemList(int index, Item item)
    {
        if (_itemUIArray[index] == null)
        {
        }
        _itemUIArray[index].GetComponent<Image>().sprite = FindSprite(item);
        _itemUIArray[index].GetComponentInChildren<TextMeshProUGUI>().text = 1.ToString();
    }

    public void UpdateItemList(int index, int count)
    {
        _itemUIArray[index].GetComponentInChildren<TextMeshProUGUI>().text = count.ToString();
    }

    Sprite FindSprite(Item item)
    {
        int typeIndex = (int)item / 100;
        int itemIndex = (int)item % 100;
        return _itemSprites[typeIndex][itemIndex];
    }
}
