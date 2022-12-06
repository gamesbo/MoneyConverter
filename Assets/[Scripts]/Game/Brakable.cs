using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brakable : MonoBehaviour
{
    void Start()
    {
        foreach (Rigidbody rb in transform.GetComponentsInChildren<Rigidbody>())
        {
            rb.transform.parent = null;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(1f, 2.5f), Random.Range(-0.9f, .9f)) * 350f);
        }
    }

    void Update()
    {
        
    }
}
