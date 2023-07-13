using UnityEngine;

public class AmmoWall : MonoBehaviour
{
    private GameObject _bulletContainer;
    public GameObject bulletWall;
    private Vector3 _initPos;

    void Start()
    {
        _bulletContainer = transform.GetChild(1).gameObject;
        _initPos = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
    }

    void Update()
    {
        SpawnBullets();
    }
    
    private void SpawnBullets()
    {
        // initPosu daha sonra direklere göre değiştirmek gerekiyor
        // eğer 5 ten fazla mermi varsa initPos değişecek
        var spawnCount = GameManager.AmmoCollected / 2; 
        if (spawnCount != _bulletContainer.transform.childCount)
        {
            var newBullet = Instantiate(bulletWall, _initPos, Quaternion.identity);
            newBullet.transform.SetParent(_bulletContainer.transform);
            var containerPos = _bulletContainer.transform.position;
            var targetPos = new Vector3(containerPos.x + 2, containerPos.y, containerPos.z);
           _bulletContainer.transform.position = Vector3.MoveTowards(containerPos, targetPos, 0.5f);
        }
    }
}
