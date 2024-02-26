using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    ResourceData _resouceData;
    [SerializeField]
    ToolsData _toolsData;
    [SerializeField]
    ArmorData _armorData;
    [SerializeField]
    BuildingData _buildingData;

    private void Awake()
    {
        _resouceData.SetData();
        _toolsData.SetData();
        _armorData.SetData();
        _buildingData.SetData();
    }

}
