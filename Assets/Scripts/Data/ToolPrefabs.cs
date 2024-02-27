using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu
    (fileName = "ToolPrefabs",
    menuName = "Scriptable Object/Tool Prefabs Data",
    order = int.MaxValue)]
public class ToolPrefabs : ScriptableObject
{
    public GameObject[] toolPrefabs;
}
