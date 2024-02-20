using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    [SerializeField]
    LayerMask _layer;

    RaycastHit _hit;

    Vector3 _screenCenter;


    void Start()
    {
        _screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    // Update is called once per frame
    void Update()
    {
/*        Ray ray = Camera.main.ScreenPointToRay(_screenCenter);
        //Debug.DrawRay()
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        if(Physics.Raycast(ray, out _hit))
        {
            //Debug.Log(_hit.transform.name);
        }*/

        Debug.DrawRay(transform.position, transform.forward * 4, Color.red);

        if(Physics.Raycast(transform.position, transform.forward, out _hit, 4, _layer))
        {
            Debug.Log(_hit.transform.name);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            Debug.Log(other.gameObject.name);

            if (Input.GetKeyDown(KeyCode.F))
            {
                //아이템 줍기

                Destroy(other.gameObject);
            }
        }
    }

}
