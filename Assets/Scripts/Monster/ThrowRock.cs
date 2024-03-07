using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRock : MonoBehaviour
{
    public Vector3 _targetPos;
    public void AddForce(Vector3 targetPos)
    {
        transform.GetComponent<Rigidbody>().
            AddForce(Vector3.Normalize(transform.position - _targetPos) * 1000000);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "µ¹");
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerInfo>().OnAttacked(30);
            Destroy(gameObject);
        }
    }


    float lifeTime;
    private void Update()
    {
        lifeTime += Time.deltaTime;

        if (lifeTime > 10f)
        {
            Destroy(gameObject);
        }
    }
}
