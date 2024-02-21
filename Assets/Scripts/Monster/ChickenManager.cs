using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenManager : MonoBehaviour, Monster
{
    [SerializeField]
    float _moveSpeed = 2f;

    Vector3 _curDestination;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomDestination();
        _hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * _moveSpeed * Time.deltaTime;
        if(Vector3.Distance(transform.position, _curDestination) < 1)
        {
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        _curDestination = transform.position 
            + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));

        if(_curDestination.x > 39)
        {
            _curDestination.x -= Random.Range(5f, 10f);
        }
        else if(_curDestination.x < -39)
        {
            _curDestination.x += Random.Range(5f, 10f);
        }

        if (_curDestination.z > 39)
        {
            _curDestination.z -= Random.Range(5f, 10f);
        }
        else if (_curDestination.z < -39)
        {
            _curDestination.z += Random.Range(5f, 10f);
        }

        transform.LookAt(_curDestination);

    }

    [SerializeField]
    int _hp;

    public bool _isAttacked;

    public void OnAttacked(int dam)
    {
        _hp -= dam;
        _isAttacked = true;
    }

}
