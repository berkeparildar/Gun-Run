using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private List<Vector3> availableSpots = new();
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject spawnPoints;
    [SerializeField] private GameObject platformTile;
    [SerializeField] private GameObject ammoWall;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject adScreen;
    [SerializeField] private GameObject endZone;
    [SerializeField] private Transform platformContainer;
    [SerializeField] private Transform objectContainer;
    [SerializeField] private GameObject emptyGameObject;
    [SerializeField] private GameObject gun;
    public static int Money;
    public static bool StartGame;
    public static int IncomeLevel;
    public static int RangeLevel;
    public static int RateLevel;
    public static int YearLevel;
    private static int _ammoCount;
    private static int _wallCount;
    private static int _platformSpawnPos;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Start()
    {
        IncomeLevel = 0;
        RangeLevel = 0;
        RateLevel = 0;
        YearLevel = 0;
        _ammoCount = 3;
        _wallCount = 1;
        _platformSpawnPos = 55;
        for (var i = 0; i < spawnPoints.transform.childCount; i++)
        {
            availableSpots.Add(spawnPoints.transform.GetChild(i).transform
                .position);
        }
        SpawnElements();
    }

    private void Update()
    {
        moneyText.text = Money.ToString();
    }

    private void SpawnElements()
    {
        var childForAmmoWall = availableSpots.Count - 1;
        var ammoWallPos = new Vector3(-5, 4, availableSpots[childForAmmoWall].z);
        var spawnedAmmoWall = Instantiate(ammoWall, ammoWallPos, Quaternion.identity);
        spawnedAmmoWall.transform.SetParent(objectContainer);
        availableSpots.RemoveAt(childForAmmoWall); // remove from list
        var wallsSpawned = 0;

        for (var i = 0; i < _ammoCount; i++)
        {
            var xPos = Random.Range(-2.5f, 2.5f);
            var yPos = 3.2f;
            var randomChild = Random.Range(0, availableSpots.Count);
            var zPos = availableSpots[randomChild].z;
            availableSpots.Remove(availableSpots[randomChild]);
            var ammoPosition = new Vector3(xPos, yPos, zPos);
            var ammo = Instantiate(ammoPrefab, ammoPosition
                , Quaternion.identity);
            ammo.transform.SetParent(objectContainer);
        }

        for (var i = 0; i < _wallCount; i++)
        {
            var spawnTwo = Random.Range(0, 2);
            if (_wallCount - wallsSpawned < 2)
            {
                spawnTwo = 0;
            }

            if (spawnTwo == 0)
            {
                float[] xPoses = { -2.25f, 0, 2.25f };
                var xPos = xPoses[Random.Range(0, 3)];
                var yPos = -0.15f;
                var randomChild = Random.Range(0, availableSpots.Count);
                var zPos = availableSpots[randomChild].z;
                availableSpots.RemoveAt(randomChild);
                var wallPosition = new Vector3(xPos, yPos, zPos);
                var wall = Instantiate(wallPrefab, wallPosition
                    , Quaternion.identity);
                wallsSpawned++;
                wall.transform.SetParent(objectContainer);
            }
            else if (spawnTwo == 1)
            {
                var yPos = -0.8f;
                var randomChild = Random.Range(0, availableSpots.Count);
                var zPos = availableSpots[randomChild].z;
                var wallPositionOne = new Vector3(-2.25f, yPos, zPos);
                var wallPositionTwo = new Vector3(2.25f, yPos, zPos);
                availableSpots.RemoveAt(randomChild);
                var wallOne = Instantiate(wallPrefab, wallPositionOne
                    , Quaternion.identity);
                var wallTwo = Instantiate(wallPrefab, wallPositionTwo
                    , Quaternion.identity);
                wallsSpawned += 2;
                wallOne.transform.SetParent(objectContainer);
                wallTwo.transform.SetParent(objectContainer);
                i++;
            }
        }
    }

    public void NextLevel()
    {
        startScreen.SetActive(true);
        var position = GameObject.FindWithTag("EndZone").transform.position;
        gun.transform.position = new Vector3(0, 3, 2);
        gun.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
        gun.transform.GetChild(0).position = new Vector3(0, 3, 2);
        gun.GetComponent<GunMovement>().isDead = false;
        gun.GetComponent<GunShoot>().ResetGun();
        for (var i = 0; i < objectContainer.transform.childCount; i++)
        {
            Destroy(objectContainer.GetChild(i).gameObject);
        }
        availableSpots.Clear();
        
        _wallCount += 1;
        var ammoChance = Random.Range(0, 2);
        if (ammoChance == 0)
        {
            _ammoCount++;
        }
        
        _platformSpawnPos += 10;
        var newPosition = Instantiate(emptyGameObject
            , new Vector3(0, 0, _platformSpawnPos), Quaternion.identity);
        newPosition.transform.SetParent(spawnPoints.transform);
        var tileOne = Instantiate(platformTile
            , new Vector3(0, 0, _platformSpawnPos), Quaternion.identity);
        tileOne.transform.SetParent(platformContainer);
        _platformSpawnPos += 10;
        var newPositionTwo = Instantiate(emptyGameObject
            , new Vector3(0, 0, _platformSpawnPos), Quaternion.identity);
        newPositionTwo.transform.SetParent(spawnPoints.transform);
        var tileTwo = Instantiate(platformTile
            , new Vector3(0, 0, _platformSpawnPos), Quaternion.identity);
        tileTwo.transform.SetParent(platformContainer);

        var endZoneInit = Instantiate(endZone, new Vector3(position.x, position.y, position.z + 20), Quaternion.identity);
        endZoneInit.transform.SetParent(objectContainer);

        for (var i = 0; i < spawnPoints.transform.childCount; i++)
        {
            availableSpots.Add(spawnPoints.transform.GetChild(i).transform
                .position);
        }
        adScreen.SetActive(false);
        SpawnElements();
    }

    public void GetAdScreen()
    {
        gameOverScreen.SetActive(false);
        adScreen.SetActive(true);
    }

    public static void InitializeGame()
    {
        StartGame = true;
    }
}