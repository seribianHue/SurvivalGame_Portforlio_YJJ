using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    [SerializeField]
    RectTransform _timeArrow;

    public void SetTimeArrow(float angle)
    {
        _timeArrow.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
