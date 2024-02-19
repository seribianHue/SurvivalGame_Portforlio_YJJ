using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public List<GameObject> _objList = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        _objList.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        _objList.Remove(other.gameObject);
    }

    private void FixedUpdate()
    {
        for (int i = 0;  i < _objList.Count; i++)
        {
            if (_objList[i] == null || _objList[i].activeSelf == false)
                _objList.Remove(_objList[i]);
        }
    }
}
