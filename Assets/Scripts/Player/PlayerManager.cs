using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] AttackRange _atkRange;

    [SerializeField] public float _atkFrequan = 2f;
    [SerializeField] int damage = 10;

    float _lastAttackTime = 0;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(Time.time - _lastAttackTime > _atkFrequan)
            {
                Attack(damage);
                _lastAttackTime = Time.time;
            }
        }
    }

    void Attack(int dam)
    {
        foreach(GameObject obj in _atkRange._objList)
        {
            if(obj != null)
            {
                if(obj.GetComponent<Resource>() != null)
                    obj.GetComponent<Resource>().OnAttacked(dam);
            }

        }
    }
    
    
}
