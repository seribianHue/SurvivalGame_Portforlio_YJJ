using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, Resource
{
    [Header("Drop Prefabs")]
    [SerializeField] GameObject _stoneItem;
    [SerializeField] GameObject _goldItem;

    int _hp = 100;

    [SerializeField]
    int _Hp = 100;
    public int _HP { get { return _hp; } set { _hp = value; } }

    bool _isStoneDropped70 = false, _isStoneDropped40 = false;
    bool _is20, _is40, _is60, _is80;
    public void OnAttacked(int dam)
    {
        _hp -= dam;
        _Hp = _hp;
        if (_hp <= 0) { OnDestroyed(); }
        else if ((_hp < 20) && !_is20)
        {
            if (CommMath.Instance.ProbabilityMethod(20)) { 
                DropItem(1, _goldItem); _is20 = true; }
            else
                DropItem(1, _stoneItem); _is20 = true; 
        }
        else if ((_hp < 40) && !_is40)
        {
            if (CommMath.Instance.ProbabilityMethod(20))
            {
                DropItem(1, _goldItem); _is40 = true;
            }
            else
                DropItem(1, _stoneItem); _is40 = true;
        }
        else if ((_hp < 60) && !_is60)
        {
            if (CommMath.Instance.ProbabilityMethod(20))
            {
                DropItem(1, _goldItem); _is60 = true;
            }
            else
                DropItem(1, _stoneItem); _is60 = true;
        }
        else if ((_hp < 80) && !_is80)
        {
            if (CommMath.Instance.ProbabilityMethod(20))
            {
                DropItem(1, _goldItem); _is80 = true;
            }
            else
                DropItem(1, _stoneItem); _is80 = true;
        }

    }

    public void OnDestroyed()
    {
        if (CommMath.Instance.ProbabilityMethod(50)) { DropItem(3, _stoneItem); }
        else { DropItem(4, _stoneItem); }

        if (CommMath.Instance.ProbabilityMethod(50)) { DropItem(1, _goldItem); }
        else { DropItem(2, _goldItem); }

        Destroy(gameObject);
    }

    public void DropItem(int num, GameObject obj)
    {
        for (int i = 0; i < num; i++)
        {
            float ranX = Random.Range(-2f, 2f);
            float ranY = Random.Range(-2f, 2f);

            GameObject Obj = Instantiate(obj, transform.position, Quaternion.identity);
            Obj.transform.position += new Vector3(ranX, 1, ranY);
            Obj.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 90), 0));
        }
    }

    [SerializeField]
    GameObject[] _rockLooks;
    public void SetRockLooks()
    {
        int index = Random.Range(0, _rockLooks.Length);
        _rockLooks[index].SetActive(true);
    }
}
