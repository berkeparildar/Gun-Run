using System.Collections;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [SerializeField] private float range;
    private readonly float _initRange = 10;
    [SerializeField] private float fireRate;
    private readonly float _initFireRate = 1;
    private Vector3 _bulletInitPoint;
    [SerializeField] private GameObject bulletPrefab;
    private GunMovement _gunMovement;

    private void Start()
    {
        _gunMovement = GetComponent<GunMovement>();
        fireRate = _initFireRate;
        range = _initRange;
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            var gunPosition = transform.position;
            _bulletInitPoint = new Vector3(gunPosition.x
                , gunPosition.y, gunPosition.z + 2);
            Instantiate(bulletPrefab, _bulletInitPoint, Quaternion.identity);
            yield return new WaitForSeconds(fireRate);
            if (_gunMovement.isDead)
            {
                break;
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