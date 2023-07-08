using System.Collections;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    private float _range;
    private readonly float _initRange = 100;
    private float _fireRate;
    private readonly float _initFireRate = 10;
    private Vector3 _bulletInitPoint;
    [SerializeField] private GameObject bulletPrefab;
    

    private void Start()
    {
        _fireRate = _initFireRate;
        _range = _initRange;
        StartCoroutine(ShootCoroutine());
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _bulletCount--;
            if (_bulletCount != 0)
            {
                _bulletInitPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
                Instantiate(bulletPrefab, _bulletInitPoint, Quaternion.identity);
            }
        }*/
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            _bulletInitPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
            Instantiate(bulletPrefab, _bulletInitPoint, Quaternion.identity);
            float fireRate = 5 / _fireRate;
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    public float GetRange()
    {
        return _range;
    }
}
