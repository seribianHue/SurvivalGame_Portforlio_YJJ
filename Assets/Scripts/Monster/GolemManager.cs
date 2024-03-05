using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GolemManager : MonoBehaviour, Monster
{
    [SerializeField]
    float _moveSpeed = 2f;

    [SerializeField]
    int _atkDamage = 5;

    [SerializeField]
    int _hp;

    Vector3 _curDestination;
    GameObject _player;

    Animator _anim;

    [SerializeField, Header("HP UI")]
    RectTransform _monsterCanvas;
    Slider _hpSlider;

    GolemAttackCollider _attackCollider;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _player = GameObject.FindWithTag("Player");
        _hpSlider = _monsterCanvas.GetComponentInChildren<Slider>();
        _attackCollider = GetComponentInChildren<GolemAttackCollider>();
    }
    private void Start()
    {
        _hp = 200;
        _hpSlider.maxValue = _hp;
        _hpSlider.value = _hp;
        _attack = -1;
    }

    private void FixedUpdate()
    {
        _monsterCanvas.rotation = Camera.main.transform.rotation;
    }

    bool _isAttacked;
    private void Update()
    {
        if (_isAttacked)
        {
            OnAttack();
            transform.LookAt(_targetPlayer.transform);
            //OnFollowPlayer();
        }

        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    GameObject _targetPlayer;
    public void OnAttacked(int dam, GameObject attacker)
    {
        _targetPlayer = attacker;
        _hp -= dam;
        SetHPSlider(_hp);
        _isAttacked = true;
    }
    public void OnFollowPlayer()
    {
        _anim.SetBool("Walk", true);
        _curDestination = _targetPlayer.transform.position;
        transform.LookAt(_curDestination);
        transform.position += transform.forward * _moveSpeed * Time.deltaTime;
    }

    void StopFollowPlayer()
    {
        _anim.SetBool("Walk", false);

    }

    float _lastAttackTime;
    public float _atkFrequan;
    int _attack;
    public void OnAttack()
    {
        transform.LookAt(_curDestination);

        if (_attack == -1)
        {
            _attack = Random.Range(0, 3);
        }

        if(_attack == 0)
        {
            //Malee Attack;
            _atkFrequan = 2f;

            if(_attackCollider._attackRangePlayer != null)
            {
                StopFollowPlayer();
                if (Time.time - _lastAttackTime > _atkFrequan)
                {
                    MaleeAttack();
                    _lastAttackTime = Time.time;
                    _attack = -1;
                }
            }
            else
            {
                //if(_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    OnFollowPlayer();
            }
        }
        else if(_attack == 1)
        {
            //Range Attack
            StopFollowPlayer();
            _atkFrequan = 5f;
            if(Time.time - _lastAttackTime > _atkFrequan)
            {
                _anim.SetTrigger("RangeAttack");
                StartCoroutine(CRT_RangeAttack());
                _lastAttackTime = Time.time;
                _attack = -1;
            }
        }
        else
        {
            //Scream Attack
            _attack = -1;
        }
    }

    void MaleeAttack()
    {
        _anim.SetTrigger("MaleeAttack");
        StartCoroutine(CRT_MaleeAttack());
    }
    IEnumerator CRT_MaleeAttack()
    {
        yield return new WaitForSeconds(1f);
        if (_attackCollider._attackRangePlayer != null)
        {
            _attackCollider._attackRangePlayer.GetComponent<PlayerInfo>().OnAttacked(_atkDamage);
        }
        yield return null;
    }

    [SerializeField, Header("Range Attack")]
    GameObject _rock;
    [SerializeField]
    Transform _handTrnf;
    void RangeAttack()
    {
        _anim.SetTrigger("RangeAttack");
        StartCoroutine(CRT_RangeAttack());
    }
    IEnumerator CRT_RangeAttack()
    {
        GameObject rock = Instantiate(_rock, _handTrnf);
        yield return new WaitForSeconds(2f);
        if(rock != null)
        {
            rock.transform.SetParent(null);
            //rock.GetComponent<ThrowRock>().AddForce(_targetPlayer.transform.position);
            rock.GetComponent<Rigidbody>().useGravity = true;
            rock.GetComponent<Rigidbody>().
                AddForce(Vector3.MoveTowards(rock.transform.position, _player.transform.position, 100f) * 100);
        }
        yield return null;
    }

    void SetHPSlider(int hp)
    {
        _hpSlider.value = hp;
    }

    void WalkToTarget(GameObject target)
    {
        transform.position += transform.forward * _moveSpeed * Time.deltaTime;
    }

    public void OnRangeAttackEnd()
    {

    }
}
