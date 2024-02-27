using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField]
    public AimUI _aimUI;

    [SerializeField]
    public ItemListUI _itemListUI;

    [SerializeField]
    public PlayerInfoUI _playerInfoUI;

    [SerializeField]
    public CraftUI _craftUI;

    [SerializeField]
    public TimeUI _timeUI;

}
