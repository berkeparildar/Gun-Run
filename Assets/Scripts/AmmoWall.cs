using UnityEngine;

public class AmmoWall : MonoBehaviour
{
    
    private GameObject _bulletContainerOne;
    private GameObject _bulletContainerTwo;
    private GameObject _bulletContainerThree;
    public GameObject wallBullet;
    private GameObject _bulletContainer;
    private float _shift;

    void Start()
    {   
        _bulletContainerOne = transform.GetChild(0).gameObject;
        _bulletContainerTwo = transform.GetChild(1).gameObject;
        _bulletContainerThree = transform.GetChild(2).gameObject;
        _bulletContainer = _bulletContainerOne;
    }

    void Update()
    {
    }
    
    // implemented the entirety of this like the donkey I am, need refactor
    public void SpawnBullets(int amountOfBullets)
    {
        var leftOverCount = 0;
        // current number of bullets contained in the current container;
        var currentBullet = _bulletContainer.transform.childCount;
        // check if the bullet limit is exceeded
        if (currentBullet + amountOfBullets >= 5)
        {
            leftOverCount = currentBullet + amountOfBullets - 5;
            // complete the current container
            for (int i = 0; i < 5 - currentBullet; i++)
            {
                if (_bulletContainerThree.transform.childCount >= 5)
                {
                    return;
                }
                var containerPos = _bulletContainer.transform.position;
                var position = new Vector3(containerPos.x + _shift, containerPos.y, containerPos.z);
                var bullet = Instantiate(wallBullet, position, Quaternion.identity);
                bullet.transform.SetParent(_bulletContainer.transform);
                _shift += 0.5f;
            }
            
            // go next according to current, add to next
            if (_bulletContainerOne.transform.childCount == 5 && _bulletContainerTwo.transform.childCount == 0)
            {
                _bulletContainer = _bulletContainerTwo;
                if (_bulletContainer.transform.childCount == 0)
                {
                    _shift = 0;
                }
                for (int i = 0; i < leftOverCount; i++)
                {
                    var containerPos = _bulletContainer.transform.position;
                    var position = new Vector3(containerPos.x + _shift, containerPos.y, containerPos.z);
                    var bullet = Instantiate(wallBullet, position, Quaternion.identity);
                    bullet.transform.SetParent(_bulletContainer.transform);
                    _shift += 0.5f;
                }
            }
            else if (_bulletContainerOne.transform.childCount == 5 && _bulletContainerTwo.transform.childCount == 5)
            {
                _bulletContainer = _bulletContainerThree;
                if (_bulletContainer.transform.childCount == 0)
                {
                    _shift = 0;
                }
                for (int i = 0; i < leftOverCount; i++)
                {
                    var containerPos = _bulletContainer.transform.position;
                    var position = new Vector3(containerPos.x + _shift, containerPos.y, containerPos.z);
                    var bullet = Instantiate(wallBullet, position, Quaternion.identity);
                    bullet.transform.SetParent(_bulletContainer.transform);
                    _shift += 0.5f;
                }
            }
            
        }
        else
        {
            for (int i = 0; i < amountOfBullets; i++)
            {
                var containerPos = _bulletContainer.transform.position;
                var position = new Vector3(containerPos.x + _shift, containerPos.y, containerPos.z);
                var bullet = Instantiate(wallBullet, position, Quaternion.identity);
                bullet.transform.SetParent(_bulletContainer.transform);
                _shift += 0.5f;
            }
        }
    }
}
