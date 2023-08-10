using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [SerializeField] private float range;
    private const float InitRange = 10;
    private const float InitFireRate = 1;
    private GameObject _gunContainer;
    public Transform initPositions;
    [SerializeField] private float fireRate;
    private Vector3 _bulletInitPoint;
    [SerializeField] private GameObject bulletPrefab;

    public float FireRate { get => fireRate;}

    private void Start()
    {
        _gunContainer = transform.GetChild(1).gameObject;
        fireRate = InitFireRate;
        range = InitRange;
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
        if (fireRate <= 0.3f)
        {
            fireRate -= points / 200;
        }
        else
        {
            fireRate -= points / 100;
        }
    }

    public void ResetGun()
    {
        fireRate = InitFireRate - (0.1f * GameManager.RateLevel);
        range = InitRange + (10 * GameManager.RangeLevel);
    }
}