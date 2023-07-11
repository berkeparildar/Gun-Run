using System.Collections;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [SerializeField] private float _range;
    private readonly float _initRange = 10;
    [SerializeField] private float _fireRate;
    private readonly float _initFireRate = 1;
    private Vector3 _bulletInitPoint;
    [SerializeField] private GameObject bulletPrefab;
    

    void Start()
    {
        _fireRate = _initFireRate;
        _range = _initRange;
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            _bulletInitPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
            Instantiate(bulletPrefab, _bulletInitPoint, Quaternion.identity);
            yield return new WaitForSeconds(_fireRate);
        }
    }
    
    public void IncreaseRange(float points)
    {
        _range += points;
    }

    public float GetRange()
    {
        return _range;
    }

    public void IncreaseFireRate(float points)
    {
        _fireRate -= points / 100;
    }
}
