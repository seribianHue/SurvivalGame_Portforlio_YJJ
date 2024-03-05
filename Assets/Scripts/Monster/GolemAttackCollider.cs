using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttackCollider : MonoBehaviour
{
    public GameObject _attackRangePlayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _attackRangePlayer = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _attackRangePlayer)
        {
            _attackRangePlayer = null;
        }
    }

}
