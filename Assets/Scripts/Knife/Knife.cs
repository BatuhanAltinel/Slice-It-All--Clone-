using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Knife : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] private Vector3 _jumpForward;
    [SerializeField] private Vector3 _spinForward;
    [SerializeField] private Vector3 _pushBackward;
    [SerializeField] private Vector3 _spinBackward;
    bool _canSpin;
    [SerializeField] float _slowDegree;
    Vector3 _knifeDirection = ((Vector3.back * 4) + (Vector3.down * 2)).normalized;


    void OnEnable()
    {
        EventManager.OnTap += OnTapHandler;

        EventManager.OnStuck += Stuck;
        EventManager.OnPushBack += PushBack;
        EventManager.OnPushBack += SpinBack;
        EventManager.OnFail += OnfailActions;
        
    }

    void OnDisable()
    {
        EventManager.OnTap -= OnTapHandler;

        EventManager.OnStuck -= Stuck;
        EventManager.OnPushBack -= PushBack;
        EventManager.OnPushBack -= SpinBack;
        EventManager.OnFail -= OnfailActions;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _rb.inertiaTensorRotation = Quaternion.identity;
    }

    void Update()
    {
        float knifeAngle = Vector3.Angle(_knifeDirection, transform.forward);

        if (_canSpin && knifeAngle < _slowDegree) _rb.maxAngularVelocity = 2f;
        
        else _rb.maxAngularVelocity = 13f;
    }

    void UnStuck()
    {
        if (_rb != null)
        {
            _rb.isKinematic = false;
        }
    }

    void Stuck()
    {
        if (_rb != null)
        {
            _rb.isKinematic = true;
        }
    }

    private void Jump()
    {
        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.AddForce(_jumpForward, ForceMode.Impulse);
        }
    }

    public void Spin()
    {
        if (_rb != null)
        {
            _canSpin = false;
            _rb.angularVelocity= Vector3.zero;
            _rb.AddTorque(_spinForward, ForceMode.Impulse);
            DOVirtual.DelayedCall(0.2f, (() => _canSpin = true));
        }
    }

    void PushBack()
    {
        if (_rb != null)
        {
            _canSpin = false;
            _rb.velocity = Vector3.zero;
            _rb.AddForce(_pushBackward, ForceMode.Impulse);
            DOVirtual.DelayedCall(0.2f, (() => _canSpin = true));
        }
    }

    void SpinBack()
    {
        if (_rb != null)
        {
            _canSpin = false;
            _rb.angularVelocity= Vector3.zero;
            _rb.AddTorque(_spinBackward, ForceMode.Impulse);
            DOVirtual.DelayedCall(0.2f, (() => _canSpin = true));
        }
    }
    
    void OnfailActions()
    {
        GetComponent<BoxCollider>().enabled = true;
        UnStuck();
    }
    
    void OnTapHandler()
    {
        GetComponent<BoxCollider>().enabled = false;
        UnStuck();
        Jump();
        Spin();
    }

}
