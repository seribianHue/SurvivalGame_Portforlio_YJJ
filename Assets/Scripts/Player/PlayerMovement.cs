using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float _moveSpeed = 5f;

    [SerializeField] Transform _camArmTrf;
    [SerializeField] Transform _playerTrf;

    Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {    }

    private void Update()
    {
        if (PlayerManager.Instance._IsCraft == false)
        {
            LookAround();

            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            bool isMove = moveInput.magnitude != 0;

            if (isMove && !_anim.GetBool("Attack"))
            {
                _anim.SetBool("Move", true);
                Vector3 lookForward = new Vector3(_camArmTrf.forward.x, 0f, _camArmTrf.forward.z).normalized;
                Vector3 lookRight = new Vector3(_camArmTrf.right.x, 0f, _camArmTrf.right.z).normalized;
                Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

                _playerTrf.forward = moveDir;

                transform.position += moveDir * Time.deltaTime * _moveSpeed;
            }
            else
            {
                _anim.SetBool("Move", false);
            }
        }

    }

    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = _camArmTrf.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        if(x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        _camArmTrf.rotation = Quaternion.Euler(x, (camAngle.y + mouseDelta.x), camAngle.z);
    }

    
}
