using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [SerializeField]
    LayerMask _layer;

    RaycastHit _hit;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 4, Color.red);

        if(Physics.Raycast(transform.position, transform.forward, out _hit, 4, _layer))
        {
            //Debug.Log(_hit.transform.name);
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
