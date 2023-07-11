using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ammo : MonoBehaviour, IPlatformObject
{
    private Transform _bulletHoles;
    public int Points { get; set; } // max is 6
    public int SumPoint { get; set; }
    public GunShoot GunShoot { get; set; }
    private int _currentBullet;

    private void Start()
    {
        SumPoint = 1;
        Points = Random.Range(0, 4);
        InitializeBullets();
    }

    private void Update()
    {
    }

    public void Perk()
    {
    }

    public void TakeHit()
    {
        Debug.Log("take hit");
        // target rotation needs to be updated everytime, currently not
        transform.DORotate(new Vector3(0, 0, 60), 0.2f, RotateMode.Fast).OnComplete(() =>
        {
        });
        _bulletHoles.GetChild(_currentBullet).GetChild(0).gameObject.SetActive(true);
        _currentBullet++;
    }

    public void Die()
    {
    }

    private void InitializeBullets()
    {
        _bulletHoles = transform.GetChild(1);
        for (int i = _bulletHoles.childCount - 1; i >=  6 - Points; i--)
        {
            _bulletHoles.GetChild(i).GetChild(0).gameObject.SetActive(true);
        }
    }
}
