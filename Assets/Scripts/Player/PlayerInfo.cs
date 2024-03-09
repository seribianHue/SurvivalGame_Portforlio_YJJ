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

    private void Update()
    {
        GetHungry();
        GetMentalLoss();
    }

    float _hungryTime = 3;
    float _curTimeHunger = 0;
    void GetHungry()
    {
        _curTimeHunger += Time.deltaTime;
        if(_curTimeHunger > _hungryTime)
        {
            _curTimeHunger = 0;
            _hunger--;
            UIManager.Instance._playerInfoUI.SetHungerSprite((float)_hunger / (float)_maxHunger);

        }
    }

    public void AddHunger(int eat)
    {
        _hunger += eat;
        UIManager.Instance._playerInfoUI.SetHungerSprite((float)_hunger / (float)_maxHunger);
    }

    float _mentalLossTime = 2;
    float _curTimeMental = 0;
    void GetMentalLoss()
    {
        if (GameManager.Instance._IsMentalLoss)
        {
            _curTimeMental += Time.deltaTime;
            if(_curTimeMental > _mentalLossTime)
            {
                _curTimeMental = 0;
                _mental--;
                UIManager.Instance._playerInfoUI.SetMentalSprite((float)_mental / (float)_maxMental);
            }
        }
    }

}
