using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AimUI : MonoBehaviour
{
    #region Aim

    [Header("Aim"), SerializeField]
    RectTransform _aimCircle;
    Vector2 _circleSizeBig = new Vector2(40, 40);
    Vector2 _circleSizeSmall = new Vector2(0, 0);

    bool _bigger;
    bool _smaller;

    public void SetAimBig()
    {
        _smaller = false;
        StopAllCoroutines();
        StartCoroutine(CRT_SetAimSizeBig());
    }

    float _sizeChangeTime = 1f;
    IEnumerator CRT_SetAimSizeBig()
    {
        float curTime = 0f;

        while (curTime < _sizeChangeTime && _smaller == false)
        {
            curTime += Time.deltaTime;
            _aimCircle.sizeDelta = Vector2.Lerp(_aimCircle.sizeDelta, _circleSizeBig, curTime / _sizeChangeTime);
            yield return null;
        }
        yield return null;
    }

    public void SetAimSmall()
    {
        _bigger = false;
        StopAllCoroutines();
        StartCoroutine(CRT_SetAimSizeSmall());
    }

    IEnumerator CRT_SetAimSizeSmall()
    {
        float curTime = 0f;

        while (curTime < _sizeChangeTime && _bigger == false)
        {
            curTime += Time.deltaTime;
            _aimCircle.sizeDelta = Vector2.Lerp(_aimCircle.sizeDelta, _circleSizeSmall, curTime / _sizeChangeTime);
            yield return null;
        }
        yield return null;
    }
    #endregion

    #region ItemExplainText

    [Header("Item"), SerializeField]
    TextMeshProUGUI _itemNameTMP;
    public void SetItemName(string name)
    {
        _itemNameTMP.text = name;
    }

    [SerializeField]
    TextMeshProUGUI _itemExplainTMP;
    public void SetItemExplainText(string explain)
    {
        _itemExplainTMP.text = explain;
    }


    #endregion
}
