using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour, Resource
{
    [SerializeField] GameObject _bushItem;

    int _hp = 1;

    public void OnAttacked(int dam)
    {
        _hp -= dam;
        if(_hp <= 0) { OnDestroyed(); }
    }

    public void OnDestroyed()
    {
        DropItem(Random.Range(2, 4), _bushItem);
        Destroy(gameObject);
        UIManager.Instance._aimUI.SetAimSmall();
    }

    public void DropItem(int num, GameObject obj)
    {
        for (int i = 0; i < num; i++)
        {
            float ranX = Random.Range(-1f, 1f);
            float ranY = Random.Range(-1f, 1f);

            GameObject Obj = Instantiate(obj, transform.position, Quaternion.identity);
            Obj.transform.position += new Vector3(ranX, 1, ranY);
            Obj.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 90), 0));
        }
    }
}
