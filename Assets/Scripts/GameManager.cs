using System.Collections.Generic;
using UnityEngine;

public struct Row
{
    public bool IsEmpty;
    public bool IsRightEmpty;
    public bool IsLeftEmpty;

    public Row(bool i, bool r, bool l)
    {
        IsEmpty = i;
        IsRightEmpty = r;
        IsLeftEmpty = l;
    }
}

public class GameManager : MonoBehaviour
{
    public static int Level;
    public GameObject ammoPrefab;
    public GameObject wallPrefab;
    public GameObject wallPoints;
    private Row[] _takenSpots =
    {
        new(true, true, true),
        new(true, true, true),
        new(true, true, true),
        new(true, true, true)
    };
    
    void Start()
    {
        Level = 8;
        SpawnWalls();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnObstacles()
    {

    }

    public void SpawnWalls()
    {
        for (int i = 0; i < Level; i++)
        {
            int randomChild = Random.Range(0, 4);
            int randomX = Random.Range(0, 2);
            while (true)
            {
                if (CheckOccupiedSpots(randomChild, randomX, i))
                {
                    break;
                }
                randomChild = Random.Range(0, 4);
                randomX = Random.Range(0, 2);
            }
            var xPos = randomX == 1 ? 2.25f : -2.25f;
            var wallPos = new Vector3(xPos, -0.8f, wallPoints.transform.GetChild(randomChild).position.z);
            var wall = Instantiate(wallPrefab, wallPos, Quaternion.identity);
        }
    }
    
    private bool CheckOccupiedSpots(int rowIndex, int xPos, int currentIteration)
    {
        if (currentIteration < 4)
        {
            if (_takenSpots[rowIndex].IsEmpty)
            {
                _takenSpots[rowIndex].IsEmpty = false;
                if (xPos == 0)
                {
                    _takenSpots[rowIndex].IsRightEmpty = false;
                }
                else
                {
                    _takenSpots[rowIndex].IsLeftEmpty = false;
                }
                return true;
            }
        }
        else
        {
            if ((xPos == 0 && _takenSpots[rowIndex].IsRightEmpty))
            {
                _takenSpots[rowIndex].IsRightEmpty = false;
                return true;
            }

            if ((xPos == 1 && _takenSpots[rowIndex].IsLeftEmpty))
            {
                _takenSpots[rowIndex].IsLeftEmpty = false;
                return true;
            }
        }
        return false;
    }
}
