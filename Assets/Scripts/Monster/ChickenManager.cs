using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenManager : MonoBehaviour, Monster
{
    [SerializeField]
    float _moveSpeed = 2f;

    [SerializeField]
    int _atkDamage = 5;

    Vector3 _curDestination;

    GameObject _player;

    Animator _anim;


    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _player = GameObject.FindWithTag("Player");
        _hpSlider = _monsterCanvas.GetComponentInChildren<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetRandomDestination();
        _hp = 50;
        _hpSlider.maxValue = _hp;
        _hpSlider.value = _hp;
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

        if(_hp <= 0)
        {
            //drop item
            DropItems();
            //destory
            Destroy(gameObject);
        }
    }
    #region Death after

    [SerializeField]
    GameObject _meat;
    void DropItems()
    {
        if (CommMath.Instance.ProbabilityMethod(33))
        {
            Instantiate(_meat, 
                transform.position + new Vector3(Random.Range(0f, 1f), 1, Random.Range(0f, 1f)), 
                Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
            Instantiate(_meat,
                transform.position + new Vector3(Random.Range(0f, 1f), 1, Random.Range(0f, 1f)),
                Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
        }
        else
        {
            Instantiate(_meat,
                transform.position + new Vector3(Random.Range(0f, 1f), 1, Random.Range(0f, 1f)),
                Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
            Instantiate(_meat,
                transform.position + new Vector3(Random.Range(0f, 1f), 1, Random.Range(0f, 1f)),
                Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
            Instantiate(_meat,
                transform.position + new Vector3(Random.Range(0f, 1f), 1, Random.Range(0f, 1f)),
                Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
        }
    }

    #endregion

    #region Get Attacked
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
        _curDestination = _player.transform.position;

        transform.LookAt( _curDestination );
    }
    #endregion
    #region Attack
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
        _anim.SetBool("Walk", false);
        _anim.SetBool("Eat", true);

        yield return new WaitForSeconds(0.5f);
        if(Vector3.Distance(transform.position, _player.transform.position) < 1.5f)
        {
            _player.GetComponent<PlayerInfo>().OnAttacked(_atkDamage);
        }
        yield return new WaitForSeconds(0.5f);

        _anim.SetBool("Eat", false);
        _anim.SetBool("Walk", true);
        yield return null;
    }

    [SerializeField, Header("HP UI")]
    RectTransform _monsterCanvas;
    Slider _hpSlider;
    #endregion

    #region HP Canvas
    void SetHPSlider(int hp)
    {
        _hpSlider.value = hp;
    }

    private void FixedUpdate()
    {
        _monsterCanvas.rotation = Camera.main.transform.rotation;
    }
    #endregion
}
