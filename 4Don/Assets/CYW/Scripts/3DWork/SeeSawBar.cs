using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SeeSawBar : MonoBehaviour
{
    public Vector3 torque;
    private Rigidbody _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        
        _rigidbody.AddTorque(torque);
    }
}
