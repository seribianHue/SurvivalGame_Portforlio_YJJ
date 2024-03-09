using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField]
    public AimUI _aimUI;

    [SerializeField]
    public ItemListUI _itemListUI;

    [SerializeField]
    public PlayerInfoUI _playerInfoUI;

    [SerializeField]
    public CraftUI _craftUI;

    [SerializeField]
    public TimeUI _timeUI;

    [SerializeField]
    public GameObject _deathImageObj;

    [SerializeField]
    float _fadeSpeed = 2f;
    public IEnumerator BlackOut()
    {
        //yield return null;
        _deathImageObj.SetActive(true);
        Image _blackImage = _deathImageObj.GetComponent<Image>();
        TextMeshProUGUI _dieText = _deathImageObj.GetComponentInChildren<TextMeshProUGUI>();
        while(_blackImage.color.a < 1)
        {
            _blackImage.color = new Color(0, 0, 0, _blackImage.color.a + Time.deltaTime * _fadeSpeed);
            _dieText.color = new Color(1, 0, 0, _dieText.color.a + Time.deltaTime * _fadeSpeed);
            yield return null;
        }
        Time.timeScale = 0f;
        yield break;
    }

}
