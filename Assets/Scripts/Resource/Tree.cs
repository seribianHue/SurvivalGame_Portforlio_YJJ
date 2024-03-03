using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, Resource
{
    [Header("Drop Prefabs")]
    [SerializeField] GameObject _logItem;
    [SerializeField] GameObject _seedItem;

    int _hp = 100;

    [SerializeField]
    int _Hp = 100;
    public int _HP { get { return _hp; } set { _hp = value; } }
    
    bool isLogDroped70 = false, isLogDroped40 = false;
    public void OnAttacked(int dam)
    {
        _hp -= dam;
        _Hp = _hp;
        if(_hp <= 0) { OnDestroyed(); }
        else if((_hp < 40) && !isLogDroped40) { DropItem(1, _logItem); isLogDroped40 = true; }
        else if((_hp < 70) && !isLogDroped70) { DropItem(1, _logItem); isLogDroped70 = true; }
        
    }
    
    public void OnDestroyed()
    {
        if (CommMath.Instance.ProbabilityMethod(50)) { DropItem(3, _logItem); }
        else { DropItem(4, _logItem); }

        if(CommMath.Instance.ProbabilityMethod(50)) { DropItem(1, _seedItem); }
        else { DropItem(2, _seedItem); }

        Destroy(gameObject);
    }

    public void DropItem(int num, GameObject obj)
    {
        for (int i = 0; i < num; i++)
        {
            float ranX = Random.Range(-1f, 1f);
            float ranY = Random.Range(-1f, 1f);

            GameObject Obj = Instantiate(obj, transform.position, Quaternion.identity);
            Obj.transform.position += new Vector3(ranX, 2, ranY);
            Obj.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 90), 0));
        }
    }

    [SerializeField]
    GameObject[] _treeObjs;

    public void ChangeTreeLook(int index)
    {
        _treeObjs[index-1].SetActive(true);
    }


}
