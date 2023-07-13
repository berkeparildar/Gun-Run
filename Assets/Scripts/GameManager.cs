using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int AmmoCollected;
    public static int Level;
    public static int ElementCount;
    public static int AmmoCount;
    public static int WallCount;
    public static int ElementIncrease;
    public GameObject ammoPrefab;
    public GameObject wallPrefab;
    public GameObject spawnPoints;
    public GameObject platformTile;
    public Transform platformContainer;
    public static int currentPlatformEnd;
    public Transform objectContainer;
    public GameObject emptyGameObject;
    public GameObject gun;
    public static int currentSpawnPointEnd;

    private List<Vector3> _availableSpots = new();

    void Start()
    {
        Level = 1;
        AmmoCount = 3;
        WallCount = 1;
        currentPlatformEnd = 58;
        currentSpawnPointEnd = 64;
        for (var i = 0; i < spawnPoints.transform.childCount; i++)
        {
            _availableSpots.Add(spawnPoints.transform.GetChild(i).transform.position);
        }
        SpawnElements();
    }

    private void SpawnElements()
    {
        for (var i = 0; i < AmmoCount; i++)
        {
           var xPos = Random.Range(-2.5f, 2.5f);
           var yPos = 3;
           var randomChild = Random.Range(0, _availableSpots.Count);
           var zPos = _availableSpots[randomChild].z;
           _availableSpots.Remove(_availableSpots[randomChild]);
           var ammoPosition = new Vector3(xPos, yPos, zPos);
           var ammo = Instantiate(ammoPrefab, ammoPosition, Quaternion.identity);
           ammo.transform.SetParent(objectContainer);
        }
        for (var i = 0; i < WallCount; i++)
        {
            var spawnTwo = Random.Range(0, 2);
            if (spawnTwo == 0)
            {   
                float[] xPoses = {-2.25f, 0, 2.25f}; 
                var xPos = xPoses[Random.Range(0, 3)];
                var yPos = -0.8f;
                var randomChild = Random.Range(0, _availableSpots.Count);
                var zPos = _availableSpots[randomChild].z;
                _availableSpots.RemoveAt(randomChild);
                var wallPosition = new Vector3(xPos, yPos, zPos);
                var wall = Instantiate(wallPrefab, wallPosition, Quaternion.identity);
                wall.transform.SetParent(objectContainer);
            }
            else
            {
                var yPos = -0.8f;
                var randomChild = Random.Range(0, _availableSpots.Count);
                var zPos = _availableSpots[randomChild].z;
                var wallPositionOne = new Vector3(-2.25f, yPos, zPos);
                var wallPositionTwo = new Vector3(2.25f, yPos, zPos);
                _availableSpots.RemoveAt(randomChild);
                var wallOne = Instantiate(wallPrefab, wallPositionOne, Quaternion.identity);
                var wallTwo = Instantiate(wallPrefab, wallPositionTwo, Quaternion.identity);
                wallOne.transform.SetParent(objectContainer);
                wallTwo.transform.SetParent(objectContainer);
                i++;
            }
        }
    }

    void Update()
    {
        Debug.Log(AmmoCollected);
    }

    public void NextLevel()
    {
        for (var i = 0; i < objectContainer.transform.childCount; i++)
        {
            Destroy(objectContainer.GetChild(i).gameObject);   
        }
        _availableSpots.Clear();
        WallCount += 1; 
        var ammoChance = Random.Range(0, 2);
        if (ammoChance == 0)
        {
            AmmoCount++;
        }
        currentPlatformEnd += 16;
        currentSpawnPointEnd += 16;
        var newPosition = Instantiate(emptyGameObject, new Vector3(0, 0, currentSpawnPointEnd), Quaternion.identity);
        newPosition.transform.SetParent(spawnPoints.transform);
        var tileOne = Instantiate(platformTile, new Vector3(0, 0, currentPlatformEnd), Quaternion.identity);
        tileOne.transform.SetParent(platformContainer);
        currentPlatformEnd += 16;
        currentSpawnPointEnd += 16;
        var newPositionTwo = Instantiate(emptyGameObject, new Vector3(0, 0, currentSpawnPointEnd), Quaternion.identity);
        newPositionTwo.transform.SetParent(spawnPoints.transform);
        var tileTwo = Instantiate(platformTile, new Vector3(0, 0, currentPlatformEnd), Quaternion.identity);
        tileTwo.transform.SetParent(platformContainer);
        for (var i = 0; i < spawnPoints.transform.childCount; i++)
        {
            _availableSpots.Add(spawnPoints.transform.GetChild(i).transform.position);
        }
        SpawnElements();
    }
}
