using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ammo : MonoBehaviour, IPlatformObject
{
    private int _minBulletCount;
    private int _maxBulletCount;
    private int _bulletCount;

    public int Points { get; set; }
    public int SumPoint { get; set; }
    public GunShoot GunShoot { get; set; }

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void Perk()
    {
    }

    public void TakeHit()
    {
    }

    public void Die()
    {
    }

        
    private void SetBulletCount()
    {
        int count = Random.Range(_minBulletCount, _maxBulletCount);
        _bulletCount = count;
    }

}
