using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static int Level;
    public static int Money;
    public static bool StartGame;

    public static int IncomeLevel;
    public static int RangeLevel;
    public static int RateLevel;
    public static int YearLevel;

    public TextMeshProUGUI moneyText;
    public List<Vector3> _availableSpots = new();

    public static int ElementCount;
    public static int AmmoCount;
    public static int WallCount;
    public static int ElementIncrease;

    public GameObject ammoPrefab;
    public GameObject wallPrefab;
    public GameObject spawnPoints;
    public GameObject platformTile;
    public GameObject ammoWall;
    public GameObject gameOverScreen;
    public GameObject startScreen;
    public GameObject adScreen;
    public GameObject endZone;

    public Transform platformContainer;
    public Transform objectContainer;
    public GameObject emptyGameObject;

    public GameObject gun;

    private static int _platformSpawnPos;
    private static int _endZoneSpawnPos;
    private Vector3 _ammoWallPos;

    void Start()
    {
        Level = 1;
        IncomeLevel = 0;
        RangeLevel = 0;
        RateLevel = 0;
        YearLevel = 0;
        AmmoCount = 3;
        WallCount = 1;
        _platformSpawnPos = 55;
        for (var i = 0; i < spawnPoints.transform.childCount; i++)
        {
            _availableSpots.Add(spawnPoints.transform.GetChild(i).transform
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
        var childForAmmoWall = _availableSpots.Count - 1;
        _ammoWallPos = _availableSpots[childForAmmoWall];
        var ammoWallPos = new Vector3(-5, 4, _availableSpots[childForAmmoWall].z);
        var spawnedAmmoWall = Instantiate(ammoWall, ammoWallPos, Quaternion.identity);
        spawnedAmmoWall.transform.SetParent(objectContainer);
        _availableSpots.RemoveAt(childForAmmoWall); // remove from list
        var wallsSpawned = 0;

        for (var i = 0; i < AmmoCount; i++)
        {
            var xPos = Random.Range(-2.5f, 2.5f);
            var yPos = 3;
            var randomChild = Random.Range(0, _availableSpots.Count);
            var zPos = _availableSpots[randomChild].z;
            _availableSpots.Remove(_availableSpots[randomChild]);
            var ammoPosition = new Vector3(xPos, yPos, zPos);
            var ammo = Instantiate(ammoPrefab, ammoPosition
                , Quaternion.identity);
            ammo.transform.SetParent(objectContainer);
        }

        for (var i = 0; i < WallCount; i++)
        {
            var spawnTwo = Random.Range(0, 2);
            if (WallCount - wallsSpawned < 2)
            {
                spawnTwo = 0;
            }

            if (spawnTwo == 0)
            {
                float[] xPoses = { -2.25f, 0, 2.25f };
                var xPos = xPoses[Random.Range(0, 3)];
                var yPos = -0.8f;
                var randomChild = Random.Range(0, _availableSpots.Count);
                var zPos = _availableSpots[randomChild].z;
                _availableSpots.RemoveAt(randomChild);
                var wallPosition = new Vector3(xPos, yPos, zPos);
                var wall = Instantiate(wallPrefab, wallPosition
                    , Quaternion.identity);
                wallsSpawned++;
                wall.transform.SetParent(objectContainer);
            }
            else if (spawnTwo == 1)
            {
                var yPos = -0.8f;
                var randomChild = Random.Range(0, _availableSpots.Count);
                var zPos = _availableSpots[randomChild].z;
                var wallPositionOne = new Vector3(-2.25f, yPos, zPos);
                var wallPositionTwo = new Vector3(2.25f, yPos, zPos);
                _availableSpots.RemoveAt(randomChild);
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
        gun.transform.GetChild(1).rotation = Quaternion.Euler(0, 0, 0);
        gun.transform.GetChild(1).position = new Vector3(0, 3, 2);
        gun.GetComponent<GunMovement>().isDead = false;
        gun.GetComponent<GunShoot>().ResetGun();
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
            _availableSpots.Add(spawnPoints.transform.GetChild(i).transform
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