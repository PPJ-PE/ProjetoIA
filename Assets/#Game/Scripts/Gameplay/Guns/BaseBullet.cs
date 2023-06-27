using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [Header("Cache")]

    private BaseGun _parentGun;
    private Rigidbody _rb;
    private Collider _collider;
    private MeshRenderer _meshRenderer;

    protected virtual void Start() {
        _rb = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    protected virtual void OnTriggerEnter(Collider other) {
        //if(other.GetComponent<>())
    }

    public virtual void Activate(bool active) {
        _rb.isKinematic = !active;
        _collider.enabled = active;
        _meshRenderer.enabled = active;
        if (active) Shoot();
        
    }

    protected virtual void Shoot() {
        transform.position = _parentGun.ShotPoint.position;
        transform.rotation = _parentGun.ShotPoint.rotation;
        _rb.velocity = transform.forward * _parentGun.BulletSpeed;
    }
}
