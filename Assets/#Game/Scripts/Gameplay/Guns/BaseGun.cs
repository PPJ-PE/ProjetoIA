using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    [Header("Ammo")]

    [SerializeField] protected BaseBullet _bulletPrefab; // Use GO + cache BaseBullet?
    [SerializeField] protected int _ammoMax;
    private int _ammoCurrent;

    [Header("Aim")]

    [SerializeField] protected Transform _shotPoint;
    public Transform ShotPoint { get { return _shotPoint; } }
    [SerializeField] protected float _shotCooldown;
    [SerializeField] protected float _bulletSpeed;
    public float BulletSpeed { get { return _bulletSpeed; } }
    [Tooltip("The radius of a circle at 1m distance from the fire point, wich inside bullets randomly go to (higher chance to get in the center)")]
    [SerializeField] protected float _shotSpread;
    [SerializeField] protected float _reloadDuration; // Instead be "Base" duration ?

    [Header("Cache")]

    private BaseBullet[] _bulletCache;

    protected virtual void Start() {
        _bulletCache = new BaseBullet[_ammoMax];
        for (int i = 0; i < _ammoMax; i++) _bulletCache[i] = Instantiate(_bulletPrefab); // Prefab should come deactivated
    }

    protected virtual void Shoot() {
        if (_ammoCurrent > 0) {
            _ammoCurrent--;

        }
    }

}
