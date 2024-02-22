using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [Header("HP")]
    public int _hp;
    int _maxhp = 100;

    public void OnAttacked(int dam)
    {
        _hp -= dam;
        UIManager.Instance._playerInfoUI.SetHpSprite((float)_hp / (float)_maxhp);
    }

    [Header("Hunger")]
    public int _hunger;
    int _maxHunger = 150;

    [Header("Mental")]
    public int _mental;
    int _maxMental = 100;


    private void Awake()
    {
        _hp = _maxhp;
        _hunger = _maxHunger;
        _mental = _maxMental;
    }

}
