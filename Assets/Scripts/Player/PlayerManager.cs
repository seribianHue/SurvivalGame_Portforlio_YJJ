using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] AttackRange _atkRange;

    [SerializeField] public float _atkFrequan = 2f;
    [SerializeField] int _damage = 10;

    float _lastAttackTime = 0;

    Vector3 _screenCenter;
    [SerializeField]
    LayerMask _layer;
    RaycastHit _hit;

    GameObject _hitObj;

    //char[] _trimChar = new char[] { '(', 'c', 'l', 'o', 'n', 'e', ')', };

    void Start()
    {
        _screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(_screenCenter);
        //Debug.DrawRay()
        Debug.DrawRay(ray.origin, ray.direction * 6, Color.yellow);

        if (Physics.Raycast(ray, out _hit, 6, _layer))
        {
            if(_hitObj == null)
            {
                UIManager.Instance.SetAimBig();
            }
            Debug.Log(_hit.transform.name);
            _hitObj = _hit.transform.gameObject;

            SetUIItem();
        }
        else
        {
            if(_hitObj != null)
            {
                UIManager.Instance.SetAimSmall();
            }
            _hitObj = null;
            SetUIItem();
        }

        if (Input.GetMouseButton(0))
        {
            if(Time.time - _lastAttackTime > _atkFrequan)
            {
                Attack(_damage);
                _lastAttackTime = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(_hitObj != null)
            {
                //pick
                Debug.Log("Picked " + _hitObj.name);
                Destroy(_hitObj);


            }   
        }
    }

    void Attack(int dam)
    {
        if(_hitObj == _atkRange._atkObj)
        {
            if (_hitObj.GetComponent<Resource>() != null)
            {
                _hitObj.GetComponent<Resource>().OnAttacked(dam);
            }
        }

/*        foreach(GameObject obj in _atkRange._objList)
        {
            if(obj != null)
            {
                if(obj.GetComponent<Resource>() != null)
                    obj.GetComponent<Resource>().OnAttacked(dam);
            }

        }*/


    }

    void SetUIItem()
    {
        if(_hitObj != null)
        {
            int startIndex = _hitObj.name.IndexOf("(");

            UIManager.Instance.SetItemName(_hitObj.name.Remove(startIndex, 7));
            if (_hitObj.CompareTag("Item"))
            {
                UIManager.Instance.SetItemExplainText("Press F to Pick Up");
            }
            else
            {
                UIManager.Instance.SetItemExplainText("Click to Attack\nDamage : " + _damage.ToString());
            }
        }
        else
        {
            UIManager.Instance.SetItemName("");
            UIManager.Instance.SetItemExplainText("");
        }
    }


}
