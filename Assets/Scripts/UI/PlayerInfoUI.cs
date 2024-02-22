using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    private void Start()
    {
        _hpSprite.fillAmount = 1;
        _hungerSprite.fillAmount = 1;
        _mentalSprite.fillAmount = 1;
    }
    [SerializeField]
    Image _hpSprite;
    
    public void SetHpSprite(float ratioHP)
    {
        _hpSprite.fillAmount = ratioHP;
    }

    
    [SerializeField]
    Image _hungerSprite;

    public void SetHungerSprite(float ratioHunger)
    {
        _hungerSprite.fillAmount = ratioHunger;
    }

    [SerializeField]
    Image _mentalSprite;

    public void SetMentalSprite(float ratioMental)
    {
        _mentalSprite.fillAmount = ratioMental;
    }



}
