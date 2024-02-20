using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public List<GameObject> _objList = new List<GameObject>();

    public GameObject _atkObj;

    private void OnTriggerEnter(Collider other)
    {
        //_objList.Add(other.gameObject);
        _atkObj = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        //_objList.Remove(other.gameObject);
        if(other.gameObject == _atkObj)
            _atkObj = null;
    }

    private void FixedUpdate()
    {
/*        for (int i = 0;  i < _objList.Count; i++)
        {
            if (_objList[i] == null || _objList[i].activeSelf == false)
                _objList.Remove(_objList[i]);
        }*/

        if (_atkObj == null || _atkObj.activeSelf == false)
            _atkObj = null;
    }
}
