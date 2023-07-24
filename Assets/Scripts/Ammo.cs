using DG.Tweening;
using UnityEngine;

public class Ammo : MonoBehaviour, IPlatformObject
{
    private Transform _bulletHoles;
    public float Points { get; set; }
    public float SumPoint { get; set; }
    public GunShoot GunShoot { get; set; }
    
    private int _currentBullet;
    private int _amountToRotate;
    private int _currentRotate = 1;
    private Transform _player;
    private bool _isPassed;
    private CapsuleCollider _capsuleCollider;
    private AmmoWall _ammoWall;

    private void Start()
    {
        _player = GameObject.Find("Gun").transform;
        _capsuleCollider = GetComponent<CapsuleCollider>();
        
        SumPoint = 1;
        Points = Random.Range(0, 4);
        _amountToRotate = 6 - (int) Points;
        InitializeBullets();
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
        if (_currentRotate > _amountToRotate + 1) return;
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

    public void Die()
    {
        _ammoWall = GameObject.FindWithTag("AmmoWall").GetComponent<AmmoWall>();
        _capsuleCollider.enabled = false;
        transform.DOMoveX(-6, 1).OnComplete(() =>
        {
            transform.DOMoveZ(_ammoWall.transform.position.z, 1).OnComplete(
                () =>
                {
                    _ammoWall.SpawnBullets((int)Points);
                    DOTween.instance.DOKill();
                });
        });
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
