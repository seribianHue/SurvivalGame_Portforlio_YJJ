using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float _moveSpeed = 5f;
    [SerializeField] public float _rotSpeed = 2f;

    [SerializeField] Transform _camArmTrf;
    [SerializeField] Transform _playerTrf;

    [Header("Head"), SerializeField]
    GameObject _headGObj;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        LookAround();

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        bool isMove = moveInput.magnitude != 0;

        if (isMove)
        {
            Vector3 lookForward = new Vector3(_camArmTrf.forward.x, 0f, _camArmTrf.forward.z).normalized;
            Vector3 lookRight = new Vector3(_camArmTrf.right.x, 0f, _camArmTrf.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            _playerTrf.forward = moveDir;

            transform.position += moveDir * Time.deltaTime * _moveSpeed;
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
