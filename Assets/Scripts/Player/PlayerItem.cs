using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    ItemBase[] _myItemArray = new ItemBase[10];

    List<int> _curEmptyIndex = new List<int>();

    private void Start()
    {
        for(int i = 0; i < _myItemArray.Length; i++)
        {
            _curEmptyIndex.Add(i);
        }
    }

    public void AddList(Item item)
    {
        foreach(ItemBase myItem in _myItemArray)
        {
            if(myItem == null) continue;
            else
            {

            }
        }
    }
}
