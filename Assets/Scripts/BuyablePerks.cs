using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuyablePerks : MonoBehaviour
{
    public GameObject incomeButton;
    public GameObject rangeButton;
    public GameObject yearButton;
    public GameObject rateButton;
    private static int _incomeCost;
    private static int _rangeCost;
    private TextMeshProUGUI _incomeButtonText;
    private TextMeshProUGUI _rangeButtonText;
    private TextMeshProUGUI _yearButtonText;
    private TextMeshProUGUI _rateButtonText;

    // Start is called before the first frame update
    void Awake()
    {
        _incomeCost = 100;
        _rangeCost = 100;
        _incomeButtonText = incomeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _rangeButtonText = rangeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (GameManager.Money < 100)
        {
            incomeButton.GetComponent<Button>().interactable = false;
            rangeButton.GetComponent<Button>().interactable = false;
            _incomeButtonText.color = Color.red;
            _rangeButtonText.color = Color.red;
        }
    }

    private void OnDisable()
    {
        incomeButton.GetComponent<Button>().interactable = true;
        rangeButton.GetComponent<Button>().interactable = true;
        _incomeButtonText.color = Color.white;
        _rangeButtonText.color = Color.white;
        _incomeButtonText.text = ButtonText("Income", GameManager.IncomeLevel + 1, _incomeCost);
        _rangeButtonText.text = ButtonText("Range", GameManager.RangeLevel + 1, _rangeCost);
        
    }

    private string ButtonText(string perk, int level, int price)
    {
        return perk + "\n" + "Level " + level + "\n \n \n \n \n$" + price;
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
