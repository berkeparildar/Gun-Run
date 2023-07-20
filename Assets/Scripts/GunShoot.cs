using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [SerializeField] private float range;
    private readonly float _initRange = 10;
    private GameObject _gunContainer;
    public Transform initPositions;
    [SerializeField] private float fireRate;
    private readonly float _initFireRate = 1;
    private Vector3 _bulletInitPoint;
    [SerializeField] private GameObject bulletPrefab;
    private GunMovement _gunMovement;
    private static readonly int Shoot = Animator.StringToHash("shoot");

    private void Start()
    {
        _gunMovement = GetComponent<GunMovement>();
        _gunContainer = transform.GetChild(1).gameObject;
        fireRate = _initFireRate;
        range = _initRange;
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (GameManager.StartGame)
            {
                _bulletInitPoint = initPositions.GetChild(GunChange.CurrentIndex).position;
                Instantiate(bulletPrefab, _bulletInitPoint, Quaternion.identity);
                _gunContainer.transform.DORotate(new Vector3(-20, 0, 0), fireRate / 2).OnComplete(() =>
                {
                    _gunContainer.transform.DORotate(new Vector3(0, 0, 0), fireRate / 2);
                });
                yield return new WaitForSeconds(fireRate);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void IncreaseRange(float points)
    {
        range += points;
    }

    public float GetRange()
    {
        return range;
    }

    public void IncreaseFireRate(float points)
    {
        fireRate -= points / 100;
    }
}