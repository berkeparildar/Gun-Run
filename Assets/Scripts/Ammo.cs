using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ammo : MovingObject
{
    private int _minBulletCount;
    private int _maxBulletCount;
    private int _bulletCount;
    
    private void Start()
    {
        SetBulletCount();
        hitPoints = _bulletCount;
    }

    private void Update()
    {
        Move();
        TakeHit();
    }
    
    private void SetBulletCount()
    {
        int count = Random.Range(_minBulletCount, _maxBulletCount);
        _bulletCount = count;
    }
}
