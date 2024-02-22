using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenManager : MonoBehaviour, Monster
{
    [SerializeField]
    float _moveSpeed = 2f;

    [SerializeField]
    int _atkDamage = 5;

    Vector3 _curDestination;

    GameObject _player;

    Animator _anim;


    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _player = GameObject.FindWithTag("Player");
        SetRandomDestination();
        _hp = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAttacked)
        {
            OnFollowPlayer();
            if(Vector3.Distance(transform.position, _curDestination) < 1.5f)
            {
                OnAttack();
            }
            else
            {
                transform.position += transform.forward * _moveSpeed * Time.deltaTime;

            }
        }
        else
        {
            transform.position += transform.forward * _moveSpeed * Time.deltaTime;
            if(Vector3.Distance(transform.position, _curDestination) < 1)
            {
                SetRandomDestination();
            }
        }

        if(_hp < 0)
        {
            //drop item

            //destory
            Destroy(gameObject);
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

    public void OnFollowPlayer()
    {
        _curDestination = _player.transform.position;

        transform.LookAt( _curDestination );
    }

    float _lastAttackTime;
    public float _atkFrequan = 5f;

    public void OnAttack()
    {
        if(Time.time - _lastAttackTime > _atkFrequan)
        {
            //Attack
            StartCoroutine(CRT_attack());

            _lastAttackTime = Time.time;
        }
    }

    IEnumerator CRT_attack()
    {
        _player.GetComponent<PlayerInfo>().OnAttacked(_atkDamage);
        _anim.SetBool("Walk", false);
        _anim.SetBool("Eat", true);
        yield return new WaitForSeconds(1);
        _anim.SetBool("Eat", false);
        _anim.SetBool("Walk", true);
        yield return null;
    }

    

}
