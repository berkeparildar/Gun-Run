using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private GameObject _buttonOne;
    private GameObject _buttonTwo;
    private static int _incomeCost;
    private static int _rangeCost;

    // Start is called before the first frame update
    void Awake()
    {
        _incomeCost = 100;
        _rangeCost = 100;
        _buttonOne = transform.GetChild(1).gameObject;
        _buttonTwo = transform.GetChild(2).gameObject;
    }

    private void OnEnable()
    {
        if (GameManager.Money < 100)
        {
            _buttonOne.GetComponent<Button>().interactable = false;
            _buttonTwo.GetComponent<Button>().interactable = false;
            _buttonOne.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
            _buttonTwo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
        }
    }

// Update is called once per frame
    public void IncomeButton()
    {
        GameManager.IncomeLevel++;
        GameManager.Money -= _incomeCost * GameManager.IncomeLevel;
        _incomeCost += 100;
    }
    
    public void RangeButton()
    {
        GameManager.RangeLevel++;
        GameManager.Money -= _rangeCost * GameManager.RangeLevel;
        _rangeCost += 100;
    }
}