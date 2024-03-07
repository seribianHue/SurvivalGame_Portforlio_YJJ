using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GolemManager : MonoBehaviour, Monster
{
    #region Golem Info
    [SerializeField]
    float _moveSpeed = 3f;

    [SerializeField]
    int _atkDamage = 5;

    [SerializeField, Header("HP UI")]
    RectTransform _monsterCanvas;
    Slider _hpSlider;
    [SerializeField]
    int _hp;
    #endregion

    //GameObject _player;

    Animator _anim;

    GolemAttackCollider _attackCollider;
    bool _isAttacked;



    #region awake, start, update
    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        //_player = GameObject.FindWithTag("Player");
        _hpSlider = _monsterCanvas.GetComponentInChildren<Slider>();
        _attackCollider = GetComponentInChildren<GolemAttackCollider>();
    }
    private void Start()
    {
        _hp = 200;
        _hpSlider.maxValue = _hp;
        _hpSlider.value = _hp;
        _attack = -1;
        _isAttackDone = true;
    }

    private void FixedUpdate()
    {
        _monsterCanvas.rotation = Camera.main.transform.rotation;
    }

    private void Update()
    {
        if (_isAttacked)
        {
            OnAttack();
            //transform.LookAt(_targetPlayer.transform);
            //OnFollowPlayer();
        }

        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Player Intraction
    GameObject _targetPlayer;
    public void OnAttacked(int dam, GameObject attacker)
    {
        _targetPlayer = attacker;
        _hp -= dam;
        SetHPSlider(_hp);
        _isAttacked = true;
    }
    Vector3 _curDestination;
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
    #endregion

    #region Attack algorithem
    float _lastAttackTime;
    public float _atkFrequan;
    int _attack;

    [SerializeField]
    float _maleeAttackFreq = 2f;
    [SerializeField]
    float _rangeAttackFreq = 5f;
    [SerializeField]
    float _soundAttackFreq = 5f;
    public void OnAttack()
    {
        transform.LookAt(_targetPlayer.transform);

        if (_attack == -1)
        {
            if (_isAttackDone)
            {
                _attack = Random.Range(0, 3);
                _isAttackDone = false;
            }
        }

        if(_attack == 0)
        {
            //Malee Attack;
            if(_attackCollider._attackRangePlayer != null)
            {
                StopFollowPlayer();
                if (Time.time - _lastAttackTime > _maleeAttackFreq)
                {
                    _anim.SetTrigger("MaleeAttack");
                    StartCoroutine(CRT_MaleeAttack());
                    _lastAttackTime = Time.time;
                    _attack = -1;
                }
            }
            else
            {
                OnFollowPlayer();
            }
        }
        else if(_attack == 1)
        {
            //Range Attack
            StopFollowPlayer();
            if(Time.time - _lastAttackTime > _rangeAttackFreq)
            {
                _anim.SetTrigger("RangeAttack");
                StartCoroutine(CRT_RangeAttack());
                _lastAttackTime = Time.time;
                _attack = -1;
            }
        }
        else if(_attack == 2)
        {
            //Scream Attack
            if (Vector3.Distance(transform.position, _targetPlayer.transform.position) < 5f)
            {
                StopFollowPlayer();
                if (Time.time - _lastAttackTime > _soundAttackFreq)
                {
                    _anim.SetTrigger("SoundAttack");
                    StartCoroutine(CRT_SoundAttack());
                    _lastAttackTime = Time.time;
                    _attack = -1;
                }
            }
            else
            {
                OnFollowPlayer();
            }
            //_isAttackDone = true;
        }
    }
    #endregion

    #region Malee Attack
    IEnumerator CRT_MaleeAttack()
    {
        yield return new WaitForSeconds(1f);
        if (_attackCollider._attackRangePlayer != null)
        {
            _attackCollider._attackRangePlayer.GetComponent<PlayerInfo>().OnAttacked(_atkDamage);
        }
        yield return null;
    }
    #endregion

    #region Range Attack
    [SerializeField, Header("Range Attack")]
    GameObject _rock;
    [SerializeField]
    Transform _handTrnf;
    IEnumerator CRT_RangeAttack()
    {
        GameObject rock = Instantiate(_rock, _handTrnf);
        yield return new WaitForSeconds(2f);
        if(rock != null)
        {
            rock.transform.SetParent(null);
            //rock.GetComponent<ThrowRock>().AddForce(_targetPlayer.transform.position);
            rock.GetComponent<Rigidbody>().useGravity = true;
            rock.GetComponent<Rigidbody>().AddForce(Vector3.MoveTowards(
                rock.transform.position, _targetPlayer.transform.position, 100f) * 100);
        }
        yield return null;
    }
    #endregion
    #region Sound Attack
    IEnumerator CRT_SoundAttack()
    {
        yield return new WaitForSeconds(1.5f);
        if (Vector3.Distance(transform.position, _targetPlayer.transform.position) < 5f)
        {
            _targetPlayer.GetComponent<PlayerItem>().DropItem();    
        }
        yield return null;
    }
    #endregion

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

    #region Animation
    bool _isAttackDone;
    public void EndMaleeAttack()
    {
        Debug.Log("maleeattack done");
        _isAttackDone = true;
    }

    public void EndRangeAttack()
    {
        Debug.Log("RangeAttack done");
        _isAttackDone = true;
    }

    public void EndSoundAttack()
    {
        _isAttackDone = true;
    }
    #endregion
}
