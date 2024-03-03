using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    private void Awake()
    {
        instance = this;
    }

    float _gameTime;
    public float _GameTime { get { return _gameTime; } }

    [SerializeField]
    public float _timeDay;

    [SerializeField]
    GameObject _light;
    Vector3 _initLightAngle;

    private void Start()
    {
        _initLightAngle = _light.transform.localEulerAngles;
    }

    private void Update()
    {
        _gameTime += Time.deltaTime;
        if (_gameTime > _timeDay) _gameTime = 0;

        float angleX = _initLightAngle.x - _gameTime * 5;


        _light.transform.localEulerAngles = new Vector3(
            angleX, _initLightAngle.y, _initLightAngle.z);

        Debug.Log(_light.transform.localEulerAngles);

        if(_light.transform.localEulerAngles.x < 359 && _light.transform.localEulerAngles.x > 179)
        {
            _light.GetComponent<Light>().enabled = false;
        }
        else
        {
            _light.GetComponent<Light>().enabled = true;
        }

        UIManager.Instance._timeUI.SetTimeArrow(angleX);

        SetMentalLoss();
    }


    [SerializeField]
    bool _isMentalLoss = false;
    public bool _IsMentalLoss { get { return _isMentalLoss; } }

    void SetMentalLoss()
    {
        if(_light.transform.localEulerAngles.x < 340 && _light.transform.localEulerAngles.x > 210)
        {
            _isMentalLoss = true;
        }
        else
        {
            _isMentalLoss = false;
        }
    }

}
