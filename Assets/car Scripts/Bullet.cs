using System;
using UnityEditor.Callbacks;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] public float force = 10;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward * force, ForceMode.Impulse);

        Destroy(gameObject, 10f);
    }
}
