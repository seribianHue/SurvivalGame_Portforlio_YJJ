using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public List<GameObject> _objList = new List<GameObject>();

    public GameObject _atkObj;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Attackable"))
        {
            _atkObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == _atkObj)
            _atkObj = null;
    }

    private void FixedUpdate()
    {
        if (_atkObj == null || _atkObj.activeSelf == false)
            _atkObj = null;
    }
}
