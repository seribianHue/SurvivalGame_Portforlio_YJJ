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

    [Header("Pointer")]
    [SerializeField]
    RectTransform _pointerTrf;
    float[] _indexTranfX = { -672, -558, -444, -330, -216, -102, 12, 121, 233, 346, 455, 566, 677 };
    public void SetPointerTrf(int index)
    {
        _pointerTrf.anchoredPosition = new Vector2(_indexTranfX[index], 4.7f);
    }

    [Header("Equip")]
    [SerializeField] Image _toolSpot;
    [SerializeField] Sprite _toolSprite;
    public void SetToolSpot(Sprite image)
    {
        _toolSpot.sprite = image;
    }
    public void ReturnToolSpot()
    {
        _toolSpot.sprite = _toolSprite;
    }

    [SerializeField] Image _armorSpot;
    [SerializeField] Sprite _armorSprite;
    public void SetArmorSpot(Sprite image) { _armorSpot.sprite = image; }
    public void ReturnArmorSprite() { _armorSpot.sprite= _armorSprite; }

    [SerializeField] Image _helmetSpot;
    [SerializeField] Sprite _helmetSprite;
    public void SetHelmetSpot(Sprite image) { _helmetSpot.sprite = image; }
    public void ReturnHelmetSpot() { _helmetSpot.sprite = _helmetSprite; }

}
