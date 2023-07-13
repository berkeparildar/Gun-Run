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
    private int _amountToRotate;
    private int _currentRotate = 1;
    private Transform _player;
    private bool _isPassed;
    private CapsuleCollider _capsuleCollider;

    private void Start()
    {
        _player = GameObject.Find("Gun").transform;
        _capsuleCollider = GetComponent<CapsuleCollider>();
        SumPoint = 1;
        Points = Random.Range(0, 4);
        _amountToRotate = 6 - Points;
        InitializeBullets();
        Debug.Log(_player.position);
    }

    private void Update()
    {
        CheckPosition();
    }

    public void Perk()
    {
    }

    public void TakeHit()
    {
        Debug.Log("take hit");
        // target rotation needs to be updated everytime, currently not
        if (_currentRotate <= _amountToRotate + 1)
        {
            transform.DORotate(new Vector3(0, 0, 60 * _currentRotate), 0.2f
                    , RotateMode.Fast)
                .OnComplete(() => {});
            _bulletHoles.GetChild(_currentBullet).GetChild(0).gameObject
                .SetActive(true);
            _currentRotate++;
            _currentBullet++;
            Points++;
            if (_currentRotate == _amountToRotate + 1)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        _capsuleCollider.enabled = false;
        transform.DOMove(
            new Vector3(-8, transform.position.y, transform.position.z), 3).OnComplete(() => {
                DOTween.KillAll();
            });
        GameManager.AmmoCollected += Points;
        _isPassed = true;
    }

    private void InitializeBullets()
    {
        _bulletHoles = transform.GetChild(1);
        for (var i = _bulletHoles.childCount - 1; i >= 6 - Points; i--)
        {
            _bulletHoles.GetChild(i).GetChild(0).gameObject.SetActive(true);
        }
    }

    private void CheckPosition()
    {
        if (_player.position.z > transform.position.z && !_isPassed)
        {
            Die();
        }
    }
}
