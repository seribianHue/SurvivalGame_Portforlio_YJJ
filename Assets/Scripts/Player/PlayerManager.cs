using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    static PlayerManager instance;
    public static PlayerManager Instance { get { return instance; } }

    [Header("Player Scripts")]
    [SerializeField] public AttackRange _atkRange;
    [SerializeField] public PlayerItem _playerItem;
    [SerializeField] public PlayerInfo _playerInfo;
    [SerializeField] public PlayerMovement _playerMovement;

    [Header("Properties")]
    [SerializeField] public float _atkFrequan = 2f;
    [SerializeField] int _damage = 10;

    float _lastAttackTime = 0;

    Vector3 _screenCenter;
    [SerializeField]
    LayerMask _layer;
    RaycastHit _hit;

    GameObject _hitObj;


    Animator _anim;

    bool _isCraft;
    public bool _IsCraft { get { return _isCraft; } }
    private void Awake()
    {
        instance = this;
        _playerItem = GetComponent<PlayerItem>();
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _isCraft = false;
        _screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    bool _isGameEnd = false;
    private void Update()
    {
        #region Mouse Lock Unlock

        if(_isCraft == false)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Cursor.lockState = CursorLockMode.Confined;
                _anim.SetBool("Move", false);
                _anim.SetBool("Attack", false);
                _isCraft = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                UIManager.Instance._craftUI.SetScrollOff();
                Cursor.lockState = CursorLockMode.Locked;
                _isCraft = false;
            }
        }
        #endregion

        #region Ray Object Indicate
        Ray ray = Camera.main.ScreenPointToRay(_screenCenter);
        Debug.DrawRay(ray.origin, ray.direction * 6, Color.yellow);

        if (Physics.Raycast(ray, out _hit, 6, _layer))
        {
            if(_hitObj == null)
            {
                UIManager.Instance._aimUI.SetAimBig();
            }
            Debug.Log(_hit.transform.name);
            _hitObj = _hit.transform.gameObject;

            SetUIItem();
        }
        else
        {
            if (_hitObj != null)
            {
                UIManager.Instance._aimUI.SetAimSmall();
            }
            _hitObj = null;
            SetUIItem();
        }
        #endregion

        #region Attack
        if (!_isCraft)
        {
            if (Input.GetMouseButton(0))
            {
                if(Time.time - _lastAttackTime > _atkFrequan)
                {
                    Attack(_damage);
                    StartCoroutine(CRT_attack());
                    //_anim.SetBool("Attack", true);

                    _lastAttackTime = Time.time;
                }
            }
        }

        #endregion

        #region Pick Item
        if (!_isCraft)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(_hitObj != null)
                {
                    //pick
                    if (_hitObj.GetComponent<ItemData>() != null)
                    {
                        _playerItem.AddList(_hitObj.GetComponent<ItemData>()._item, 1);
                        Debug.Log("Picked " + _hitObj.name);
                        Destroy(_hitObj);

                        _hitObj = null;
                        UIManager.Instance._aimUI.SetAimSmall();

                    }
                }   
            }
        }   
        #endregion

        #region Check Building
        if (_hitObj != null)
        {
            if(_hitObj.GetComponent<ItemData>() != null)
            {
                if(_hitObj.GetComponent<ItemData>()._item._id == 300) 
                {
                    _isCraftTabel = true;
                    UIManager.Instance._craftUI.SetArmorScrollBTN(true);
                }
                else if(_hitObj.GetComponent<ItemData>()._item._id == 301)
                {
                    _isBonFire = true;
                    UIManager.Instance._craftUI.SetFoodScrollBTN(true);

                }
            }
            else
            {
                _isCraftTabel = false;
                _isBonFire = false;
                UIManager.Instance._craftUI.SetArmorScrollBTN(false);
                UIManager.Instance._craftUI.SetFoodScrollBTN(false);

            }
        }
        else
        {
            _isCraftTabel = false;
            _isBonFire = false;
            UIManager.Instance._craftUI.SetArmorScrollBTN(false);
            UIManager.Instance._craftUI.SetFoodScrollBTN(false);


        }
        #endregion

        if ((_playerInfo._hp <= 0) && (_isGameEnd == false))
        {
            _isGameEnd = true;
            //Time.timeScale = 0;
            _isCraft = true;
            _anim.SetTrigger("Death");
            StartCoroutine(UIManager.Instance.BlackOut());

        }

    }

    public bool _isBonFire;
    public bool _isCraftTabel;

    void Attack(int dam)
    {
        if(_hitObj == _atkRange._atkObj && _hitObj != null)
        {
            if (_hitObj.GetComponent<Resource>() != null)
            {
                _hitObj.GetComponent<Resource>().OnAttacked(dam);
            }
            else if(_hitObj.GetComponent<Monster>() != null)
            {
                _hitObj.GetComponent<Monster>().OnAttacked(dam, gameObject);
            }
        }
    }

    void SetUIItem()
    {
        if(_hitObj != null)
        {
            int startIndex = _hitObj.name.IndexOf("(");

            if (startIndex > -1)
                UIManager.Instance._aimUI.SetItemName(_hitObj.name.Remove(startIndex, 7));
            else
                UIManager.Instance._aimUI.SetItemName(_hitObj.name);


            if (_hitObj.CompareTag("Item"))
            {
                UIManager.Instance._aimUI.SetItemExplainText("Press F to Pick Up");
            }
            else
            {
                UIManager.Instance._aimUI.SetItemExplainText("Click to Attack\nDamage : " + _damage.ToString());
            }
        }
        else
        {
            UIManager.Instance._aimUI.SetItemName("");
            UIManager.Instance._aimUI.SetItemExplainText("");
        }
    }

    IEnumerator CRT_attack()
    {
        _anim.SetBool("Move", false);
        _anim.SetBool("Attack", true);
        yield return new WaitForSeconds(0.5f);
        _anim.SetBool("Attack", false);
        //_anim.SetBool("Walk", true);
        yield return null;
    }
}
